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
        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            chatId = update.Message.Chat.Id;
            messageText = update.Message.Text;
            Console.WriteLine($"Приняли в работу: {messageText}");
            cts.Cancel();
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
            if(textToSend == null)
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

                    using (StreamWriter writetext = new StreamWriter("D:\\AMD\\gg.txt", append: true))
                    {
                        writetext.WriteLine(textToSend);
                    }
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
            if(vacancy.MinSalary != null || vacancy.MaxSalary != null)
            {
                if(vacancy.MinSalary > 0)
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
