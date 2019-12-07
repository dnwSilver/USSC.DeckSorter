using System.Linq;
using USSC.DeckSorter.BusinessLogic;

namespace USSC.DeckSorter.Responses
{
    /// <summary>
    /// Маппер для перевода объекта <see cref="Deck"/> в <see cref="DeckResponse"/>.
    /// </summary>
    /// <remarks> Желательно это всё делать через AutoMapper.</remarks>
    public class DeckMapper
    {
        /// <summary>
        /// Преобразование объектов.
        /// </summary>
        /// <param name="deck">Объект который будет преобразовываться.</param>
        /// <returns>Готовый для ответа объект.</returns>
        public DeckResponse Map(IDeck deck)
        {
            return new DeckResponse
            {
                Id = deck.Id,
                Name = deck.Name,
                Cards = deck.Select(card=>card.ToString()).ToArray()
            };
        }
    }
}