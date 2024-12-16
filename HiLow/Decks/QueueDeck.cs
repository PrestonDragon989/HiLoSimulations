namespace HiLoSimulations.HiLow.Decks
{
    /// <summary>
    /// A deck, but it's based off of the Queue and List objects.
    /// </summary>
    internal class QueueDeck : Deck
    {
        private static readonly int[] CONSTANT_DECK = new int[52];
        private readonly Queue<int> _cardValues;
        private readonly List<int> _usedCards;

        static QueueDeck()
        {
            for (int i = 0; i < 52; i++)
            {
                CONSTANT_DECK[i] = i / 4 + 2;
            }
        }

        public QueueDeck(bool shuffle = true)
        {
            _cardValues = new(CONSTANT_DECK);
            _usedCards = new();

            if (shuffle)
            {
                Shuffle();
            }
        }

        /// <summary>
        /// Shuffles all of the cards using the Fisher-Yates algorithm. 
        /// </summary>
        public override void Shuffle()
        {
            int[] deckArray = _cardValues.ToArray();
            for (int i = deckArray.Length - 1; i > 0; i--)
            {
                int j = Utils.random.Next(0, i + 1);

                // Swap the elements at indices i and j
                (deckArray[i], deckArray[j]) = (deckArray[j], deckArray[i]);
            }
            _cardValues.Clear();
            foreach (int c in deckArray)
            {
                _cardValues.Enqueue(c);
            }
        }

        /// <summary>
        /// Deals card from the top of the deck, returns it, then moves it to the used cards.
        /// </summary>
        /// <returns>Card (integer) dealt.</returns>
        public override int DealCard()
        {
            int r = _cardValues.Dequeue();  // O(1) operation
            _usedCards.Add(r);  // O(1) operation

            return r;
        }

        /// <summary>
        /// Resets the whole deck. Empties used cards, and sets the main deck back up.
        /// </summary>
        /// <param name="shuffle">Whether to shuffle the deck at the start or not.</param>
        public override void Reset(bool shuffle = true)
        {
            _cardValues.Clear();
            foreach (int card in CONSTANT_DECK)
            {
                _cardValues.Enqueue(card);
            }

            _usedCards.Clear();

            if (shuffle)
            {
                Shuffle();
            }
        }

        /// <summary>
        /// Whether the deck of cards is empty or not.
        /// </summary>
        /// <returns>Deck empty or not.</returns>
        public override bool IsEmpty() => _cardValues.Count == 0;

        /// <summary>
        /// Prints out all the cards still in the deck in a neat way.
        /// </summary>
        public override void PrintCards()
        {
            foreach (var c in _cardValues)
            {
                Console.WriteLine(c);
            }
        }

        /// <summary>
        /// Prints out all the used cards in a neat way to the console.
        /// </summary>
        public override void PrintUsedCards()
        {
            foreach (var c in _usedCards)
            {
                Console.WriteLine(c);
            }
        }

        /// <summary>
        /// An array of all the used cards.
        /// </summary>
        /// <returns>An integer array of all the used cards.</returns>
        public override int[] GetUsedCards() { return _usedCards.ToArray(); }

        /// <summary>
        /// An array of all the cards in the deck.
        /// </summary>
        /// <returns>An integer array of all the cards in the deck.</returns>
        public override int[] GetCards() { return _cardValues.ToArray(); }

        /// <summary>
        /// A new QueueDeck object.
        /// </summary>
        /// <returns>QueueDeck Object. (Shuffled)</returns>
        public override Deck Copy() => new QueueDeck();

        /// <summary>
        /// The Queue object of the deck.
        /// </summary>
        public Queue<int> CardValues { get { return _cardValues; } }
        
        /// <summary>
        /// The list of used cards.
        /// </summary>
        public List<int> UsedCards { get { return _usedCards; } }
    }
}
