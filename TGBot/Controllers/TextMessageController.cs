using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TGBot.Models;
using TGBot.Services.CountOfCharacters;
using TGBot.Services.Storage;
using TGBot.Services.SumOfNumbers;

namespace TGBot.Controllers;

/// <summary>
/// Контроллер обработки текстового сообщения.
/// </summary>
public class TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage,ISumOfNumbers sumOfNumbers, ICountOfCharacters countOfCharacters)
{
    /// <summary>
    /// Обрабатывает текстовое сообщение.
    /// </summary>
    /// <param name="message">Текстовое сообщение.</param>
    /// <param name="ct">Токен отмены.</param>
    public async Task Handle(Message message, CancellationToken ct)
    {
        // Если сообщение не содержит текста, выходим из метода
        if (message.Text == null) return;

        // Обрабатываем текст сообщения
        switch (message.Text)
        {
            case "/start":
                // Создаем инлайн-клавиатуру с двумя кнопками
                var buttons = new List<InlineKeyboardButton[]>
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Количество символов" , $"char_count"),
                        InlineKeyboardButton.WithCallbackData($" Сумма чисел" , $"sum")
                    }
                };
                // Отправляем приветственное сообщение с инлайн-клавиатурой
                await telegramBotClient.SendMessage(message.Chat.Id, $"<b>  Этот бот умеет подсчитывать количество символов в тексте и вычислять сумму чисел, которые вы ему отправляете (одним сообщением через пробел).</b> {Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                break;
            default:
                // Получаем режим работы из сессии пользователя
                switch (memoryStorage.GetSession(message.Chat.Id).OperatingMode)
                {
                    case OperatingModesEnum.None:
                        // Если режим не выбран, отправляем сообщение с просьбой выбрать режим работы
                        await telegramBotClient.SendMessage(message.Chat.Id, "Выберите режим работы в главном меню", cancellationToken: ct);
                        break;
                    case OperatingModesEnum.NumberOfCharacters:
                        // Если выбран режим подсчета количества символов, вызываем соответствующий метод
                        await countOfCharacters.GetCount(message,ct);
                        break;
                    case OperatingModesEnum.SumOfNumbers:
                        // Если выбран режим вычисления суммы чисел, вызываем соответствующий метод
                        await sumOfNumbers.GetSum(message,ct);
                        break;
                }
                
                break;
        }
    }
}