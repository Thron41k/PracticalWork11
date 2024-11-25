using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TGBot.Controllers;
using TGBot.Services.Logger;

namespace TGBot;

/// <summary>
/// Реализация бота Telegram.
/// </summary>
internal class Bot(ITelegramBotClient telegramClient, TextMessageController textMessageController, InlineKeyboardController inlineKeyboardController, DefaultMessageController defaultMessageController,ILogger logger) : BackgroundService
{
    /// <summary>
    /// Метод, выполняющий основную логику выполнения бота.
    /// </summary>
    /// <param name="stoppingToken">Токен отмены для управления выполнением бота.</param>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Запускаем получение обновлений от Telegram API
        telegramClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions(),
            cancellationToken: stoppingToken);

        logger.Event("Бот запущен");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод для обработки обновлений от Telegram API.
    /// </summary>
    /// <param name="botClient">Клиент Telegram API.</param>
    /// <param name="update">Обновление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        switch (update.Type)
        {   
            case UpdateType.CallbackQuery:
                // Обрабатываем нажатие кнопки
                await inlineKeyboardController.Handle(update.CallbackQuery, cancellationToken);
                return;
            case UpdateType.Message:
                if (update.Message == null) return;
                switch (update.Message!.Type)
                {
                    case MessageType.Text:
                        // Обрабатываем текстовое сообщение
                        await textMessageController.Handle(update.Message, cancellationToken);
                        return;
                    default:
                        // Обрабатываем другие типы сообщений
                        await defaultMessageController.Handle(update.Message, cancellationToken);
                        return;
                }
        }
    }

    /// <summary>
    /// Метод для обработки ошибок при получении обновлений от Telegram API.
    /// </summary>
    /// <param name="botClient">Клиент Telegram API.</param>
    /// <param name="exception">Исключение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // Формируем сообщение об ошибке
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        logger.Error(errorMessage);
        logger.Event("Ожидаем 10 секунд перед повторным подключением.");
        Thread.Sleep(10000);
        return Task.CompletedTask;
    }
}