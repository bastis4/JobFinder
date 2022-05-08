using JobFinder.HhApi.Models;
using JobFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace JobFinder
{
    public class TelegramBot : IMessenger
    {
        private static readonly string _token = "5134107568:AAEhKE0fybgOLWM_qz5r8WEX9NscwZhxn8g";
        private static TelegramBotClient bot = new TelegramBotClient(_token);
        private static CancellationTokenSource cts = new CancellationTokenSource();
        private long chatId;
        private string messageText;
        private static readonly int _messageLimit = 4090;
        private readonly int _delayMsec = 1500;
  

        public async Task<string> GetKeywordsToSearchForVacancies()
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
                ThrowPendingUpdates = true // чистим все старые запросы https://api.telegram.org/bot5134107568:AAEhKE0fybgOLWM_qz5r8WEX9NscwZhxn8g/getUpdates
            };


            Console.WriteLine("Что ищем?");
            await bot.ReceiveAsync(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token
            );

            return messageText;
        }

       public static async void SendInline(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {
            InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(text: "месяц", callbackData: "month"),
                        InlineKeyboardButton.WithCallbackData(text: "2 недели", callbackData: "fortnight"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(text: "неделя", callbackData: "week"),
                        InlineKeyboardButton.WithCallbackData(text: "день", callbackData: "day"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(text: "<< отмена", callbackData: "cancel"),
                    },

                });

            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Пожалуйста, выберите временной интервал: ",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }
        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    SendInline(botClient: botClient, chatId: message.Chat.Id, cancellationToken: cancellationToken);
                    return;
                }
            }
            if (update.Type == UpdateType.CallbackQuery)
            {
                string codeOfButton = update.CallbackQuery.Data;
                if (codeOfButton == "month")
                {
                    Console.WriteLine("Выбран месяц");
                    string telegramMessage = "Вы выбрали месяц";
                    await botClient.SendTextMessageAsync(chatId: update.CallbackQuery.Message.Chat.Id, telegramMessage, parseMode: ParseMode.Html);
                }
                if (codeOfButton == "fortnight")
                {
                    Console.WriteLine("Выбрано 2 недели");
                    string telegramMessage = "Вы выбрали 2 недели";

                    InlineKeyboardMarkup inlineKeyBoard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(text: "Кнопка 3", callbackData: "post3"),
                                InlineKeyboardButton.WithCallbackData(text: "Кнопка 4", callbackData: "post4"),
                            },

                        });
                    await bot.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, telegramMessage, replyMarkup: inlineKeyBoard, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                    messageText = update.Message.Text;
                    Console.WriteLine($"Приняли в работу: {messageText}");
                    cts.Cancel();
                }
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task<Message> SendMessage(string textToSend, int delay)
        {
            if (textToSend == null)
            {
                Console.WriteLine("Empty");
            }
            var messageToSent = await bot.SendTextMessageAsync(
                                chatId: chatId,
                                text: textToSend,
                                parseMode: ParseMode.Html,
                                disableWebPagePreview: true);

            await Task.Delay(delay);

            return messageToSent;
        }

        public async Task SendNewVacancy(List<Vacancy> newVacancies)
        {
            var textToSend = new StringBuilder();
            var listOfTasks = new List<Task<Message>>();

            Console.WriteLine(newVacancies.Count);

            Task<Message> prevTask = default;

            for (int i = 0; i < newVacancies.Count;)
            {
                var t = textToSend.Length;
                var stringToCheck = StringFormer(newVacancies[i]);

                if (textToSend.Length <= _messageLimit - stringToCheck.Length)
                {
                    var length = textToSend.Length;
                    textToSend.Append(stringToCheck);
                    var finalLength = textToSend.Length;
                    i++;
                }
                if (textToSend.Length > _messageLimit - stringToCheck.Length || i - 1 == newVacancies.Count - 1)
                {
                    var msg = textToSend.ToString();
                    var delay = _delayMsec;

                    if (prevTask != default)
                    {
                        var result = prevTask.ContinueWith(async (x) => await SendMessage(msg, delay));

                        prevTask = result.Unwrap();
                    }
                    else
                    {
                        prevTask = SendMessage(msg, delay);
                    }

                    listOfTasks.Add(prevTask);
                    /*using (StreamWriter writetext = new StreamWriter("D:\\AMD\\gg.txt", append: true))
                    {
                        writetext.WriteLine(textToSend);
                    }*/
                    textToSend.Clear();
                }
            }
            await Task.WhenAll<Message>(listOfTasks);
        }

        private StringBuilder StringFormer(Vacancy vacancy)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(char.ConvertFromUtf32(0x1F41D) + $" <a href='{vacancy.Link}'><b>{vacancy.Name}</b></a>\n");
            builder.Append($"<b>{vacancy.EmployerName}</b>\n");
            if (vacancy.Location != null)
            {
                builder.Append(vacancy.Location + "\n");
            }
            if (vacancy.MinSalary != null || vacancy.MaxSalary != null)
            {
                if (vacancy.MinSalary > 0)
                {
                    builder.Append($"от {Convert.ToDecimal(vacancy.MinSalary):#,0.#}");
                }
                if (vacancy.MaxSalary > 0)
                {
                    builder.Append($" до {Convert.ToDecimal(vacancy.MaxSalary):#,0.#}");
                }
                builder.Append($" {vacancy.Currency}\n");
            }
            if (vacancy.Address != null)
            {
                builder.Append(vacancy.Address + "\n");
            }
            builder.Append($"<i>{ vacancy.Schedule}</i>\n\n");
            return builder;
        }
    }
}
