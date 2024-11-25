using Telegram.Bot;
using Telegram.Bot.Types;
using ILogger = TGBot.Services.Logger.ILogger;

namespace TGBot.Services.SumOfNumbers;

/// <summary>
/// Класс для получения суммы чисел.
/// </summary>
public class SumOfNumbers(ITelegramBotClient telegramBotClient, ILogger logger) : ISumOfNumbers
{
    /// <summary>
    /// Получает сумму чисел в сообщении.
    /// </summary>
    /// <param name="message">Текстовое сообщение.</param>
    /// <param name="ct">Токен отмены.</param>
    public async Task GetSum(Message message, CancellationToken ct)
    {
        try
        {
            // Разделяем текст сообщения на отдельные числа и парсим их в целые числа
            var numbersList = message.Text?.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            // Отправляем сообщение с суммой чисел
            await telegramBotClient.SendMessage(message.Chat.Id, $"Сумма чисел: {numbersList!.Sum()}", cancellationToken: ct);
        }
        catch (Exception ex)
        {
            // Логируем ошибку и отправляем сообщение об ошибке
            logger.Error(ex.Message);
            await telegramBotClient.SendMessage(message.Chat.Id, "Не удалось распознать числа", cancellationToken: ct);
        }
    }
}