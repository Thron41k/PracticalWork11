using Telegram.Bot.Types;

namespace TGBot.Services.SumOfNumbers;

/// <summary>
/// Интерфейс для получения суммы чисел.
/// </summary>
public interface ISumOfNumbers
{
    /// <summary>
    /// Получения суммы чисел в сообщении.
    /// </summary>
    /// <param name="message">Сообщение, содержащее числа для суммирования.</param>
    /// <param name="ct">Токен отмены.</param>
    Task GetSum(Message message, CancellationToken ct);
}