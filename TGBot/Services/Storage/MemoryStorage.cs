using System.Collections.Concurrent;
using TGBot.Models;

namespace TGBot.Services.Storage;

/// <summary>
/// Хранилище сессий пользователей в памяти.
/// </summary>
public class MemoryStorage : IStorage
{
    private readonly ConcurrentDictionary<long, Session> _sessions = new();

    /// <summary>
    /// Получает сессию по идентификатору чата.
    /// Если сессия не существует, создает новую.
    /// </summary>
    /// <param name="chatId">Идентификатор чата.</param>
    /// <returns>Сессия пользователя.</returns>
    public Session GetSession(long chatId)
    {
        // Сначала мы пытаемся получить существующую сессию из словаря _sessions
        // по предоставленному идентификатору чата в качестве ключа.
        if (_sessions.TryGetValue(chatId, out var session))
            // Если сессия найдена, мы сразу же возвращаем ее.
            return session;

        // Если сессия не найдена, мы создаем новую и устанавливаем режим работы (OperatingMode) в значение "None",
        // указывая, что никакая операция еще не выбрана.
        var newSession = new Session { OperatingMode = OperatingModesEnum.None };

        // Далее мы добавляем новую сессию в словарь _sessions, используя идентификатор чата
        // в качестве ключа. Метод TryAdd обеспечивает безопасность операции для многопоточной среды.
        _sessions.TryAdd(chatId, newSession);

        // Наконец, мы возвращаем только что созданную сессию.
        return newSession;
    }
}