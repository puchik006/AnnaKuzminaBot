using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using static AnnaKuzminaBot.WordBase;

namespace AnnaKuzminaBot
{
    class ReplyKeyboards
    {
        public static ReplyKeyboardMarkup MainMenu = MainMenuUserWords;

        public static ReplyKeyboardMarkup FeedbackMenu = FeedbackMenuUserWords;

        //public static KeyboardButton FeedBackButton = new KeyboardButton("Главное меню");

        //public static List<KeyboardButton> FeedBackButoons = new List<KeyboardButton>() {FeedBackButton};

        //public static ReplyKeyboardMarkup FeedbackMenu = new ReplyKeyboardMarkup(FeedBackButoons, true);

        public static ReplyKeyboardMarkup ServiceTypeMenu = ServiceTypeMenuUserWords;

        public static ReplyKeyboardMarkup MKADMenu = MKADMenuUserWords;

        public static ReplyKeyboardMarkup MonthMenu = MonthMenuUserWords;

        public static ReplyKeyboardMarkup DateMenu31 = DateMenu31UserWords;

        public static ReplyKeyboardMarkup DateMenu30 = DateMenu30UserWords;

        public static ReplyKeyboardMarkup DateMenu29 = DateMenu29UserWords;

        public static ReplyKeyboardMarkup DateMenu28 = DateMenu28UserWords;

        public static ReplyKeyboardMarkup TimeMenu = TimeMenuUserWords;
    }
}
