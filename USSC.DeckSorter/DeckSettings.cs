namespace USSC.DeckSorter
{
    /// <summary>
    /// Настройки для колоды.
    /// </summary>
    public class DeckSettings
    {
        /// <summary>
        /// Ручная сортировка.
        /// </summary>
        public bool HandSnuffle { get; set; }
        
        /// <summary>
        /// Доступные карты.
        /// </summary>
        public string[] Ranks { get; set; }
        
        /// <summary>
        /// Масти карт.
        /// </summary>
        public string[] Suits { get; set; }
    }
}