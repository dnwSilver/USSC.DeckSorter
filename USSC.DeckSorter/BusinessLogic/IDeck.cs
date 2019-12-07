﻿using System;
using System.Collections.Generic;

namespace USSC.DeckSorter.BusinessLogic
{
    /// <summary>
    /// Колода карт.
    /// </summary>
    public interface IDeck: IEnumerable<ICard>
    {
        /// <summary>
        /// Уникальный идентификатор колоды.
        /// </summary>
        Guid Id { get; }
        
        /// <summary>
        /// Уникальное имя колоды.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Перетасовать колоду.
        /// </summary>
        void Shuffle();
    }
}