namespace HiLoSimulations.Benchmarks
{
    /// <summary>
    /// The overall controller for all benchmarks, that monitors every thread's benchmarks.
    /// </summary>
    public class OverallControllerBenchmarks
    {
        private readonly ControllerBenchmarks[] _controllerBenchmarks;

        private double _overallMean;
        private double _overallStandardDeviation;

        public OverallControllerBenchmarks(int amountOfControllers)
        {
            _controllerBenchmarks = new ControllerBenchmarks[amountOfControllers];
        }

        /// <summary>
        /// Updates the whole class. Gets the means for the G/s standard deviations & the G/s mean.
        /// </summary>
        public void Update()
        {
            double totalStandardDeviations = 0;
            double totalMeans = 0;
            foreach (var benchmark in _controllerBenchmarks)
            {
                if (benchmark != null)
                {
                    totalMeans += benchmark.Mean();
                    totalStandardDeviations += benchmark.StandardDeviation();
                }
            }

            _overallMean = totalMeans / (double)_controllerBenchmarks.Length;
            _overallStandardDeviation = totalStandardDeviations / (double)_controllerBenchmarks.Length;
        }

        /// <summary>
        /// Marks a thread's benchmark in the class/system to be used in the update.
        /// </summary>
        /// <param name="thread">The custom thread ID</param>
        /// <param name="controllerBenchmark">The thread's controller benchmark</param>
        public void Mark(int thread, ControllerBenchmarks controllerBenchmark)
        {
            _controllerBenchmarks[thread] = controllerBenchmark;
        }

        /// <summary>
        /// Gets totaled games per second, to see what all the G/s are added up.
        /// </summary>
        /// <returns>Double of total Games per Second.</returns>
        public double TotalGamesPerSecond()
        {
            return _controllerBenchmarks
            .Select(b => b != null ? Math.Max(b.Mean(), 0) : 0)
            .Sum();
        }

        /// <summary>
        /// Getter for the overall G/s mean.
        /// </summary>
        public double OverallMean { get { return _overallMean; } }
        
        /// <summary>
        /// Getter for the overall G/s standard deviation.
        /// </summary>
        public double OverallStandardDeviation { get { return _overallStandardDeviation; } }
    }
}
