namespace TGBot.Configuration;

/// <summary>
/// Представляет настройки приложения.
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Возвращает или задает токен Telegram API.
    /// </summary>
    /// <value>Токен Telegram API.</value>
    public string? BotToken { get; init; }
}