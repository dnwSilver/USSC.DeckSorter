namespace USSC.DeckSorter.BusinessLogic
{
    /// <summary>
    /// Алгоритм перетасовки колоды.
    /// </summary>
    public interface IShuffleAlgorithm
    {
        /// <summary>
        /// Перетасовать колоду.
        /// </summary>
        /// <param name="deck">Набор карт.</param>
        void Shuffle(IDeck deck);
    }
}