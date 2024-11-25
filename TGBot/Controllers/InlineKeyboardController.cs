using Telegram.Bot;
using Telegram.Bot.Types;
using TGBot.Models;
using TGBot.Services.Storage;

namespace TGBot.Controllers;

/// <summary>
/// Контроллер обработки нажатий кнопок.
/// </summary>
public class InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
{
    /// <summary>
    /// Обрабатывает нажатие кнопки.
    /// </summary>
    /// <param name="callbackQuery">Запрос от кнопки.</param>
    /// <param name="ct">Токен отмены.</param>
    public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
    {
        // Если запрос не содержит данных, выходим из метода
        if (callbackQuery?.Data == null)
            return;

        // Обрабатываем данные запроса
        switch (callbackQuery.Data)
        {
            case "char_count":
                // Устанавливаем режим работы в сессии пользователя
                memoryStorage.GetSession(callbackQuery.Message!.Chat.Id).OperatingMode = OperatingModesEnum.NumberOfCharacters;
                // Отправляем сообщение пользователю с просьбой ввести текст
                await telegramBotClient.SendMessage(callbackQuery.Message!.Chat.Id, "Введите текст", cancellationToken: ct);
                break;
            case "sum":
                // Устанавливаем режим работы в сессии пользователя
                memoryStorage.GetSession(callbackQuery.Message!.Chat.Id).OperatingMode = OperatingModesEnum.SumOfNumbers;
                // Отправляем сообщение пользователю с просьбой ввести числа
                await telegramBotClient.SendMessage(callbackQuery.Message!.Chat.Id, "Введите числа через пробел", cancellationToken: ct);
                break;
        }
    }
}