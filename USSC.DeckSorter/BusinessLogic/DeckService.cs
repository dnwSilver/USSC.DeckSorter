using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace USSC.DeckSorter.BusinessLogic
{
    /// <summary>
    /// Сервис для управление картами.
    /// </summary>
    public class DeckService : IDeckService
    {
        /// <summary>
        /// Набор настроек для колоды карт.
        /// </summary>
        private readonly IOptionsMonitor<DeckSettings> _deckSettings;

        /// <summary>
        /// Репозиторий для работы с колодой.
        /// </summary>
        private readonly IDeckRepository _deckRepository;

        /// <summary>
        /// Репозиторий для работы с колодой.
        /// </summary>
        private readonly IShuffleAlgorithm _shuffleAlgorithm;

        /// <summary>
        /// Конструктор для <see cref="DeckService"/>.
        /// </summary>
        /// <param name="deckSettings">Набор настроек для колоды карт.</param>
        /// <param name="deckRepository">Репозиторий для работы с колодой.</param>
        /// <param name="shuffleAlgorithm">Алгоритм перетасовки колоды..</param>
        public DeckService(IOptionsMonitor<DeckSettings> deckSettings, IDeckRepository deckRepository,
            IShuffleAlgorithm shuffleAlgorithm)
        {
            _deckSettings = deckSettings;
            _deckRepository = deckRepository;
            _shuffleAlgorithm = shuffleAlgorithm;
        }

        /// <summary>
        /// Создание новой колоды.
        /// </summary>
        /// <param name="deckName">Наименование колоды.</param>
        /// <returns>Уникальный идентификатор колоды.</returns>
        public async Task<Guid> CreateNewDeck(string deckName)
        {
            var deck = new Deck(Guid.NewGuid(), deckName);

            foreach (var suit in _deckSettings.CurrentValue.Suits)
            {
                foreach (var rank in _deckSettings.CurrentValue.Ranks)
                {
                    deck.Add(new Card(rank, suit));
                }
            }

            var deckId = await _deckRepository.Create(deck);

            return deckId;
        }

        /// <summary>
        /// Поиск колоды карт по идентификатору.
        /// </summary>
        /// <param name="deckId">Уникальный идентификатор колоды.</param>
        /// <returns>Колода карт.</returns>
        public async Task<IDeck> Find(Guid deckId)
        {
            return await _deckRepository.FindById(deckId);
        }

        /// <summary>
        /// Поиск колоды карт по имени.
        /// </summary>
        /// <param name="deckName">Наименование колоды карт.</param>
        /// <returns>Колода карт.</returns>
        public async Task<IDeck> Find(string deckName)
        {
            return await _deckRepository.FindByName(deckName);
        }

        /// <summary>
        /// Поиск всех колод карт по имени.
        /// </summary>
        /// <returns>Колода карт.</returns>
        public async Task<IEnumerable<IDeck>> Find()
        {
            return await _deckRepository.FindAll();
        }

        /// <summary>
        /// Проверка существования колоды карт.
        /// </summary>
        /// <param name="deckId">Уникальный идентификатор колоды карт.</param>
        /// <returns>true - колода существует, false - колода не существует.</returns>
        public async Task<bool> IsDeckExists(Guid deckId)
        {
            return await _deckRepository.CheckById(deckId);
        }

        /// <summary>
        /// Проверка существования колоды карт.
        /// </summary>
        /// <param name="deckId">Уникальный идентификатор колоды карт.</param>
        /// <returns>false - колода существует, true - колода не существует.</returns>
        public async Task<bool> IsDeckNotExists(Guid deckId)
        {
            return !await _deckRepository.CheckById(deckId);
        }

        /// <summary>
        /// Перетасовка колоды.
        /// </summary>
        /// <param name="deckId">Уникальный идентификатор колоды.</param>
        public async Task<bool> SnuffleDeck(Guid deckId)
        {
            var deck = await _deckRepository.FindById(deckId);
            _shuffleAlgorithm.Shuffle(deck);
            return await _deckRepository.Update(deck);
        }

        /// <summary>
        /// Удаление колоды.
        /// </summary>
        /// <param name="id">Уникальный идентификатор колоды.</param>
        public async Task<bool> Remove(Guid id)
        {
            return await _deckRepository.Delete(id);
        }
    }
}