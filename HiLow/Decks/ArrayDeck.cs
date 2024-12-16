namespace HiLoSimulations.HiLow.Decks
{
    /// <summary>
    /// Implementation of the abstract class deck, built off of using arrays. 
    /// </summary>
    public class ArrayDeck : Deck
    {
        private readonly int[] _cardValues;
        private readonly bool[] _usedCards;

        public ArrayDeck(bool shuffle = true)
        {
            _cardValues = new int[CARD_COUNT];
            for (int i = 0; i < CARD_COUNT; i++)
            {
                _cardValues[i] = i / 4 + 2;
            }
            _usedCards = new bool[CARD_COUNT];

            if (shuffle)
            {
                Shuffle();
            }
        }

        /// <summary>
        /// Shuffling the deck of cards using the Fisher-Yates algorithm.
        /// </summary>
        public override void Shuffle()
        {
            for (int i = CARD_COUNT - 1; i > 0; i--)
            {
                int j = Utils.random.Next(i + 1);
                (_cardValues[i], _cardValues[j]) = (_cardValues[j], _cardValues[i]);
                _usedCards[_cardValues[i]] = false;
                _usedCards[_cardValues[j]] = false;
            }
        }

        /// <summary>
        /// Resetting the cards back to default.
        /// </summary>
        /// <param name="shuffle">Whether to shuffle the deck or not.</param>
        public override void Reset(bool shuffle = true)
        {
            Array.Clear(_usedCards, 0, CARD_COUNT);
            if (shuffle)
            {
                Shuffle();
            }
        }

        /// <summary>
        /// Moves a card from the deck to the used cards, and returns that card.
        /// </summary>
        /// <returns>Integer of the card dealt.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public override int DealCard()
        {
            for (int i = 0; i < CARD_COUNT; i++)
            {
                if (!_usedCards[i])
                {
                    _usedCards[i] = true;
                    return _cardValues[i];
                }
            }
            throw new InvalidOperationException("ArrayDeck: No more cards available");
        }

        /// <summary>
        /// Whether the deck is empty or not.
        /// </summary>
        /// <returns>True if empty, false if not.</returns>
        public override bool IsEmpty()
        {
            return Array.TrueForAll(_usedCards, card => card);
        }

        /// <summary>
        /// Prints out all the cards in the deck in a neat way.
        /// </summary>
        public override void PrintCards()
        {
            Console.WriteLine(string.Join(", ", _cardValues.Where((_, i) => !_usedCards[i])));
        }

        /// <summary>
        /// Prints out all the used cards in a neat way.
        /// </summary>
        public override void PrintUsedCards()
        {
            Console.WriteLine(string.Join(", ", GetUsedCards()));
        }

        /// <summary>
        /// Returns an integer array of all the used cards.
        /// </summary>
        /// <returns>Integer array of used cards.</returns>
        public override int[] GetUsedCards() { return Enumerable.Range(0, CARD_COUNT).Where(i => _usedCards[i]).Select(i => _cardValues[i]).ToArray(); }
        
        /// <summary>
        /// Returns an array of all the cards still in the deck.
        /// </summary>
        /// <returns>An integer array of all the cards.</returns>
        public override int[] GetCards() { return _cardValues; }

        /// <summary>
        /// New instance of the ArrayDeck class.
        /// </summary>
        /// <returns>ArrayDeck object (Shuffled)</returns>
        public override Deck Copy() => new ArrayDeck();

        /// <summary>
        /// Integer array of the card values.
        /// </summary>
        public int[] CardValues { get { return _cardValues; } }
        
        /// <summary>
        /// Boolean array of all the used cards.
        /// </summary>
        public bool[] UsedCards { get { return _usedCards; } }
    }
}
