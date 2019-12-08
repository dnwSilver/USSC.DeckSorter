namespace USSC.DeckSorter.BusinessLogic
{
    /// <summary>
    /// Колода карт.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Значение карты (2, 3, 4 ... король, туз).
        /// </summary>
        string Rank { get; }

        /// <summary>
        /// Масть карты.
        /// </summary>
        string Suit { get; }
    }
}