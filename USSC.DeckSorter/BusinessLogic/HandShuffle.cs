using System;

namespace USSC.DeckSorter.BusinessLogic
{
    /// <summary>
    /// Алгоритм ручной перетасовки колоды.
    /// </summary>
    public class HandShuffle : IShuffleAlgorithm
    {
        /// <summary>
        /// Рандомайзер.
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// Перетасовка колоды в ручном стиле.
        /// </summary>
        /// <param name="deck">Колода для перетасовки.</param>
        public void Shuffle(IDeck deck)
        {
            var countSwap = Random.Next(50,60);
            while (countSwap != 0)
            {
                Swap(deck, Random.Next(10, 40));
                countSwap--;
            }
        }

        /// <summary>
        /// Перенос карт из конца колод в начало, с сохранение порядка.
        /// </summary>
        /// <param name="deck">Колода для перетасовки.</param>
        /// <param name="swapIndex">Индекс карты смещения колоды.</param>
        public void Swap(IDeck deck, int swapIndex)
        {
            throw new NotImplementedException();
        }
    }
}