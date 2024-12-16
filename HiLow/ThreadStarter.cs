using HiLoSimulations.Benchmarks;
using HiLoSimulations.HiLow.Decks;
using HiLoSimulations.Logics;

namespace HiLoSimulations.HiLow
{
    /// <summary>
    /// Class that controls all the simulation threads.
    /// </summary>
    public class ThreadStarter
    {
        private readonly bool[] _threads;

        private readonly Logic _logicType;
        private readonly Deck _deckType;

        private readonly int _historyBackAmount;

        private readonly OverallControllerBenchmarks _totalBenchmarks;

        public ThreadStarter(int threads, Logic logicType, Deck deckType, int historyBackAmount)
        {
            _threads = new bool[threads];
            _logicType = logicType;
            _deckType = deckType;
            _historyBackAmount = historyBackAmount;

            _totalBenchmarks = new(threads);
        }

        /// <summary>
        /// Starts all the threads, and gives them the proper parameters.
        /// </summary>
        /// <param name="lockObject">The lock object used for the thread lock, to access the data across threads without errors.</param>
        /// <param name="cts">Cancelation Token Source. Used to tell the threads when to stop.</param>
        public void Start(object lockObject, CancellationTokenSource cts)
        {
            int customThreadId = 0;

            ThreadPool.SetMaxThreads(_threads.Length, _threads.Length);
            for (int i = 0; i < _threads.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(async (_) =>
                {
                    await Task.Run(() =>
                    {
                        int threadId;
                        lock (lockObject)
                        {
                            threadId = customThreadId++;
                            _threads[customThreadId - 1] = true;
                        }

                        Console.WriteLine($"Thread {threadId}: Started");

                        Deck deck = _deckType.Copy();

                        Controller controller = new(deck, _logicType, _historyBackAmount, threadId, lockObject, cts, _totalBenchmarks);

                        controller.GameCycle();

                        Console.WriteLine($"Thread {threadId}: Finished");
                        lock (lockObject)
                        {
                            _threads[controller.ThreadID] = false;
                        }
                    });
                });
            }
            do
            {
                Thread.Sleep(50);
            } while (!AllThreadsActive());
            Console.WriteLine($"----- Game Threads Startup Complete -----");
        }

        /// <summary>
        /// Checks whether there is active threads running or not.
        /// </summary>
        /// <returns>True if there are threads, false if not.</returns>
        public bool IsActiveThreads()
        {
            return Array.TrueForAll(_threads, (thread) => thread == true);
        }

        /// <summary>
        /// Used to check if all threads are active and running.
        /// </summary>
        /// <returns>True if all threads are active, false if not.</returns>
        public bool AllThreadsActive()
        {
            foreach (var thread in _threads)
            {
                if (thread != true)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// The collective benchmarks of all the threads getter. Simulations benchmarks.
        /// </summary>
        public OverallControllerBenchmarks TotalBenchmarks { get { return _totalBenchmarks; } }

        /// <summary>
        /// Amount of threads being ran in the current simulation.
        /// </summary>
        public int Threads { get { return _threads.Length; } }
    }
}
