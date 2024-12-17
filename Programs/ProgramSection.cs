using HiLoSimulations.Config.Types;

namespace HiLoSimulations.Programs
{
    /// <summary>
    /// A sub section of program. Which one used is detailed by the config file. They each do different things,
    /// like running the threads for the data, analyzing the data, etc.
    /// </summary>
    public abstract class ProgramSection
    {
        /// <summary>
        /// The start notes when a program is first ran.
        /// </summary>
        public abstract void PrintNotes();

        /// <summary>
        /// Final words when a program ends.
        /// </summary>
        public abstract void PrintExit();

        /// <summary>
        /// The program section's equivalent to 'Main'
        /// </summary>
        /// <param name="profile">The data needed to run the type of program.</param>
        public abstract void RunProgram();

        /// <summary>
        /// The name meant to be seen in the console.
        /// </summary>
        /// <returns>Display name string</returns>
        public abstract string PresentationName();
    }
}