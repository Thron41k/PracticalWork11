using Telegram.Bot.Types;

namespace TGBot.Services.CountOfCharacters;

/// <summary>
/// Интерфейс для подсчета количества символов в тексте.
/// </summary>
public interface ICountOfCharacters
{
    /// <summary>
    /// Получает количество символов в тексте.
    /// </summary>
    /// <param name="message">Текстовое сообщение.</param>
    /// <param name="ct">Токен отмены.</param>
    Task GetCount(Message message, CancellationToken ct);
}