namespace TGBot.Models;

/// <summary>
/// Класс сессии пользователя.
/// </summary>
public class Session
{
    /// <summary>
    /// Режим работы бота для текущей сессии.
    /// </summary>
    public OperatingModesEnum OperatingMode { get; set; }
}