using System;
using System.Collections.Generic;
using System.Text;
using KeyboardGame.Views;
using System.IO;
using KeyGameModel;
using System.Windows.Forms;
using System.Threading;

namespace KeyboardGame
{
    class LevelSelectionController : TalkingController
    {
        private const string LevelFolderPath = @".\Levels";
        private const string LevelFilePattern = @"*.exercise";

        private List<Level> levels;

        private LevelSelectionView levelSelectionView = new LevelSelectionView();

        private Thread gameThread;

        public LevelSelectionController()
        {
            this.speakKeysPressed = false;

            levels = new List<Level>();

            this.talkingWindow.Load += new System.EventHandler(this.TalkingWindow_Load);
            this.talkingWindow.Shown += new System.EventHandler(this.TalkingWindow_Shown);
            this.levelSelectionView.GetLevelListBox().SelectedIndexChanged += new EventHandler(LevelSelectionController_SelectedIndexChanged);

            this.keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
        }

        void LevelSelectionController_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpeakSelectedLevel();
        }

        void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {
            // Ignore key presses if the child game thread is active
            if (gameThread != null && gameThread.IsAlive)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Return:
                    RunSelectedLevel();
                    break;
                case Keys.Space:
                    RunSelectedLevel();
                    break;
                default:
                    break;
            }
        }

        private void TalkingWindow_Load(object sender, EventArgs e)
        {
            this.talkingWindow.SetCurrentView(levelSelectionView);
            LoadLevels();
        }

        private void TalkingWindow_Shown(object sender, EventArgs e)
        {
            this.talkingWindow.Speak(levelSelectionView.GetTitle(), false);
        }

        private void LoadLevels()
        {
            // Find and read in the level files
            string[] levelFiles = Directory.GetFiles(LevelFolderPath, LevelFilePattern, SearchOption.TopDirectoryOnly);
            List<string> levelNames = new List<string>();
            foreach(string levelFile in levelFiles)
            {
                Level level = new Level(levelFile);
                levels.Add(level);
                levelNames.Add(level.Name);
            }

            // Display the levels
            this.levelSelectionView.DisplayLevels(levelNames);
        }
        
        private void RunGame(Object data)
        {
            Level level = (Level)data;
            GameConfiguration config = GameConfigReader.GetGameConfig(@".\KeyboardGame.exe");
            GameController controller = new GameController(config, level);
            controller.Run();
        }

        private void RunSelectedLevel()
        {
            int index = levelSelectionView.GetLevelListBox().SelectedIndex;
            Level level = levels[index];

            // Run game on seperate thread
            gameThread = new Thread(new ParameterizedThreadStart(RunGame));
            gameThread.Start(level);
            gameThread.Join();
            levelSelectionView.GetLevelListBox().SelectedIndex = (index + 1) % levels.Count;
        }

        private void SpeakSelectedLevel()
        {
            int index = levelSelectionView.GetLevelListBox().SelectedIndex;
            string levelName = levels[index].Name;
            this.talkingWindow.Speak("玩" + levelName, true);
        }
    }
}
