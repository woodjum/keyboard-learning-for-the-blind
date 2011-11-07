using System;
using System.Collections.Generic;

namespace KeyGameModel
{
    public class GameModel : BaseGameModel
    {


        #region Private Members

        private GameConfiguration _configuration = null;
        private Level _level;
        private LevelSequence _currentSequence;

        private double _currentChainScore;
        private int _currentChainLength;
        private int _currentSequenceSpeedBonus;

        private double _baseScoreRunningTotal;
        private double _speedBonusRunningTotal;
        private double _chainBonusRunningTotal;

        #endregion Private Members

        public GameModel(GameConfiguration configuration, Level level)
        {
            _configuration = configuration;
            _level = level;
            _baseScoreRunningTotal = 0;
            _speedBonusRunningTotal = 0;
            _chainBonusRunningTotal = 0;
            _currentChainScore = 0;
            _currentChainLength = 0;
            _currentSequenceSpeedBonus = 0;
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
        public override GameState UserInput(String input, int milliSeconds)
        {
            GameState returnFlags;
            if (_currentSequence != null && !String.IsNullOrEmpty(_currentSequence.CurrentCharacter))
            {
                if (input == _currentSequence.CurrentCharacter)
                {
                    //Actions to take if user input is correct
                    _currentSequenceSpeedBonus += Math.Min(_configuration.MaxSpeedBonusPerLetter, Math.Max(0, _configuration.ExpectedMsPerLetter - milliSeconds));
                    returnFlags = GameState.Correct;

                    if (_currentSequence.EndOfSequence)
                    {
                        double baseScore = _currentSequence.GetFullBaseScore(_configuration);
                        _currentChainLength++;
                        _currentChainScore += baseScore + _currentSequenceSpeedBonus;

                        _baseScoreRunningTotal += baseScore;
                        _speedBonusRunningTotal += _currentSequenceSpeedBonus;

                        _currentSequenceSpeedBonus = 0;

                        returnFlags = returnFlags | GameState.EndOfSequence;
                    }
                    else
                    {
                        _currentSequence.AdvanceToNextCharacter();
                    }

                    if (_level.EndOfLevel)
                    {
                        _chainBonusRunningTotal += ApplyChainBonus(_currentChainScore, _currentChainLength);
                        _currentChainScore = 0;
                        _currentChainLength = 0;
                        returnFlags = returnFlags | GameState.EndOfLevel;
                    }
                }
                else
                {
                    //Actions to take if user input is incorrect
                    _chainBonusRunningTotal += ApplyChainBonus(_currentChainScore, _currentChainLength);
                    _currentChainScore = 0;
                    _currentChainLength = 0;
                    _currentSequenceSpeedBonus = 0;

                    returnFlags = GameState.Incorrect | GameState.EndOfSequence;

                    if (_level.EndOfLevel)
                    {
                        returnFlags = returnFlags | GameState.EndOfLevel;
                    }
                }
            }
            else
            {
                returnFlags = GameState.InvalidGameState;
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
                do
                {
                    _currentSequence = _level.NextSequence;
                } while (_currentSequence.SequenceLength > _configuration.MaxSequenceLength);
                _currentSequenceSpeedBonus = 0;
                return _currentSequence.Clone();
            }
        }

        /// <summary>
        /// Gets the current sequence being worked on
        /// </summary>
        public override LevelSequence CurrentSequence
        {
            get
            {
                return _currentSequence.Clone();
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
                return (int)_baseScoreRunningTotal;
            }
        }

        public override int SpeedBonus
        {
            get
            {
                return (int)_speedBonusRunningTotal;
            }
        }

        public override int ChainBonus
        {
            get
            {
                return (int)(_chainBonusRunningTotal + ApplyChainBonus(_currentChainScore, _currentChainLength));
            }
        }

        private double ApplyChainBonus(double chainScore, int chainLength)
        {
            return chainScore * Math.Min(_configuration.ChainingBonusCap, (double)(chainLength - 1) * _configuration.ChainingBonus);
        }
    }
}