using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TbotForHW.Utilities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Models;
using TelegramBot.Services;

namespace TelegramBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            string taskCode = _memoryStorage.GetSession(message.Chat.Id).TaskCode;
            if (message.Text == "/start")
            {
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]
                {
                        InlineKeyboardButton.WithCallbackData($" 1" , $"1"),
                        InlineKeyboardButton.WithCallbackData($" 2" , $"2")
                    });

                // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Бот выполняет две задачи :\n1 - считает сумму введенных через пробел чисел. \"1 2 3 ...\" \n2 - считает число символов в сообщении.</b> {Environment.NewLine}" +
                    $"{Environment.NewLine}Для выбора задачи нажмите 1 или 2, по умолчанию выполняется задача 1.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

            }
            else if (taskCode == "1")
            {
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, Calc.Sum(message.Text), cancellationToken: ct);
                return;
            }
            else if (taskCode == "2")
            {
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, TextLengthReader.Length(message.Text), cancellationToken: ct);
                return;
            }
        }
    }
}
