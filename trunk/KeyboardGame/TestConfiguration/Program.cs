using System;
using System.Collections.Generic;
using System.Text;
using KeyGameModel;

namespace TestConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            GameConfiguration config = new KeyGameModel.GameConfiguration();
            config.ChainingBonus = 0.05;

            GameConfigReader.CreateGameConfig(@".\TestConfiguration.exe", config);
        }
    }
}
