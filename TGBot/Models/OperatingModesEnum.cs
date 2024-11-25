namespace TGBot.Models;

/// <summary>
/// Перечисление режимов работы бота.
/// </summary>
public enum OperatingModesEnum
{
    /// <summary>
    /// Не выбрано.
    /// </summary>
    None,
    /// <summary>
    /// Подсчет количества символов.
    /// </summary>
    NumberOfCharacters,
    /// <summary>
    /// Вычисление суммы чисел.
    /// </summary>
    SumOfNumbers
}