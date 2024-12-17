using HiLoSimulations.Programs;
using IniParser.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.AccessControl;

namespace HiLoSimulations.Config.Types
{
    /// <summary>
    /// The abstract class config. This is what each section of the config file will represent. 
    /// </summary>
    public abstract class BaseConfig
    {
        /// <summary>
        /// A list of the codes 'Setup' can return.
        /// </summary>
        public static readonly int SetupComplete     = 0;
        public static readonly int TypeNotFound      = -1;
        public static readonly int NameNotFound      = -2;
        public static readonly int TypeSpecificError = -3;

        /// <summary>
        /// Fill in defaults.
        /// </summary>
        public static readonly string NameNotFoundDefaultName = "NAME NOT FOUND";

        /// <summary>
        /// Getting the type of program to run.
        /// </summary>
        public abstract ProgramSection? Type { get; }

        /// <summary>
        /// The 'name' of the profile. This is what displays to the console.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Set up the whole config. Parses the given data, and checks everything.
        /// </summary>
        /// <returns>Exit code. (Specified in the BaseConfig class.)</returns>
        public abstract int Setup();

        /// <summary>
        /// Getting the complete full program.
        /// </summary>
        /// <returns>Complete program to run.</returns>
        public abstract ProgramSection GetProgram();
    }
}
