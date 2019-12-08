using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USSC.DeckSorter.BusinessLogic
{
    /// <summary>
    /// Репозиторий для хранения колод.
    /// </summary>
    public class DeckRepository : IDeckRepository
    {
        /// <summary>
        /// Хранилище колод в памяти.
        /// </summary>
        private static readonly HashSet<IDeck> InMemoryStorage = new HashSet<IDeck>();

        /// <summary>
        /// Создание колоды карт.
        /// </summary>
        /// <param name="deck">Колода карт.</param>
        /// <returns>Уникальный идентификатор созданной колоды.</returns>
        public async Task<Guid> Create(IDeck deck)
        {
            InMemoryStorage.Add(deck);
            return deck.Id;
        }

        /// <summary>
        /// Получение набора колод.
        /// </summary>
        /// <returns>Хранимая колода.</returns>
        public async Task<IEnumerable<IDeck>> FindAll()
        {
            return InMemoryStorage;
        }

        /// <summary>
        /// Получение колоды по идентификатору.
        /// </summary>
        /// <returns>Хранимая колода.</returns>
        public async Task<IDeck> FindById(Guid deckId)
        {
            return InMemoryStorage.FirstOrDefault(x => x.Id == deckId);
        }

        /// <summary>
        /// Получение колоды по имени.
        /// </summary>
        /// <returns>Хранимая колода.</returns>
        public async Task<IDeck> FindByName(string deckName)
        {
            return InMemoryStorage.FirstOrDefault(x => x.Name == deckName);
        }

        /// <summary>
        /// Проверка существование колоды.
        /// </summary>
        /// <param name="deckName">Наименование колоды.</param>
        /// <returns>true - существует, false - не существует.</returns>
        public async Task<bool> CheckByName(string deckName)
        {
            return InMemoryStorage.Any(x => x.Name == deckName);
        }

        /// <summary>
        /// Проверка существование колоды.
        /// </summary>
        /// <param name="deckId">Уникальный идентификатор колоды.</param>
        /// <returns>true - существует, false - не существует.</returns>
        public async Task<bool> CheckById(Guid deckId)
        {
            return InMemoryStorage.Any(x => x.Id == deckId);
        }

        /// <summary>
        /// Обновление колоды в источнике данных.
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        public async Task<bool> Update(IDeck deck)
        {
            if (!await CheckById(deck.Id))
            {
                return false;
            }

            InMemoryStorage.Add(deck);
            return true;
        }

        /// <summary>
        /// Удаление колоды из источника данных.
        /// </summary>
        /// <param name="deckId"></param>
        /// <returns></returns>
        public async Task<bool> Delete(Guid deckId)
        {
            if (!await CheckById(deckId))
            {
                return false;
            }

            var deck = await FindById(deckId);
            InMemoryStorage.Remove(deck);
            return true;
        }
    }
}