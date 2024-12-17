using HiLoSimulations.HiLow;
using HiLoSimulations.HiLow.Decks;
using HiLoSimulations.Logics;
using IniParser.Model;
using System.Diagnostics;

namespace HiLoSimulations.Programs
{
    /// <summary>
    /// The simulations program.
    /// This program is made to use a profile's information, and then run several threads to check the data for 
    /// </summary>
    /// <seealso cref="ProgramSection"/>
    /// <seealso cref="ThreadStarter"/>
    public class Simulations : ProgramSection
    {
        private const string _presentationName = "HiLo Data Simulations";

        private readonly CancellationTokenSource _cts = new();
        private readonly Object _lockObject = new();

        private readonly Stopwatch _simulationsTimer = new();
        private int _programUpdateTimes = 1;

        private ThreadStarter? _threadStarter;

        private int _amountOfThreads;
        private int _historyBackupAmount;

        /// <summary>
        /// Runs the threads, monitors them, and controls console output/input.
        /// </summary>
        /// <seealso cref="ProgramSection"/>
        /// <seealso cref="ThreadStarter"/>
        public override void RunProgram()
        {
            PrintNotes();

            Console.WriteLine($"----- Game Threads Starting -----");

            _threadStarter = new(_amountOfThreads, new PlaceholderLogic(), new ArrayDeck(), _historyBackupAmount);
            _threadStarter.Start(_lockObject, _cts);
            
            _simulationsTimer.Start();

            Console.WriteLine($"----- Program Officially Running | Press Enter to receive an update -----");
            do
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.X || key.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine($"----- Game Threads CLosing -----");
                        _cts.Cancel();
                    }

                    if (key.Key == ConsoleKey.Enter)
                    {
                        PrintUpdate();
                    }
                }
            } while (_threadStarter.IsActiveThreads());
            _simulationsTimer.Stop();

            Thread.Sleep(500);
            Console.WriteLine($"All Threads Complete - Total Program Time: {_simulationsTimer.Elapsed}");

            PrintExit();
        }

        /// <summary>
        /// Sets the amount of threads and history backup amount.
        /// </summary>
        /// <seealso cref="SimulationsConfig"/>
        public void SetInputs(int threads, int gamesBeforeBackup)
        {
            _amountOfThreads = threads;
            _historyBackupAmount = gamesBeforeBackup;
        }

        /// <summary>
        /// Prints out an update for all of the threads.
        /// </summary>
        private void PrintUpdate()
        {
            Console.WriteLine($"----- Update #: {_programUpdateTimes++} -----");
            _threadStarter?.TotalBenchmarks.Update();

            Console.WriteLine("Total Elapsed Time: " + _simulationsTimer.Elapsed.ToString());
            Console.WriteLine($"Total Threads: " + _threadStarter?.Threads.ToString());
            Console.WriteLine($"Estimated Total Games Played: " + _threadStarter?.TotalBenchmarks.TotalGamesPerSecond() * _simulationsTimer.Elapsed.TotalSeconds);
            Console.WriteLine($"Total Games Per Second: " + _threadStarter?.TotalBenchmarks.TotalGamesPerSecond().ToString());
            Console.WriteLine($"Overall Thread G/s Mean: {_threadStarter?.TotalBenchmarks.OverallMean}");
            Console.WriteLine($"Overall Thread G/s Standard Deviation: {_threadStarter?.TotalBenchmarks.OverallStandardDeviation}");
        }

        /// <summary>
        /// Prints start notes regarding how to use, and what some things mean.
        /// </summary>
        public override void PrintNotes()
        {
            Console.WriteLine($"----- Notes -----");

            Console.WriteLine("Games before data backup means how many games will be played before a thread backs up the data & updates it's stats.");
            Console.WriteLine("A thread is a process of the code running. If you don't know what this means, put in the recommended.");
            Console.WriteLine("Press 'X' or Escape while running to end.");
            Console.WriteLine("Press 'Enter' while running to get an update & program stats.");
        }

        /// <summary>
        /// Printing out the final exit message. Things outputted include: efficiency, total games, total time, threads, etc.
        /// </summary>
        public override void PrintExit()
        {
            Console.WriteLine($"----- Final Assessment -----");
            _threadStarter?.TotalBenchmarks.Update();

            Console.WriteLine("Total Elapsed Time: " + _simulationsTimer.Elapsed.ToString());
            Console.WriteLine($"Total Threads Used: " + _threadStarter?.Threads.ToString());
            Console.WriteLine($"Estimated Total Games Played: " + _threadStarter?.TotalBenchmarks.TotalGamesPerSecond() * _simulationsTimer.Elapsed.TotalSeconds);
            Console.WriteLine($"Total Games Per Second: " + _threadStarter?.TotalBenchmarks.TotalGamesPerSecond().ToString());
            Console.WriteLine($"Overall Thread G/s Mean: {_threadStarter?.TotalBenchmarks.OverallMean}");
            Console.WriteLine($"Overall Thread G/s Standard Deviation: {_threadStarter?.TotalBenchmarks.OverallStandardDeviation}");
        }

        /// <summary>
        /// The name meant to displayed to the console.
        /// </summary>
        /// <returns>Presentation string</returns>
        /// <seealso cref="ProgramSection.PresentationName"/>
        public override string PresentationName()
        {
            return _presentationName;
        }
    }
}
