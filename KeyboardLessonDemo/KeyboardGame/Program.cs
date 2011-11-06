using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KeyGameModel;


namespace KeyboardGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameConfiguration config = new GameConfiguration();
            Level level = new Level(".\\Levels\\TestExercise1.exercise");

            GameController controller = new GameController(config, level);
 
            controller.Run();
        }
    }
}
