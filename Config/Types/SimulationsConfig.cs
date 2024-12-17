using BenchmarkDotNet.Loggers;
using HiLoSimulations.HiLow.Decks;
using HiLoSimulations.Logics;
using HiLoSimulations.Programs;
using IniParser.Model;

namespace HiLoSimulations.Config.Types
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BaseConfig"/>
    /// <seealso cref="ProgramSection"/>
    public class SimulationsConfig : BaseConfig
    {
        private readonly KeyDataCollection _profileData;

        private ProgramSection _type = Utils.DefaultProgram;
        private string _name = BaseConfig.NameNotFoundDefaultName;

        private int? _threads;
        private int? _backupAmount;
        private Deck? _deck;
        private Logic? _logic;

        public SimulationsConfig(KeyDataCollection profileData)
        {
            _profileData = profileData;
        }

        /// <summary>
        /// Sets up all of the variables, getting them from the data.
        /// It will return an error code if something goes wrong, but if everything goes right it will send a
        /// All clear code.
        /// </summary>
        /// <returns>The end code (Error, or Safe)</returns>
        public override int Setup()
        {
            // Getting Program Section / Type
            try
            {
                string typeName = _profileData["Type"].ToString() ?? "NONE";
                if (typeName == "None")
                {
                    return BaseConfig.TypeNotFound;
                }
                ProgramSection? maybeType = Utils.GetSectionFromName(typeName);
                
                if (maybeType == null)
                {
                    return BaseConfig.TypeNotFound;
                }
                _type = maybeType;
            }
            catch
            {
                return BaseConfig.TypeNotFound;
            }

            // Getting Name
            try
            {
                _name = _profileData["Name"].ToString() ?? BaseConfig.NameNotFoundDefaultName;
            } catch
            {
                return BaseConfig.NameNotFound;
            }

            // Getting the type specific config
            try
            {
                // Threads and games before backup (both integers)
                string? threadsString = _profileData["Threads"].ToString();
                string? backupGamesString = _profileData["GameBackupAmount"].ToString();
                if (threadsString == null || backupGamesString == null || !int.TryParse(threadsString, out int threadInt) || !int.TryParse(backupGamesString, out int backupGamesInt))
                {
                    return BaseConfig.TypeSpecificError;
                }

                _threads = int.Parse(threadsString);
                _backupAmount = int.Parse(backupGamesString);

                // Deck and Logic
                Deck? deckParse = Utils.GetDeckFromName(_profileData["Deck"].ToString() ?? "N/A");
                Logic? logicParse = Utils.GetLogicFromName(_profileData["Logic"].ToString() ?? "N/A");

                if (deckParse == null || logicParse == null)
                {
                    return BaseConfig.TypeSpecificError;
                }

                _deck = deckParse;
                _logic = logicParse;

            } catch
            {
                return BaseConfig.TypeSpecificError;
            }

            return BaseConfig.SetupComplete;
        }

        public override ProgramSection GetProgram()
        {
            Simulations simulations = new();
            simulations.SetInputs(Threads, GamesBeforeBackup);
            return simulations;
        }

        /// <summary>
        /// Getters for all of the important things (Name, Type, Threads, Backup amount, Deck, & Logic, Data)
        /// </summary>
        public override ProgramSection Type => _type;
        public override string Name => _name.ToString();
        public Logic Logic { get { return _logic ?? Utils.DefaultLogic; } }
        public Deck Deck { get { return _deck ?? Utils.DefaultDeck; } }
        public int Threads { get { return _threads ?? 0; } }
        public int GamesBeforeBackup { get { return _backupAmount ?? 0; } }
    }
}
