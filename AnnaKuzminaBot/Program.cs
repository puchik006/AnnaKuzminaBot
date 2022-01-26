using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using static AnnaKuzminaBot.ReplyKeyboards;
using static AnnaKuzminaBot.WordBase;
using System.Collections.Generic;

namespace AnnaKuzminaBot
{
    public class Program
    {
        static ITelegramBotClient Bot;

        static MyDataTypeList check = new MyDataTypeList();

        public static Dictionary<string, string> LastMessage = new Dictionary<string, string>();

        public static Dictionary<string, string> ServiceTypeAnswer = new Dictionary<string, string>();
        public static Dictionary<string, string> UserNameAnswer = new Dictionary<string, string>();
        public static Dictionary<string, string> UserNameSavedAnswer = new Dictionary<string, string>();
        public static Dictionary<string, string> PhoneAnswer = new Dictionary<string, string>();
        public static Dictionary<string, string> MonthAnswer = new Dictionary<string, string>();
        public static Dictionary<string, string> DayAnswer = new Dictionary<string, string>();
        public static Dictionary<string, string> MKADAnswer = new Dictionary<string, string>();
        public static Dictionary<string, string> TimeAnswer = new Dictionary<string, string>();
        public static Dictionary<string, string> CommentAnswer = new Dictionary<string, string>();

        public static int MainMenuItem = 0;

        static void Main(string[] args)
        {
                string token = "";

                Bot = new TelegramBotClient(token);

                var me = Bot.GetMeAsync().Result;

                Console.WriteLine(me.FirstName + " " + me.Username + " " + me.Id);

                Bot.StartReceiving();

                Bot.OnMessage += BotOnMessageReceived;

                Console.ReadKey();

                Bot.StopReceiving();
        }

        public static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            int AnyaChatID = 331744894;
            int BuChatID = 625820708;

            var message = messageEventArgs.Message;
            var chat = messageEventArgs.Message.Chat;
            string TextMessage = message.Text;

