using Telegram.Bot;
using Telegram.Bot.Types;
using TGBot.Services.Logger;

namespace TGBot.Controllers;

/// <summary>
/// Контроллер обработки сообщений по умолчанию.
/// </summary>
public class DefaultMessageController(ITelegramBotClient telegramBotClient,ILogger logger)
{
    /// <summary>
    /// Обрабатывает сообщение.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    /// <param name="ct">Токен отмены.</param>
    public async Task Handle(Message message, CancellationToken ct)
    {
        // Логируем событие о получении сообщения в контроллере
        logger.Event($"Контроллер {GetType().Name} получил сообщение");
        // Отправляем ответное сообщение в чат, из которого пришло исходное сообщение
        await telegramBotClient.SendMessage(message.Chat.Id, $"Получено сообщение не поддерживаемого формата", cancellationToken: ct);
    }
}