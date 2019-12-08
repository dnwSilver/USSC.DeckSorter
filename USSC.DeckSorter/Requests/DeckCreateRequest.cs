using System.ComponentModel.DataAnnotations;

namespace USSC.DeckSorter.Requests
{
    /// <summary>
    /// Тело запроса для создания колоды.
    /// </summary>
    public class DeckCreateRequest
    {
        /// <summary>
        /// Наименование колоды.
        /// </summary>
        [Required]
        [StringLength(30 , MinimumLength = 2)]
        public string Name { get; set; }
    }
}