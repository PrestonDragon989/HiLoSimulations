using HiLoSimulations.HiLow.Decks;
using HiLoSimulations.Logics;
using HiLoSimulations.Programs;

namespace HiLoSimulations
{
    /// <summary>
    /// The class that holds many static variables and functions. Made to stand as a universal 
    /// </summary>
    public static class Utils
    {
        // Config File Path
        public readonly static string ConfigFilePath = "config.ini";

        // Re Usable CLasses
        public readonly static Random random = new();

        // Constants
        public readonly static int HIGHER = 0;
        public readonly static int LOWER = 1;
        public readonly static int TIE = 2;

        public readonly static int WIN = 3;
        public readonly static int LOSE = 4;

        // Defaults for config failure
        public readonly static ProgramSection DefaultProgram = new Simulations();
        public readonly static Deck DefaultDeck = new ArrayDeck();
        public readonly static Logic DefaultLogic = new PlaceholderLogic();
        public readonly static int DefaultThreads = 1;
        public readonly static int DefaultGameBackupAmount = 1000;

        // Names
        public readonly static string SimulationsName = "Simulations";

        public readonly static string ArrayDeckName = "ArrayDeck";
        public readonly static string QueueDeckName = "QueueDeck";

        public readonly static string PlaceholderLogicName = "PlaceholderLogic";

        /// <summary>
        /// Clears a certain amount of lines from the console.
        /// </summary>
        /// <param name="linesToClear">Amount of lines to clear.</param>
        public static void ClearLines(int linesToClear)
        {
            // Get the current cursor position
            int currentTop = Console.CursorTop;
            int currentLeft = Console.CursorLeft;

            // Setting New Position
            int newTop = Math.Max(currentTop - linesToClear, 0);
            Console.SetCursorPosition(0, newTop);

            // Clear from the current position down to the new position
            for (int i = currentTop; i > newTop; i--)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }

            // Move the cursor back to its original position
            Console.SetCursorPosition(currentLeft, currentTop);
        }

        /// <summary>
        /// Gets a parsed int from the console, with certain restrictions.
        /// </summary>
        /// <param name="startMessage">The first message presented to the console, asking for the integer.</param>
        /// <param name="tryAgainMessage">The message said after a failure to present a correct integer.</param>
        /// <param name="canBeNegative">Rule that states whether a negative integer is accepted.</param>
        /// <param name="canBeZero">Rule that states whether a 0 can be taken as an answer.</param>
        /// <returns>An integer that follows the given rules, taken from the console/user.</returns>
        public static int GetParsedInt(string startMessage, string tryAgainMessage, bool canBeNegative = false, bool canBeZero = false)
        {
            Console.Write(startMessage);
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    if (!canBeZero)
                    {
                        if (number > 0)
                        {
                            return number;
                        }
                    } else if (!canBeNegative)
                    {
                        if (number >= 0)
                        {
                            return number;
                        }
                    } else
                    {
                        return number;
                    }
                }
                else
                {
                    Console.Write(tryAgainMessage);
                }
            }
        }

        /// <summary>
        /// Tries to get a program section by a string name.
        /// </summary>
        /// <param name="name">The name of the program section.</param>
        /// <returns>Either a program section, or null.</returns>
        public static ProgramSection? GetSectionFromName(string name)
        {
            if (name == Utils.SimulationsName)
            {
                return new Simulations();
            }

            return null;
        }

        /// <summary>
        /// Gets a deck based on the name.
        /// </summary>
        /// <param name="name">Name of deck.</param>
        /// <returns>Deck from name, or null.</returns>
        public static Deck? GetDeckFromName(string name)
        {
            if (name == Utils.ArrayDeckName)
            {
                return new ArrayDeck();
            } else if (name == Utils.QueueDeckName)
            {
                return new QueueDeck();
            }

            return null;
        }
    
        /// <summary>
        /// Gets a logic/strategy by the name.
        /// </summary>
        /// <param name="name">Name of logic.</param>
        /// <returns>Logic by name, or null.</returns>
        public static Logic? GetLogicFromName(string name)
        {
            if (name == Utils.PlaceholderLogicName)
            {
                return new PlaceholderLogic();
            }

            return null;
        }
    }
}
