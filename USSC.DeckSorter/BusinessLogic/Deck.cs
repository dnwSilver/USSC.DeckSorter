using System;
using System.Collections;
using System.Collections.Generic;

namespace USSC.DeckSorter.BusinessLogic
{
    /// <summary>
    /// Колода карт.
    /// </summary>
    public class Deck : HashSet<ICard>, IDeck
    {
        /// <summary>
        /// Размер стандартной колоды карт.
        /// </summary>
        private const int DECK_CAPACITY = 52;
        
        /// <summary>
        /// Уникальный идентификатор колоды.
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// Уникальное имя колоды.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Конструктор для <see cref="Deck"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор колоды.</param>
        /// <param name="name">Уникальное имя колоды.</param>
        public Deck(Guid id, string name) : base(DECK_CAPACITY)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Добавление новой карты в колоду.
        /// </summary>
        /// <param name="card">Добавляемая карта.</param>
        /// <returns>true - карта добавлена, false - карта не добавлена.</returns>
        public new bool Add(ICard card)
        {
            base.Add(card);
            return true;
        }
        
        /// <summary>
        /// Перетасовать колоду.
        /// </summary>
        public void Shuffle()
        {
            //todo Тут необходимо вызывать алгоритм сортировки.
            throw new NotImplementedException();
        }
    }
}