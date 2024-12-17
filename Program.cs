using HiLoSimulations.Config;
using HiLoSimulations.Config.Types;
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
        /// <remarks>You can add the 'string[] args' parameter back to receive arguments.</remarks>
        static void Main()
        {
            Console.WriteLine($"----- Main Thread Starting -----");
            Console.WriteLine("Any questions? Hers the wiki: https://github.com/PrestonDragon989/HiLoSimulations/wiki");

            // Getting Profiles from the config file
            ConfigManager configManager = new(Utils.ConfigFilePath);
            
            BaseConfig?[] profiles = configManager.CollectProfiles();

            // Printing out the profile options
            Console.WriteLine($"Profiles found in the {Utils.ConfigFilePath} config file.");
            for (int i = 0;  i < profiles.Length; i++)
            {
                BaseConfig? profile = profiles[i];
                if (profile != null)
                {
                    Console.WriteLine($"\t{i}. {profile.Name}");
                } else
                {
                    Console.WriteLine($"\t{i}. CONFIG PROFILE ERROR");
                }
            }
            
            // Getting Selected Profile
            BaseConfig? selectedProfile = null;
            do
            {
                Console.Write("Please enter the number of the profile you want: ");
                string enteredNumber = (Console.ReadLine() ?? "0").ToString().Replace(" ", "");
                if (int.TryParse(enteredNumber, out int result))
                {
                    if (result >= 0 && result < profiles.Length)
                    {
                        if (profiles[result] == null)
                        {
                            Console.WriteLine("The profile selected is invalid. Please choose another, or close the program.");
                        } else
                        {
                            selectedProfile = profiles[result];
                        }
                    } else
                    {
                        Console.Write("Number can't be out of range. ");
                    }
                } else
                {
                    Console.Write("Input was invalid. Put and integer. ");
                }
            } while (selectedProfile == null);

            // Getting program, & Running the selected profile/program
            Console.WriteLine($"\n----- Selected Profile: {selectedProfile.Name} -----");

            ProgramSection programSection = selectedProfile.GetProgram();
            programSection.RunProgram();

            // Exiting
            Console.WriteLine($"----- Program Ended -----");
            Console.Write("Press any key to exit. . .");
            Console.ReadKey();
        }
    }
}