using System;

namespace USSC.DeckSorter.BusinessLogic
{
    /// <summary>
    /// Алгоритм случайной перетасовки колоды.
    /// </summary>
    public class RandomShuffle : IShuffleAlgorithm
    {
        /// <summary>
        /// Рандомайзер.
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// Перетасовка колоды.
        /// </summary>
        /// <param name="deck">Колода для перетасовки.</param>
        /// <remarks>Тасование Фишера—Йетса.</remarks>
        public void Shuffle(IDeck deck)
        {
            for (var cardIndex = deck.Count - 1; cardIndex > 0; --cardIndex)
            {
                var newCardIndex = Random.Next(cardIndex + 1);
                var temp = deck[cardIndex];
                deck[cardIndex] = deck[newCardIndex];
                deck[newCardIndex] = temp;
            }
        }
    }
}