namespace TGBot.Services.Logger;

/// <summary>
/// Класс логгера, реализующий интерфейс ILogger.
/// </summary>
internal class Logger : ILogger
{
    /// <summary>
    /// Регистрирует событие с указанным сообщением.
    /// </summary>
    /// <param name="message">Сообщение о событии (тип string)</param>
    public void Event(string message)
    {
        // Сохраняем текущий цвет текста консоли в переменной.
        var color = Console.ForegroundColor;

        // Меняем цвет текста консоли на синий.
        Console.ForegroundColor = ConsoleColor.Blue;

        // Записываем сообщение о событии в консоль.
        Console.WriteLine(message);

        // Восстанавливаем исходный цвет текста консоли.
        Console.ForegroundColor = color;
    }

    /// <summary>
    /// Регистрирует ошибку с указанным сообщением.
    /// </summary>
    /// <param name="message">Сообщение об ошибке (тип string)</param>
    public void Error(string message)
    {
        // Сохраняем текущий цвет текста консоли в переменной.
        var color = Console.ForegroundColor;

        // Меняем цвет текста консоли на красный.
        Console.ForegroundColor = ConsoleColor.Red;

        // Записываем сообщение об ошибке в консоль.
        Console.WriteLine(message);

        // Восстанавливаем исходный цвет текста консоли.
        Console.ForegroundColor = color;
    }
}