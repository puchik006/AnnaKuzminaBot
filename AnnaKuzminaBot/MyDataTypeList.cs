using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;
using static AnnaKuzminaBot.ReplyKeyboards;
using static AnnaKuzminaBot.WordBase;
using static AnnaKuzminaBot.Program;

namespace AnnaKuzminaBot
{
    public class MyDataTypeList
    {
        public MyDataTypeList()
        {
            FillMyData();
        }

        public List<MyDataType> datalist = new List<MyDataType>();

        public void FillChainTable(List<MyDataType> listToFill, string previousWord, string currentWord, string botWord, ReplyKeyboardMarkup nextKeyboard,
            bool repeatKeyboard = false, bool answered = false, string answer = null, bool endOfChain = false)
        {
            listToFill.Add(new MyDataType()
            {
                UserPreviousMessage = previousWord,
                UserMessage = currentWord,
                BotMessage = botWord,
                Keyboard = nextKeyboard,
                RepeatKeyboard = repeatKeyboard,
                Answered = answered,
                Answer = answer,
                EndOfChain = endOfChain
            });
                
        }

        // one to many to one bot
        public void FillChainTable(List<MyDataType> listToFill, string previousWord, string[][] currentWords, string botWord, ReplyKeyboardMarkup nextKeyboard, 
            bool repeatKeyboard = false, bool answered = false, string answer = null, bool endOfChain = false)
        {
            for (int k = 0; k < currentWords.Length; k ++ )
            {
                for (int l = 0; l < currentWords[k].Length; l++)
                {
                    listToFill.Add(new MyDataType()
                    {
                        UserPreviousMessage = previousWord,
                        UserMessage = currentWords[k][l],
                        BotMessage = botWord,
                        Keyboard = nextKeyboard,
                        RepeatKeyboard = repeatKeyboard,
                        Answered = answered,
                        Answer = answer,
                        EndOfChain = endOfChain
                    }) ;
                }
            }
        }

        public void FillChainTable(List<MyDataType> listToFill, string previousWord, string[][] currentWords, string[][] botWords, ReplyKeyboardMarkup nextKeyboard,
            bool repeatKeyboard = false, bool answered = false, string answer = null, bool endOfChain = false)
        {
            for (int k = 0; k < currentWords.Length; k++)
            {
                for (int l = 0; l < currentWords[k].Length; l++)
                {
                    listToFill.Add(new MyDataType()
                    {
                        UserPreviousMessage = previousWord,
                        UserMessage = currentWords[k][l],
                        BotMessage = botWords[k][l],
                        Keyboard = nextKeyboard,
                        RepeatKeyboard = repeatKeyboard,
                        Answered = answered,
                        Answer = answer,
                        EndOfChain = endOfChain
                    });
                }
            }
        }

        public void FillChainTable(List<MyDataType> listToFill, string[][] previousWords, string[][] currentWords, string botWord, ReplyKeyboardMarkup nextKeyboard,
            bool repeatKeyboard = false, bool answered = false, string answer = null, bool endOfChain = false)
        {
            for (int i = 0; i < previousWords.Length; i++)
            {
                for (int j = 0; j < previousWords[i].Length; j++)
                {
                    for (int k = 0; k < currentWords.Length; k++)
                    {
                        for (int l = 0; l < currentWords[k].Length; l++)
                        {
                            listToFill.Add(new MyDataType()
                            {
                                UserPreviousMessage = previousWords[i][j],
                                UserMessage = currentWords[k][l],
                                BotMessage = botWord,
                                Keyboard = nextKeyboard,
                                RepeatKeyboard = repeatKeyboard,
                                Answered = answered,
                                Answer = answer,
                                EndOfChain = endOfChain
                            });
                        }
                    }
                }
            }
        }