            try
            {
                string username = messageEventArgs.Message.From.FirstName + " " + messageEventArgs.Message.From.LastName + " @" + messageEventArgs.Message.From.Username;

                if (message == null) return;

                //Start bot
                if (TextMessage == "/start")
                {
                    await Bot.SendTextMessageAsync(chat,
                        "Привет, я бот-помощник визажиста Анны Кузьминой @darzere." + "\r\n" +
                        "У меня ты можешь записаться (кнопка запись)" + "\r\n" +
                        "узнать подробнее об услугах и ценах (прайс лист)" + "\r\n" +
                        "получить ответы на некоторые свои вопросы (FAQ)" + "\r\n" +
                        "помочь мне стать лучше (обратная связь)",
                        replyMarkup: check.datalist[MainMenuItem].Keyboard);

                    LastMessage[username] = null;

                    ServiceTypeAnswer[username] = null;
                    UserNameAnswer[username] = null;
                    UserNameSavedAnswer[username] = "asdikagkaasdqq";
                    PhoneAnswer[username] = null;
                    MonthAnswer[username] = null;
                    DayAnswer[username] = null;
                    MKADAnswer[username] = null;
                    TimeAnswer[username] = null;
                    CommentAnswer[username] = null;

                    Console.WriteLine(username + " start bot at time: " + DateTime.Now.ToString("g"));

                }

                if (TextMessage == "тест")
                {
                    //for sending ics appointment
                    await Bot.SendTextMessageAsync(chat, GetRequset("https://tools.emailmatrix.ru/event-generator/"));
                }

                //Main menu button
                if (TextMessage == check.datalist[MainMenuItem].UserMessage)
                {
                    await Bot.SendTextMessageAsync(chat, check.datalist[MainMenuItem].BotMessage, replyMarkup: check.datalist[MainMenuItem].Keyboard);

                    LastMessage[username] = null;

                    ServiceTypeAnswer[username] = null;
                    UserNameAnswer[username] = null;
                    UserNameSavedAnswer[username] = "asdikagkaasdqq";
                    PhoneAnswer[username] = null;
                    MonthAnswer[username] = null;
                    DayAnswer[username] = null;
                    MKADAnswer[username] = null;
                    TimeAnswer[username] = null;
                    CommentAnswer[username] = null;

                }

                //for user name answer
                if (LastMessage[username] == "Записаться" && TextMessage != "Записаться")
                {
                    UserNameAnswer[username] = TextMessage;

                    LastMessage[username] = TextMessage;

                    await Bot.SendTextMessageAsync(chat, "Укажите номер вашего телефона", replyMarkup: FeedbackMenu);
                }

                if (UserNameAnswer[username] != null)
                {
                    UserNameSavedAnswer[username] = UserNameAnswer[username];

                    UserNameAnswer[username] = null;

                    LastMessage[username] = TextMessage;
                }

                //for phone answer
                if (LastMessage[username] == UserNameSavedAnswer[username] && TextMessage != UserNameSavedAnswer[username])
                {
                    PhoneAnswer[username] = TextMessage;

                    LastMessage[username] = TextMessage;

                    check.FillChainTable(check.datalist, PhoneAnswer[username], ServiceTypeMenuUserWords, ServiceTypeBotWord, MonthMenu, false, true, nameof(ServiceTypeAnswer));

                    await Bot.SendTextMessageAsync(chat, "Выберите услугу на которую хотите записаться", replyMarkup: ServiceTypeMenu);
                }

                //for feedback replies
                if ((LastMessage[username] == "Обратная связь" && TextMessage != "Обратная связь"))
                {
                    await Bot.SendTextMessageAsync(AnyaChatID, "<b>" + username + " leave feedback reply: </b>" + "\r\n" + TextMessage, ParseMode.Html);
                    await Bot.SendTextMessageAsync(BuChatID, "<b>" + username + " leave feedback reply: </b>" + "\r\n" + TextMessage, ParseMode.Html);
                }

                //for comment answer
                if ((LastMessage[username] == "В пределах МКАД" && TextMessage != "В пределах МКАД") || (LastMessage[username] == "За МКАД" && TextMessage != "За МКАД"))
                {
                    await Bot.SendTextMessageAsync(chat, "Спасибо за оставленный заказ - Аня свяжется с вами в ближашее время " +
                            "для подтверждения заказа и уточнение деталей", replyMarkup: check.datalist[MainMenuItem].Keyboard);

                    CommentAnswer[username] = TextMessage;

                    LastMessage[username] = null;

                    //TODO: separate method for admin messages
                    await Bot.SendTextMessageAsync(AnyaChatID,
                        "Name: " + UserNameSavedAnswer[username] + "\r\n" +
                        "Telename: " + username + "\r\n" +
                        "Phone: " + PhoneAnswer[username] + "\r\n" +
                        "Ser.Type: " + ServiceTypeAnswer[username] + "\r\n" +
                        "Month: " + MonthAnswer[username] + "\r\n" +
                        "Day: " + DayAnswer[username] + "\r\n" +
                        "Time: " + TimeAnswer[username] + "\r\n" +
                        "MKAD: " + MKADAnswer[username] + "\r\n" +
                        "Comment: " + CommentAnswer[username]
                        );

                    await Bot.SendTextMessageAsync(BuChatID,
                        "Name: " + UserNameSavedAnswer[username] + "\r\n" +
                        "Telename: " + username + "\r\n" +
                        "Phone: " + PhoneAnswer[username] + "\r\n" +
                        "Ser.Type: " + ServiceTypeAnswer[username] + "\r\n" +
                        "Month: " + MonthAnswer[username] + "\r\n" +
                        "Day: " + DayAnswer[username] + "\r\n" +
                        "Time: " + TimeAnswer[username] + "\r\n" +
                        "MKAD: " + MKADAnswer[username] + "\r\n" +
                        "Comment: " + CommentAnswer[username]
                        );

                }

                //MyDataTypeList buttons
                for (int i = 1; i < check.datalist.Count; i++)
                {
                    if (TextMessage == check.datalist[i].UserMessage && LastMessage[username] == check.datalist[i].UserPreviousMessage)
                    {
                        await Bot.SendTextMessageAsync(chat, check.datalist[i].BotMessage, ParseMode.Html, replyMarkup: check.datalist[i].Keyboard);

                        //check if keyboard need to be repeated after interaction
                        if (check.datalist[i].RepeatKeyboard == false)
                        {
                            LastMessage[username] = TextMessage;
                        }

                        //check if answer should be saved
                        if (check.datalist[i].Answered == true)
                        {
                            Console.WriteLine(check.datalist[i].Answer);

                            switch (check.datalist[i].Answer)
                            {
                                case nameof(UserNameAnswer):
                                    UserNameAnswer[username] = TextMessage;
                                    break;
                                case nameof(PhoneAnswer):
                                    PhoneAnswer[username] = TextMessage;
                                    break;
                                case nameof(ServiceTypeAnswer):
                                    ServiceTypeAnswer[username] = TextMessage;
                                    break;
                                case nameof(MonthAnswer):
                                    MonthAnswer[username] = TextMessage;
                                    break;
                                case nameof(DayAnswer):
                                    DayAnswer[username] = TextMessage;
                                    break;
                                case nameof(MKADAnswer):
                                    MKADAnswer[username] = TextMessage;
                                    break;
                                case nameof(TimeAnswer):
                                    TimeAnswer[username] = TextMessage;
                                    break;
                                case nameof(CommentAnswer):
                                    CommentAnswer[username] = TextMessage;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine("Good");
            }
        }
        //this method request JSON to EMX service
        static string GetRequset(string address)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://tools.emailmatrix.ru/event-generator/");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"apikey\":\"y6GDS4btEHAt47sYr5ai123139019\"," +
                                "\"start\":\"2021-09-28 00:00\"," +
                                "\"end\":\"2021-09-28 01:00\"," +
                                "\"timezone\":\"Europe/Moscow\"," +
                                "\"title\":\"Событие\"," +
                                "\"url\":\"http://emailmatrix.ru\"," +
                                "\"location\":\"г. Рязань, 390010, ул. Октябрьская, д. 65, H264\"," +
                                "\"description\":\"Описание события\"," +
                                "\"remind\":\"2\"," +
                                "\"remind_unit\":\"h\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            var streamReader = new StreamReader(httpResponse.GetResponseStream());

            var result = streamReader.ReadToEnd();

            object a = JsonConvert.DeserializeObject(result);

            object b = JsonConvert.SerializeObject(a, Formatting.Indented);

            var j = JObject.Parse(result);

            string icsLink = (string)j["ics"];

            return icsLink;
        }
    }
}
