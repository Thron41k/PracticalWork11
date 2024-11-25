using TGBot.Models;

namespace TGBot.Services.Storage;

/// <summary>
/// Интерфейс для работы с хранилищем сессий.
/// </summary>
public interface IStorage
{
    /// <summary>
    /// Получает сессию по идентификатору чата.
    /// </summary>
    /// <param name="chatId">Идентификатор чата.</param>
    /// <returns>Сессия пользователя.</returns>
    Session GetSession(long chatId);
}