        public void FillMyData()
        {
            #region MainMenu
            datalist.Add(new MyDataType() { UserPreviousMessage = null, UserMessage = MainMenuUserWords[2][0], BotMessage = MainMenuBotWords[2][0], Keyboard = MainMenu});
            datalist.Add(new MyDataType() { UserPreviousMessage = null, UserMessage = MainMenuUserWords[0][0], BotMessage = MainMenuBotWords[0][0], Keyboard = FeedbackMenu});
            datalist.Add(new MyDataType() { UserPreviousMessage = null, UserMessage = MainMenuUserWords[0][1], BotMessage = MainMenuBotWords[0][1], Keyboard = ServiceTypeMenu });
            datalist.Add(new MyDataType() { UserPreviousMessage = null, UserMessage = MainMenuUserWords[1][0], BotMessage = FAQMenuBotWord, Keyboard = FeedbackMenu });
            datalist.Add(new MyDataType() { UserPreviousMessage = null, UserMessage = MainMenuUserWords[1][1], BotMessage = MainMenuBotWords[1][1], Keyboard = FeedbackMenu });
            #endregion

            #region ServiceTypeMenu
            //FillChainTable(datalist, UserNameAnswer, ServiceTypeMenuUserWords, ServiceTypeBotWord, FeedbackMenu, false, true, nameof(ServiceTypeAnswer));
            #endregion

            #region PriceListMenu
            FillChainTable(datalist, MainMenuUserWords[0][1], ServiceTypeMenuUserWords, PriceListMenuBotWords, ServiceTypeMenu, true);
            #endregion

            #region MonthMenu
            for (int i = 0; i < ServiceTypeMenuUserWords.Length; i++)
            {
                for (int j = 0; j < ServiceTypeMenuUserWords[i].Length; j++)
                {
                    for (int k = 0; k < MonthMenuUserWords.Length; k++)
                    {
                        for (int l = 0; l < MonthMenuUserWords[k].Length; l++)
                        {
                            if ((k == 0 && l == 0) || (k == 0 && l == 2) || (k == 1 && l == 1) || (k == 2 && l == 0) || (k == 2 && l == 1) || (k == 3 && l == 0) || (k == 3 && l == 2))
                            {
                                datalist.Add(new MyDataType()
                                {
                                    UserPreviousMessage = ServiceTypeMenuUserWords[i][j],
                                    UserMessage = MonthMenuUserWords[k][l],
                                    BotMessage = MonthMenuBotWord,
                                    Keyboard = DateMenu31,
                                    Answered = true,
                                    Answer = nameof(MonthAnswer)
                                });
                            }
                            else if ((k == 0 && l == 1))
                            {
                                datalist.Add(new MyDataType()
                                {
                                    UserPreviousMessage = ServiceTypeMenuUserWords[i][j],
                                    UserMessage = MonthMenuUserWords[k][l],
                                    BotMessage = MonthMenuBotWord,
                                    Keyboard = DateMenu29,
                                    Answered = true,
                                    Answer = nameof(MonthAnswer)
                                });
                            }
                            else
                            {
                                datalist.Add(new MyDataType()
                                {
                                    UserPreviousMessage = ServiceTypeMenuUserWords[i][j],
                                    UserMessage = MonthMenuUserWords[k][l],
                                    BotMessage = MonthMenuBotWord,
                                    Keyboard = DateMenu30,
                                    Answered = true,
                                    Answer = nameof(MonthAnswer)
                                });
                            }
                        }
                    }
                }
            }
            #endregion

            #region DateMenu
            FillChainTable(datalist, MonthMenuUserWords, DateMenu31UserWords, DateMenuBotWord, TimeMenu, false, true, nameof(DayAnswer));
            #endregion

            #region MKADMenu
            FillChainTable(datalist, TimeMenuUserWords, MKADMenuUserWords, MKADMenuBotWord, FeedbackMenu, false, true, nameof(MKADAnswer));
            #endregion

            #region TimeMenu
            FillChainTable(datalist, DateMenu31UserWords, TimeMenuUserWords, TimeMenuBotWord, MKADMenu, false, true, nameof(TimeAnswer));
            #endregion
        }
    }
}
