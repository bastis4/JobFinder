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
        private static readonly int _messageLimit = 4000;

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
        private async Task<Message> SendMessage(String textToSend)
        {
            await Task.Delay(1000);
            var messageToSent = await bot.SendTextMessageAsync(
                    chatId: chatId,
                    text: textToSend,
                    disableWebPagePreview: true);
             return messageToSent;
        }

        public async Task SendNewVacancy(List<Vacancy> newVacancies)
        {
            var builder = new StringBuilder();
            var textToSend = "";
            var listOfTasks = new List<Task<Message>>();
            foreach (var vacancy in newVacancies)
            {
                Console.WriteLine(vacancy.HhId);
            }

            for (int i = 0; i < newVacancies.Count;)
            {
                var t = builder.Length;
                var stringToCheck = StringFormer(newVacancies[i]);
                if (builder.Length <= _messageLimit - stringToCheck.Length)
                {
                    var length = builder.Length;
                    builder.Append(stringToCheck);
                    var finalLength = builder.Length;
                    i++;
                }
                if (builder.Length > _messageLimit - stringToCheck.Length || i-1 == newVacancies.Count -1)
                {
                    listOfTasks.Add(SendMessage(builder.ToString()));
                    using (StreamWriter writetext = new StreamWriter("D:\\AMD\\gg.txt", append: true))
                    {
                        writetext.WriteLine(builder);
                    }
                    builder.Clear();
                }
            }
/*
                foreach (var vacancy in newVacancies)
            {
                if (builder.Length < _messageLimit)
                {
                    var length = builder.Length;
                    builder.Append(StringFormer(vacancy));
                    var finalLength = builder.Length;
                }
                else
                {
                    listOfTasks.Add(SendMessage(builder.ToString()));
                    builder.Clear();
                } 
            }*/

            /*            var chunkSize = 5;
             *            foreach (var chunk in newVacancies.Chunk(chunkSize))
                        {
                            foreach (var vacancy in chunk)
                            {
                                textToSend += $"{vacancy.Name} \n" +
                                $"{vacancy.Location} \n" +
                                $"{vacancy.MinSalary} - {vacancy.MaxSalary} {vacancy.Currency}\n" +
                                $"Компания: {vacancy.EmployerName} {vacancy.EmployerLink}\n" +
                                $"{vacancy.Schedule} \n" +
                                $"Подробнее: {vacancy.Link} \n" +
                                $"Адрес: {vacancy.Address} \n" +
                                "---------------------------------------------------------------\n";
                            }
                            Console.WriteLine(textToSend);
                            listOfTasks.Add(SendMessage(textToSend));
                            //Message result = await PrepareMessage(textToSend);
                            textToSend = "";
                        }*/

            var x =  await Task.WhenAll<Message>();
        }

        private StringBuilder StringFormer(Vacancy vacancy)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"id {vacancy.HhId} + {vacancy.Name} \n" +
                   $"{vacancy.Location} \n" +
                   $"{vacancy.MinSalary} - {vacancy.MaxSalary} {vacancy.Currency}\n" +
                   $"Компания: {vacancy.EmployerName} {vacancy.EmployerLink}\n" +
                   $"{vacancy.Schedule} \n" +
                   $"Подробнее: {vacancy.Link} \n" +
                   $"Адрес: {vacancy.Address} \n" +
                   "---------------------------------------------------------------\n");
            return builder;

        }
 
    }
}
