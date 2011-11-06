using System;
using System.Collections.Generic;
using System.Text;

namespace KeyGameModel
{
    /// <summary>
    /// A mock version of the game model used for testing purposes.
    /// It currently runs through a hardcoded level "one two three".
    /// 
    /// For simplicity, it also takes any character in a seqence as correct.
    /// Eg. typing "nnn" will be taken as correct for "one" but "nnm" would be 
    /// incorrect
    /// 
    /// </summary>
    public class MockGameModel : BaseGameModel
    {
         #region Private Members

        private GameConfiguration _configuration = null;
        private Level _level;
        private string[] _sequences = new string[] { "one", "two", "three" };
        private int _currentSequenceIndex;
        private int _currentSequenceCharIndex;
        private int _baseScore;

        #endregion Private Members

        public MockGameModel(GameConfiguration configuration, Level level)
        {
            _configuration = configuration;
            _level = level;
            _currentSequenceIndex = -1;
            _baseScore = 0;
            _currentSequenceCharIndex = -1;
        }

        /// <summary>
        /// Take the user character input and the time (in milliseconds) used
        /// </summary>
        /// <param name="input">input character from View, using String type to accommodate for special characters</param>
        /// <param name="milliSeconds">time the user took to enter the character</param>
        /// Proposal: if first character in sequence, then timer starts right after countdown "3, 2, 1, 0" finishes
        /// if subsequent character, use the timespan between last and this key press.
        /// 
        /// <returns>return the currect state, [Flags] allow for scenarios such as Correct|EndOfLevel, Incorrect|EndOfSequence, etc.</returns>
        public override GameModel.GameState UserInput(String input, int milliSeconds)
        {

            GameModel.GameState returnFlags;

            //if we already are past the end of all the sequences, we are done
            if (_currentSequenceIndex >= _sequences.Length) 
            {
                returnFlags = GameModel.GameState.EndOfLevel;
                return returnFlags;
            }

            _currentSequenceCharIndex++;

            if (_sequences[_currentSequenceIndex].Contains(input) 
                && _currentSequenceCharIndex < _sequences[_currentSequenceIndex].Length)
            {
                _baseScore++;
                returnFlags = GameModel.GameState.Correct;
            }
            else
            {
                returnFlags = GameModel.GameState.Incorrect;
                _currentSequenceCharIndex = _sequences[_currentSequenceIndex].Length;
            }

            if (_currentSequenceCharIndex +1 >= _sequences[_currentSequenceIndex].Length)
            {
                returnFlags = returnFlags | GameModel.GameState.EndOfSequence;

                if (_currentSequenceIndex +1 >= _sequences.Length)
                {
                    returnFlags = returnFlags | GameModel.GameState.EndOfLevel;
                }
            }



            return returnFlags;
        }

        /// <summary>
        /// view would ask for next sequence at the beginning and every time it encounters EndOfSequence state
        /// </summary>
        public override LevelSequence NextSequence
        {
            get
            {
                 _currentSequenceIndex++;
                _currentSequenceCharIndex = -1;
                return CurrentSequence;
            }
        }

        /// <summary>
        /// Gets the current sequence being worked on
        /// </summary>
        public override LevelSequence CurrentSequence
        {
            get
            {
                return new LevelSequence(_sequences[_currentSequenceIndex]);
            }
        }

        public override int TotalScore
        {
            get
            {
                return BaseScore + SpeedBonus + ChainBonus;
            }
        }

        public override int BaseScore
        {
            get
            {
                return _baseScore;
            }
        }

        public override int SpeedBonus
        {
            get
            {
                return 0;
            }
        }

        public override int ChainBonus
        {
            get
            {
                return 0;
            }
        }

    }
}
