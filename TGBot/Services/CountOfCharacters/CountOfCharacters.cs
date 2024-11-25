using Telegram.Bot;
using Telegram.Bot.Types;
using TGBot.Services.Logger;

namespace TGBot.Services.CountOfCharacters;

/// <summary>
/// Класс для подсчета количества символов в тексте.
/// </summary>
public class CountOfCharacters(ITelegramBotClient telegramBotClient, ILogger logger) : ICountOfCharacters
{
    /// <summary>
    /// Получает количество символов в тексте.
    /// </summary>
    /// <param name="message">Текстовое сообщение.</param>
    /// <param name="ct">Токен отмены.</param>
    public async Task GetCount(Message message, CancellationToken ct)
    {
        try
        {
            // Подсчитываем количество символов в тексте
            var count = message.Text!.Length;
            // Отправляем сообщение с количеством символов
            await telegramBotClient.SendMessage(message.Chat.Id, $"Количество символов в тексте: {count}", cancellationToken: ct);
        }
        catch (Exception ex)
        {
            // Логируем ошибку
            logger.Error(ex.Message);
            // Отправляем сообщение об ошибке
            await telegramBotClient.SendMessage(message.Chat.Id, "Не удалось распознать текст", cancellationToken: ct);
        }
    }
}