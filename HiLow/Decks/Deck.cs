namespace HiLoSimulations.HiLow.Decks
{
    /// <summary>
    /// The deck used in the simulation games.
    /// </summary>
    public abstract class Deck
    {
        // Base amount of cards in a deck
        protected static readonly int CARD_COUNT = 52;

        /// <summary>
        /// Shuffles all of the cards in the deck.
        /// </summary>
        public abstract void Shuffle();

        /// <summary>
        /// Resets the deck back to the start.
        /// </summary>
        /// <param name="shuffle">Shuffles the deck after setting everything up or not.</param>
        public abstract void Reset(bool shuffle = true);

        /// <summary>
        /// Deals a card from the deck. Number on a scale of 2 to 14.
        /// </summary>
        /// <returns>The card integer from the top of the deck.</returns>
        public abstract int DealCard();

        /// <summary>
        /// Whether the deck is empty or not.
        /// </summary>
        /// <returns>Deck empty or not.</returns>
        public abstract bool IsEmpty();

        /// <summary>
        /// Gets an array of all used cards.
        /// </summary>
        /// <returns>Integer array of used cards.</returns>
        public abstract int[] GetUsedCards();

        /// <summary>
        /// Getting all cards still in the deck.
        /// </summary>
        /// <returns>Integer array of yet to be dealt cards.</returns>
        public abstract int[] GetCards();

        /// <summary>
        /// Copies current deck's base class.
        /// </summary>
        /// <returns>New class of the current class.</returns>
        public abstract Deck Copy();

        /// <summary>
        /// Prints out all the cards in the deck to the console in a neat way.
        /// </summary>
        public abstract void PrintCards();

        /// <summary>
        /// Prints all of the used cards out to the console in a neat way.
        /// </summary>
        public abstract void PrintUsedCards();
    }
}
