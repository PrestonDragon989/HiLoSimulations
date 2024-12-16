using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace HiLoSimulations.Benchmarks
{
    /// <summary>
    /// Class to monitor a controller's benchmarks & efficiency. 
    /// </summary>
    public class ControllerBenchmarks
    {
        private readonly Stopwatch _stopwatch = new();
        private readonly List<double> _gamesPerSecond = new();

        private readonly int _gamesPerMark;

        public ControllerBenchmarks(int gamesPerMark)
        {
            _gamesPerMark = gamesPerMark;
        }

        /// <summary>
        /// Function that is called when the controller checks in.
        /// Calculates games per second based off of games passed divided by seconds passed.
        /// </summary>
        public void Mark()
        {
            _gamesPerSecond.Add((double)_gamesPerMark / (_stopwatch.ElapsedMilliseconds / 1000d));
        }

        /// <summary>
        /// Stops the benchmark stopwatch.
        /// </summary>
        public void StopTimer()
        {
            _stopwatch.Stop();
        }

        /// <summary>
        /// Starts the benchmark stopwatch.
        /// </summary>
        public void StartTimer()
        {
            _stopwatch.Start();
        }

        /// <summary>
        /// Resets the benchmark stopwatch.
        /// </summary>
        public void ResetTimer()
        {
            _stopwatch.Restart();
        }

        /// <summary>
        /// The standard deviation of the games per second so far.
        /// </summary>
        /// <returns>Standard Deviation of past games per second.</returns>
        public double StandardDeviation()
        {
            var squaredDifferences = _gamesPerSecond.Select(x => Math.Pow(x - _gamesPerSecond.Average(), 2)).ToList();
            var averageSquaredDifference = squaredDifferences.Average();

            return (long)Math.Sqrt(averageSquaredDifference);
            
        }

        /// <summary>
        /// The mean of the games per second so far.
        /// </summary>
        /// <returns>Mean of games per second.</returns>
        public double Mean()
        {
            return _gamesPerSecond.Sum() / _gamesPerSecond.Count;
        }

        /// <summary>
        /// The list of the Games per second history.
        /// </summary>
        /// <returns>Games per second history list.</returns>
        public List<double> GetGamesPerSecondHistory() => _gamesPerSecond;
    }
}