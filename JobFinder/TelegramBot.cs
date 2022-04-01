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
        private async Task<Message> PrepareMessage(string textToSend)
        {
            var messageToSent = await bot.SendTextMessageAsync(
                    chatId: chatId,
                    text: textToSend,
                    disableWebPagePreview: true);
             return messageToSent;
        }

        public async Task SendNewVacancy(List<Vacancy> newVacancies)
        {
            var textToSend = "";
            var chunkSize = 5;

            /*foreach (var chunk in newVacancies.Chunk(chunkSize)) 
            {
                foreach(var vacancy in chunk)
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
                Message result = await PrepareMessage(textToSend);
                textToSend = "";
            }*/

            List<Task<Message>> listOfTasks = new List<Task<Message>>();

            foreach (var chunk in newVacancies.Chunk(chunkSize))
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
                listOfTasks.Add(PrepareMessage(textToSend));
                //Message result = await PrepareMessage(textToSend);
                textToSend = "";
            }

            var x =  await Task.WhenAll<Message>(listOfTasks);

            /*foreach (var vacancy in newVacancies)
        {
            textToSend += $"{vacancy.Name} \n" +
                $"{vacancy.Location} \n" +
                $"{vacancy.MinSalary} - {vacancy.MaxSalary} {vacancy.Currency}\n" +
                $"Компания: {vacancy.EmployerName} {vacancy.EmployerLink}\n" +
                $"{vacancy.Schedule} \n" +
                $"Подробнее: {vacancy.Link} \n" +
                $"Адрес: {vacancy.Address} \n" +
                $"Станция метро: {vacancy.MetroStation} \n" +
                $"Откликнуться: {vacancy.LinkToApply} \n" +
                "---------------------------------------------\n";
        }*/
            /*
                        for (int i = 0; i < 5; i++)
                        {
                            textToSend += $"{newVacancies[i].Name} + Подробнее: {newVacancies[i].Link} \n";
                        }*/
            //Message result = await PrepareMessage(textToSend);

        }
        public void SendUpdatedVacancy(List<Vacancy> updatedVacancies)
        {
            throw new NotImplementedException();
        }
    }
}
