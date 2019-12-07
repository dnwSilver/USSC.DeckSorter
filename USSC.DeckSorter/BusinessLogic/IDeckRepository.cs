using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace USSC.DeckSorter.BusinessLogic
{
    /// <summary>
    /// Репозиторий для хранения колод. Подразумевается что это будет не MemoryStorage, а база или файл.
    /// </summary>
    public interface IDeckRepository
    {
        /// <summary>
        /// Создание колоды карт.
        /// </summary>
        /// <param name="deck">Колода карт.</param>
        /// <returns>Уникальный идентификатор созданной колоды.</returns>
        Task<Guid> Create(IDeck deck);

        /// <summary>
        /// Получение набора колод.
        /// </summary>
        /// <returns>Хранимая колода.</returns>
        Task<IEnumerable<IDeck>> FindAll();
        
        /// <summary>
        /// Получение колоды по идентификатору.
        /// </summary>
        /// <returns>Хранимая колода.</returns>
        Task<IDeck> FindById(Guid deckId);
        
        /// <summary>
        /// Получение колоды по имени.
        /// </summary>
        /// <returns>Хранимая колода.</returns>
        Task<IDeck> FindByName(string deckName);
        
        /// <summary>
        /// Проверка существование колоды.
        /// </summary>
        /// <param name="deckName">Наименование колоды.</param>
        /// <returns>true - существует, false - не существует.</returns>
        Task<bool> CheckByName(string deckName);
        
        /// <summary>
        /// Проверка существование колоды.
        /// </summary>
        /// <param name="deckId">Уникальный идентификатор колоды.</param>
        /// <returns>true - существует, false - не существует.</returns>
        Task<bool> CheckById(Guid deckId);

        /// <summary>
        /// Обновление колоды в источнике данных.
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        Task<bool> Update(IDeck deck);

        /// <summary>
        /// Удаление колоды из источника данных.
        /// </summary>
        /// <param name="deckId"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid deckId);
    }
}