using System;
using System.Configuration;

namespace KeyGameModel
{
    public static class GameConfigReader
    {
        /// <summary>
        /// Read the configuration file and load the game configurations
        /// </summary>
        /// <param name="configFilePath">by default, keyboardgame.config per specification</param>
        /// <returns>An instance of GameConfiguration that contains the loaded values</returns>
        public static GameConfiguration GetGameConfig(string configFilePath)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configFilePath);

            foreach (ConfigurationSection section in config.Sections)
            {
                if (section is GameConfiguration)
                {
                    return (GameConfiguration)section;
                }
            }

            return null;
        }

        /// <summary>
        /// Creates and saves a game configuration file given an instance of GameConfiguration
        /// </summary>
        /// <param name="configFilePath">Configuration file to save to</param>
        /// <param name="configSection">Populated configuration to be saved to file</param>
        public static void CreateGameConfig(string configFilePath, GameConfiguration configSection)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(configFilePath);
            config.Sections.Add("GameConfig", configSection);
            config.Save();
        }
    }
}
