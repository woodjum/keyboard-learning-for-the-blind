using System;
using System.Collections.Generic;
using System.Text;

namespace KeyGameModel
{
    public abstract class BaseGameModel
    {
        /// <summary>
        /// enum to keep track of current Game state, may replace with Events
        /// </summary>
        [Flags]
        public enum GameState { Correct = 0x01, Incorrect= 0x00, EndOfSequence = 0x02, EndOfLevel =0x04, InvalidGameState = 0x08 }

        /// <summary>
        /// Take the user character input and the time (in milliseconds) used
        /// </summary>
        /// <param name="input">input character from View, using String type to accommodate for special characters</param>
        /// <param name="milliSeconds">time the user took to enter the character</param>
        /// Proposal: if first character in sequence, then timer starts right after countdown "3, 2, 1, 0" finishes
        /// if subsequent character, use the timespan between last and this key press.
        /// 
        /// <returns>return the currect state, [Flags] allow for scenarios such as Correct|EndOfLevel, Incorrect|EndOfSequence, etc.</returns>
        public abstract GameState UserInput(String input, int milliSeconds);
        /// <summary>
        /// view would ask for next sequence at the beginning and every time it encounters EndOfSequence state
        /// </summary>
        public virtual LevelSequence NextSequence{get;set;}

        /// <summary>
        /// Gets the current sequence being worked on
        /// </summary>
        public virtual LevelSequence CurrentSequence { get; set; }

        public virtual int TotalScore { get; set; }

        public virtual int BaseScore { get; set; }

        public virtual int SpeedBonus { get; set; }

        public virtual int ChainBonus { get; set; }
    }
}
