using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Model
{
    public class CreatePassCommand : Command
    {
        public override string Name => "/createPassword";
        private TelegramBotClient bot;


        public override bool Contains(string command)
        {
            return command.Contains(this.Name);
        }

        public override  void Execute(Message message, TelegramBotClient botClient)
        {
            var pass = Guid.NewGuid().ToString();
            bot = botClient;


             botClient.SendTextMessageAsync(message.Chat.Id, pass.Replace('-', 'a'));
            

            var inlineKeyboard = new InlineKeyboardMarkup(new[]{
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Da"),
                    InlineKeyboardButton.WithCallbackData("Net")
                }

            });
            botClient.OnCallbackQuery += Bot_OnCallbackQuery;

            botClient.SendTextMessageAsync(message.Chat.Id, "Хотите сохранить пароль?", replyMarkup:inlineKeyboard);

        }

        private void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var buttonText = e.CallbackQuery.Data;

            if(buttonText == "Da")
            {
                //Проверить есть ли пользователь, если нет - предложить зарегаться или войти в аккаунт, если есть пользователь - спросить для какого сайта
            }
            else if(buttonText == "Net")
            {
                bot.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Nu net tak net");
            }
            bot.OnCallbackQuery -= Bot_OnCallbackQuery;
        }
    }
}
