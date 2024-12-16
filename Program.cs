using HiLoSimulations.Programs;

namespace HiLoSimulations
{
    /// <summary>
    /// Main class for whole program, launching point for everything.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main function. This is where the program starts.
        /// </summary>
        /// <param name="args">The arguements passed in when the .exe is ran.</param>
        static void Main(string[] args)
        {
            Console.WriteLine($"----- Main Thread Starting -----");

            ProgramSection? programSection = ConfigManager.StartMode(Utils.ConfigFilePath);
            
            Console.WriteLine("Reading Config file. . .");
            if (programSection == null)
            {
                Console.WriteLine("Config File start mode not found.");
                Console.WriteLine("Plase fix your config file.");
                Console.WriteLine("You can copy/paste a fresh config file from the README.md if you need to.");
                Console.WriteLine("For more information, please refer to the README.md file.");
            } else
            {
                Console.WriteLine($"Start program found: {programSection.PresentationName()}");
                Console.WriteLine($"Running Found Program. . .");
                Thread.Sleep(500);
                Console.WriteLine($"Program Started!");
                Console.WriteLine($"----- Main Program Running -----\n\n");
                Thread.Sleep(500);

                programSection.RunProgram();
            }
        }
    }
}