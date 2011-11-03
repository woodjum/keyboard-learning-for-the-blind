using System;
using System.Collections.Generic;
using System.Text;
using KeyboardGame.Views;
using System.Windows.Forms;
using KeyGameModel;

namespace KeyboardGame
{
    class GameController : TalkingController
    {
        static bool HIDE_PROMPT = true;

        //states of the games
        enum ViewState {Intro, Prompting, Listening , Score};
        ViewState currentViewState;

        //different views
        GameIntroView gameIntroView = new GameIntroView();
        PlayView playview = new PlayView();
        ScoreView scoreView = new ScoreView();

        //timers
        DateTime promptFinish = DateTime.MinValue;
        DateTime lastCorrectKeyPress = DateTime.MaxValue;
        double inputDrainSec = .25;

        //game model
        GameModel model;
        GameConfiguration configuration;
        Level level;

        //stuff to pull from the game model
        //String levelName = "Level 1";
        //String levelDescription = "abcde";

        public GameController(GameConfiguration configuration, Level level)
            : base()
        {
            //intiliazing model
            this.configuration = configuration;
            this.level = level;
            this.model = new GameModel(configuration, level);
           
            
            //attaching input hooks
            keyboardHook.KeyPress += new KeyPressEventHandler(keyboardHook_KeyPress);

            this.talkingWindow.Load += new System.EventHandler(this.TalkingWindow_Load);
            this.talkingWindow.Shown += new System.EventHandler(this.TalkingWindow_Shown);
        }

        private void TalkingWindow_Load(object sender, EventArgs e)
        {
            currentViewState = ViewState.Intro;
            gameIntroView.SetTitle(level.Name);
            gameIntroView.SetDescription(level.Description);
            this.talkingWindow.SetCurrentView(gameIntroView);
            
        }

        private void TalkingWindow_Shown(object sender, EventArgs e)
        {

            this.talkingWindow.Speak(this.level.Name + this.level.Description);
  

        }



        private void StartPlaying(){
            this.talkingWindow.SetCurrentView(playview);
            Prompt();
        }

        private void StartScore()
        {
            this.talkingWindow.SetCurrentView(this.scoreView);
            int score = 113;  //replace with model numbers later
            //int score = this.model.TotalScore;
            //TODO: speak score
            this.scoreView.SetScore(score);
        }

        private void Finish()
        {
            this.talkingWindow.Close();
        }

        private void Prompt()
        {
             string prompt = "AAAAAh"; //pull from model later
            //string prompt = this.model.NextSequence.ToString();
            
            this.currentViewState = ViewState.Prompting;
            this.playview.SetPrompt(prompt);
            this.playview.SetPromptVisibility(true);
            this.playview.ClearInput();

            this.playview.Refresh();

            this.speakKeysPressed = false;

            this.talkingWindow.Speak(prompt, false);

            //TODO: start timer;
            this.promptFinish = DateTime.Now;
            this.lastCorrectKeyPress = this.promptFinish;

            this.currentViewState = ViewState.Listening;
            
            if (HIDE_PROMPT)
            {
                this.playview.SetPromptVisibility(false);
            }
        }

        private void Listening_KeyPress(KeyPressEventArgs e) 
        {
            DateTime pressTime = DateTime.Now;
            if (pressTime.Ticks > this.promptFinish.AddSeconds(this.inputDrainSec).Ticks)
            {
                //update playview with input and speak it
                this.speakKeysPressed = true;
                this.playview.AppendInput(e.KeyChar.ToString());

                //caclulate speed
                TimeSpan differenceFromLastKey = pressTime.Subtract(this.lastCorrectKeyPress);
                this.lastCorrectKeyPress = pressTime;
                
                //pass on input to model
                GameModel.GameState gameState = GameModel.GameState.EndOfLevel; //give to model later
                //GameModel.GameState gameState = this.model.UserInput(e.KeyChar.ToString(), differenceFromLastKey.Milliseconds);

                //TODO: make sounds for correct and incorrect
                
                //Move onto next sequence, or score screen if needed
                //Otherwise it will wait for next input
                if ((gameState & GameModel.GameState.EndOfLevel) == GameModel.GameState.EndOfLevel)
                {
                    //bring up and display score screen
                    StartScore();
                }
                else if ((gameState & GameModel.GameState.EndOfSequence) == GameModel.GameState.EndOfSequence)
                {
                    //Prompt the next sequence
                    Prompt();
                }

                
                
            }
            
        }

 


        void keyboardHook_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (currentViewState)
            {
                case ViewState.Intro:
                    // Any Key Press will move onto the actual game
                    StartPlaying();
                    break;
                case ViewState.Prompting:
                    //ignore input when prompting
                    break;
                case ViewState.Listening:
                    //Handle the key press when listening
                    Listening_KeyPress(e);
                    break;
                case ViewState.Score:
                    // Any Key Press will exit the game
                    break;
                default:
                    break;
            }
        }
    }
}
