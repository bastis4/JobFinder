﻿using JobFinder.HhApi.Models;
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
                AllowedUpdates = { }
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
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            var x = update.Message;
            chatId = update.Message.Chat.Id;
            messageText = update.Message.Text;
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
        private async Task<Message> PrepareMessage(Vacancy vacancy)
        {
            var messageToSent = await bot.SendTextMessageAsync(
                    chatId: chatId,
                    text: vacancy.Name);
            return messageToSent;
        }

        public async Task SendNewVacancy(Vacancy vacancy)
        {
            Message result = await PrepareMessage(vacancy);
        }
        public void SendUpdatedVacancy(Vacancy vacancy)
        {
            throw new NotImplementedException();
        }
    }
}
