using System;

namespace USSC.DeckSorter.BusinessLogic
{
    /// <summary>
    /// Алгоритм перетасовки колоды руками.
    /// </summary>
    public class RandomSnuffle : IShuffleAlgorithm
    {  
        /// <summary>
        /// Рандомайзер.
        /// </summary>
        static readonly Random random = new Random();
        
        /// <summary>
        /// Перетасовка колоды.
        /// </summary>
        /// <param name="deck">Колода для перетасовки.</param>
        public void Shuffle(IDeck deck)
        {
            for (var cardIndex = deck.Count - 1; cardIndex > 0; --cardIndex)
            {
                var newCardIndex = random.Next(cardIndex+1);
                var temp = deck[cardIndex];
                deck[cardIndex] = deck[newCardIndex];
                deck[newCardIndex] = temp;
            }
        }
    }
}