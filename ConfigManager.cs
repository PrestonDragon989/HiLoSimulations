using HiLoSimulations.HiLow.Decks;
using HiLoSimulations.Logics;
using HiLoSimulations.Programs;
using IniParser;
using IniParser.Model;

namespace HiLoSimulations
{
    /// <summary>
    /// The class that manages the given config, it collects things like profiles, and start protocols/programs. 
    /// </summary>
    public class ConfigManager
    {
        private readonly FileIniDataParser _parser = new();

        private readonly IniData? _data;
        private readonly string _configProfileName;

        public ConfigManager(string fileName, string configProfileName)
        {
            _configProfileName = configProfileName;
            try
            {
                _data = _parser.ReadFile(fileName);
            }
            catch (Exception ex) 
            {
                _data = null;
                Console.WriteLine($"File Parser Error | Parsing File '{fileName}' | Error: {ex}");
            }
        }

        /// <summary>
        /// States whether a valid config was collected.
        /// </summary>
        /// <returns>Whether the data is null, or not.</returns>
        public bool ConfigReceived()
        {
            return _data != null;
        }

        /// <summary>
        /// Collects the logic taken from the config's profile.
        /// </summary>
        /// <returns>Either the given logic from the profile, or the default logic.</returns>
        public Logic Logic()
        {
            if (_data == null)
            {
                return Utils.DefaultLogic;
            }
            string rawDeckType = _data[_configProfileName]["Logic"];
            if (rawDeckType == "PlaceholderLogic")
            {
                return new PlaceholderLogic();
            }

            return Utils.DefaultLogic;
        }

        /// <summary>
        /// The given deck from the config's profile.
        /// </summary>
        /// <returns>Either the given deck, or the default deck.</returns>
        public Deck Deck()
        {
            if (_data == null )
            {
                return Utils.DefaultDeck;
            }
            string rawDeckType = _data[_configProfileName]["Deck"];
            if (rawDeckType == "ArrayDeck")
            {
                return new ArrayDeck();
            } else if (rawDeckType == "QueueDeck")
            {
                return new QueueDeck();
            }

            return Utils.DefaultDeck;
        }

        /// <summary>
        /// The amount of threads specified by the config's profile.
        /// </summary>
        /// <returns>integer of amount of threads, or default.</returns>
        public int Threads()
        {
            if ( _data == null )
            {
                return Utils.DefaultThreads;
            }
            return int.Parse(_data[_configProfileName]["Threads"]);
        }
        
        /// <summary>
        /// The amount of games played before a thread backs it's history up, as specified by the config's profile.
        /// </summary>
        /// <returns>Integer of amount of games before backup.</returns>
        public int GameBackupAmount()
        {
            if ( _data == null )
            {
                return Utils.DefaultGameBackupAmount;
            }
            return int.Parse(_data[_configProfileName]["GameBackupAmount"]);
        }

        /// <summary>
        /// Prints out all of the config's simulation profiles to the console.
        /// </summary>
        /// <param name="configPath">Path to the used config file. (.ini)</param>
        public static void PrintConfigProfiles(string configPath)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(configPath);

            int ignoredSections = 0;
            Console.WriteLine("Config File Profiles: ");
            for (int i = 0; i < data.Sections.Count; i++)
            {
                string profileName = data.Sections.ElementAt(i).SectionName.ToString();
                if (profileName != "Start")
                {
                    Console.WriteLine($"\t{i + 1 - ignoredSections}. {profileName}");
                } else
                {
                    ignoredSections++;
                }
            }
        }

        /// <summary>
        /// Gets all of the simulation profiles from a given file.
        /// </summary>
        /// <param name="configPath">The config file path. (.ini)</param>
        /// <returns>A list of SectionData, from the FileIniDataParser. The list is of the profiles.</returns>
        public static List<SectionData> GetProfiles(string configPath)
        {
            var parser = new FileIniDataParser();
            var data = parser.ReadFile(configPath);

            List<SectionData> profiles = new();
            foreach (var section in data.Sections)
            {
                if (section.SectionName != "Start")
                {
                    profiles.Add(section);
                }
            }
            return profiles;
        }

        /// <summary>
        /// Gets the program to run, from the Start section of the given config file.
        /// </summary>
        /// <param name="configPath">The file path of the config file used. (.ini)</param>
        /// <returns>The ProgramSection to run, from the config.</returns>
        public static ProgramSection? StartMode(string configPath)
        {
            try
            {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(configPath);
                string startMode = data["Start"]["Mode"];

                if (startMode == "Simulations")
                {
                    return new Simulations();
                }
            } catch (Exception ex)
            {
                Console.WriteLine($"Start Mode Finder Error: {ex}");
            }
            return null;
        }
    }
}
