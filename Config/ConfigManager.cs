using HiLoSimulations.Config.Types;
using HiLoSimulations.HiLow.Decks;
using HiLoSimulations.Logics;
using HiLoSimulations.Programs;
using IniParser;
using IniParser.Model;

namespace HiLoSimulations.Config
{
    /// <summary>
    /// The class that manages the given config, it collects things like profiles, and start protocols/programs. 
    /// </summary>
    public class ConfigManager
    {
        private readonly FileIniDataParser _parser = new();

        private readonly IniData _allData;
        private BaseConfig?[]? _profiles;

        public ConfigManager(string fileName)
        {
            try
            {
                _allData = _parser.ReadFile(fileName);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"Config file: {fileName} not found! Error: {ex.Message}");
            } catch (IOException ex)
            {
                throw new IOException($"Config File: {fileName} I/O Error! Error: {ex.Message}");
            } catch (Exception ex)
            {
                throw new Exception($"Config file: {fileName} random error! Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Collect all of the profiles from the given config.
        /// </summary>
        /// <returns>Array of the gotten configs.</returns>
        public BaseConfig?[] CollectProfiles()
        {
            _profiles = new BaseConfig[_allData.Sections.Count];
            // Iterating through each section of all data
            for (int i = 0; i < _allData.Sections.Count; i++)
            {
                KeyDataCollection sData = _allData.Sections.ElementAt(i).Keys;

                string sectionType = sData["Type"];
                if (sectionType == Utils.SimulationsName)
                {
                    SimulationsConfig config = new(sData);
                    config.Setup();
                    _profiles[i] = config;
                } else
                {
                    _profiles[i] = null;
                }
            }
            return _profiles;
        }
    }
}
