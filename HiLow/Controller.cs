using HiLoSimulations.Benchmarks;
using HiLoSimulations.HiLow.Decks;
using HiLoSimulations.Logics;

namespace HiLoSimulations.HiLow
{
    /// <summary>
    /// The thing the threads will be running for data.
    /// This controls the games, benchmarks, and history.
    /// This is also what stops the thread through the cts, and uses the lock object.
    /// </summary>
    class Controller
    {
        private readonly ControllerBenchmarks _controllerBenchmarks;
        private readonly OverallControllerBenchmarks _totalBenchmarks;

        private readonly Deck _deck;
        private readonly Logic _logic;

        private int _games = 0;
        private readonly GameHistory[] _history;

        private readonly int _customThreadIndex;
        private readonly Object _lockObject;
        private readonly CancellationTokenSource _cts;

        public Controller(Deck deck, Logic logic, int historyBackupAmount, int customThreadIndex, Object lockObject, CancellationTokenSource cts, OverallControllerBenchmarks totalBenchmarks)
        {
            _deck = deck;
            _logic = logic;

            _history = new GameHistory[historyBackupAmount];
            _customThreadIndex = customThreadIndex;
            _lockObject = lockObject;
            _cts = cts;

            _controllerBenchmarks = new(_history.Length, _lockObject);
            _totalBenchmarks = totalBenchmarks;
        }

        /// <summary>
        /// Runs all the data gathering games, in a cycle until the cts says to stop. It will routinely back up the game history, 
        /// at the specified number though.
        /// </summary>
        public void GameCycle()
        {
            _controllerBenchmarks.StartTimer();
            do
            {
                GameHistory currentGame = new(_deck.GetCards());
                do
                {
                    // Get cards & Mark Event
                    int tableCard = _deck.DealCard();
                    int guess = _logic.Guess(tableCard, _deck.GetCards(), _deck.GetUsedCards());
                    int dealtCard = _deck.DealCard();

                    currentGame.AddEvent(tableCard, dealtCard, guess);
                } while (!_deck.IsEmpty());

                // End Game Logic
                _deck.Reset();

                _games++;

                // Benchmarks & Updating (Max games hit)
                if (_games > _history.Length)
                {
                    _controllerBenchmarks.StopTimer();
                    _controllerBenchmarks.Mark();

                    BackupHistory();

                    _games = 0;
                    Array.Clear(_history, 0, _history.Length);


                    lock (_lockObject)
                    {
                        _totalBenchmarks.Mark(_customThreadIndex, _controllerBenchmarks);
                    }

                    _controllerBenchmarks.ResetTimer();
                    _controllerBenchmarks.StartTimer();
                }

                // Putting Last Game into History
                int firstEmptySlot = Enumerable.Range(0, _history.Length)
                    .Where(i => _history[i] == null)
                    .FirstOrDefault();

                if (firstEmptySlot != -1)
                {
                    _history[firstEmptySlot] = currentGame;
                }

            } while (!_cts.IsCancellationRequested);
        }

        /// <summary>
        /// Backs up all the game history 
        /// </summary>
        private void BackupHistory()
        {
            // TODO: Do complete this
        }

        /// <summary>
        /// The getter for the custom thread ID given to the controller when started. 
        /// </summary>
        public int ThreadID { get { return _customThreadIndex; } }
    }
}
