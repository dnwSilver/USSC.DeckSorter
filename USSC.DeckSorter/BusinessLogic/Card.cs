namespace USSC.DeckSorter.BusinessLogic
{
    /// <summary>
    /// Игральная карта.
    /// </summary>
    public class Card : ICard
    {
        /// <summary>
        /// Значение карты.
        /// </summary>
        public string Rank { get; }

        /// <summary>
        /// Масть карты.
        /// </summary>
        public string Suit { get; }

        /// <summary>
        /// Конструктор для карты.
        /// </summary>
        /// <param name="rank">Значение карты.</param>
        /// <param name="suit">Масть карты.</param>
        public Card(string rank, string suit)
        {
            Rank = rank;
            Suit = suit;    
        }
        
        /// <summary>
        /// Генерация Hash кода.
        /// </summary>
        /// <returns>Hash код.</returns>
        public override int GetHashCode()
        {
            return Rank.GetHashCode() + Suit.GetHashCode();
        }

        /// <summary>
        /// Переопределение процедуры сравнения двух объектов типа <see cref="Card"/>.
        /// </summary>
        /// <param name="obj">Другой объект для сравнения с этим экземпляром объекта.</param>
        /// <returns>true - объекты равны, false - объекты не равны.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Card otherCard))
                return false;

            return this.Rank == otherCard.Rank && this.Suit == otherCard.Suit;
        }

        /// <summary>
        /// Приведение карты к строковому типу.
        /// </summary>
        public override string ToString()=> $"{this.Rank} {this.Suit}";
    }
}