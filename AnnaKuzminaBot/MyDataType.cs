using Telegram.Bot.Types.ReplyMarkups;

namespace AnnaKuzminaBot
{
    public class MyDataType
    {
        public string UserPreviousMessage;
        public string UserMessage;
        public string BotMessage;
        public ReplyKeyboardMarkup Keyboard;
        public bool RepeatKeyboard = false;
        public bool Answered = false;
        public string Answer;
        public bool EndOfChain = false;
    }
}
