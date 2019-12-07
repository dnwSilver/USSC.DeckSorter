using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace USSC.DeckSorter.BusinessLogic
{
    /// <summary>
    /// Сервис для управление картами.
    /// </summary>
    public interface IDeckService
    {
        /// <summary>
        /// Создание новой колоды.
        /// </summary>
        /// <param name="deckName">Наименование колоды.</param>
        /// <returns>Уникальный идентификатор колоды.</returns>
        Task<Guid> CreateNewDeck(string deckName);

        /// <summary>
        /// Поиск колоды карт по идентификатору.
        /// </summary>
        /// <param name="deckId">Уникальный идентификатор колоды.</param>
        /// <returns>Колода карт.</returns>
        Task<IDeck> Find(Guid deckId);
        
        /// <summary>
        /// Поиск колоды карт по имени.
        /// </summary>
        /// <param name="deckName">Наименование колоды.</param>
        /// <returns>Колода карт.</returns>
        Task<IDeck> Find(string deckName);
        
        /// <summary>
        /// Поиск всех колод карт по имени.
        /// </summary>
        /// <returns>Колода карт.</returns>
        Task<IEnumerable<IDeck>> Find();
        
        /// <summary>
        /// Проверка существования колоды карт.
        /// </summary>
        /// <param name="deckId">Уникальный идентификатор колоды карт.</param>
        /// <returns>true - колода существует, false - колода не существует.</returns>
        Task<bool> IsDeckExists(Guid deckId);
        
        /// <summary>
        /// Проверка существования колоды карт.
        /// </summary>
        /// <param name="deckId">Уникальный идентификатор колоды карт.</param>
        /// <returns>false - колода существует, true - колода не существует.</returns>
        Task<bool> IsDeckNotExists(Guid deckId);
        
        /// <summary>
        /// Перетасовка колоды.
        /// </summary>
        /// <param name="deckId">Уникальный идентификатор колоды.</param>
        Task<bool> SnuffleDeck(Guid deckId);

        /// <summary>
        /// Удаление колоды.
        /// </summary>
        /// <param name="id">Уникальный идентификатор колоды.</param>
        Task<bool> Remove(Guid id);
    }
}