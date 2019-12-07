using System;
using System.Collections.Generic;
using RiskFirst.Hateoas.Models;

namespace USSC.DeckSorter.Responses
{
    /// <summary>
    /// Тело ответа для описания колоды.
    /// </summary>
    public class DeckResponse : ILinkContainer
    {
        /// <summary>
        /// Уникальный идентификатор колоды.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Наименование колоды.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список доступных действий надо колодой.
        /// </summary>
        public Dictionary<string, Link> Links { get; set; } = new Dictionary<string, Link>();
        
        /// <summary>
        /// Добавление ссылки для действия над колодой.
        /// </summary>
        /// <param name="id">Уникальный идентификатор колоды.</param>
        /// <param name="link">Ссылка на действие.</param>
        public void AddLink(string id, Link link)
        {
            Links.Add(id, link);
        }
    }
}