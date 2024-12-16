using BenchmarkDotNet.Attributes;
using HiLoSimulations.HiLow.Decks;

namespace HiLoSimulations.Benchmarks
{
    /// <summary>
    /// Benchmarks class for all of the decks.
    /// Used to test deck speeds, to see which is most efficient for running simulations with.
    /// 
    /// Current Ranking:
    ///     1. Array Deck
    ///     2. Queue Deck
    ///     
    /// </summary>
    public class DeckComparison
    {
        private ArrayDeck _arrayDeck;
        private QueueDeck _queueDeck;

        /// <summary>
        /// Setup/Start of the deck benchmark.
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            _arrayDeck = new ArrayDeck();
            _queueDeck = new QueueDeck();
            _arrayDeck.Reset(false);
            _queueDeck.Reset(false);
        }

        /// <summary>
        /// The Card Dealing Benchmarks
        /// </summary>
        [Benchmark]
        public void ArrayDeckDeal()
        {
            _arrayDeck.DealCard();
        }

        [Benchmark]
        public void QueueDeckDeal()
        {
            _queueDeck.DealCard();
        }

        /// <summary>
        /// The resetting Benchmarks for the decks
        /// </summary>
        [Benchmark]
        public void ArrayDeckReset()
        {
            _arrayDeck.Reset(shuffle: false);
        }

        [Benchmark]
        public void QueueDeckReset()
        {
            _queueDeck.Reset(shuffle: false);
        }

        /// <summary>
        /// The shuffling benchmarks for the decks.
        /// </summary>
        [Benchmark]
        public void ArrayDeckShuffle()
        {
            _arrayDeck.Shuffle();
        }

        [Benchmark]
        public void QueueDeckShuffle()
        {
            _queueDeck.Shuffle();
        }

        /// <summary>
        /// Full use benchmarks for the decks.
        /// </summary>
        [Benchmark]
        public void FullArrayDeck()
        {
            _arrayDeck = new();
        
            for (int i = 0; i < 3; i++)
            {
                _arrayDeck.Reset();
                while (_arrayDeck.IsEmpty())
                {
                    _arrayDeck.DealCard();
                }
            }
        }

        [Benchmark]
        public void FullQueueDeck()
        {
            _queueDeck = new();

            for (int i = 0; i < 3; i++)
            {
                _queueDeck.Reset();
                while (_queueDeck.IsEmpty())
                {
                    _queueDeck.DealCard();
                }
            }
        }
    }
}
