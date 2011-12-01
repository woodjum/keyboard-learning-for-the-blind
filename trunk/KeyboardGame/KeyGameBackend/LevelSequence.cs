using System;
using System.Collections.Generic;
using System.Text;
using ExerciseGenerator;

namespace KeyGameModel
{
    public class LevelSequence
    {
        private List<String> _sequence;
        private int _currentIndex;

        public int SequenceLength
        {
            get
            {
                return _sequence.Count;
            }
        }

        public List<String> Sequence
        {
            get
            {
                return _sequence;
            }
        }


        public bool EndOfSequence
        {
            get
            {
                return _currentIndex >= _sequence.Count - 1;
            }
        }

        public int CurrentPlaceInSequence
        {
            get
            {
                return _currentIndex;
            }
        }

        public String CurrentCharacter
        {
            get
            {
                if (_currentIndex < _sequence.Count)
                {
                    return _sequence[_currentIndex];
                }
                else
                {
                    return null;
                }
            }
        }

        public String NextCharacter
        {
            get
            {
                if (_currentIndex >= _sequence.Count - 1)
                {
                    return null;
                }
                else
                {
                    return _sequence[++_currentIndex];
                }
            }
        }

        public void AdvanceToNextCharacter()
        {
            _currentIndex++;
        }

        public LevelSequence() { }

        public LevelSequence(String sequence)
        {
            ValidCharInputReader sequenceReader = new ValidCharInputReader(sequence);
            _sequence = new List<String>();

            String ch = null;
            while (!String.IsNullOrEmpty(ch = sequenceReader.ReadNextCharacter()))
            {
                _sequence.Add(ch);
            }

            _currentIndex = 0;
        }

        public double GetFullBaseScore(GameConfiguration config)
        {
            return (this.SequenceLength * config.BasePointsPerLetter) * (1.0 + config.AdditionalLetterBonus * (this.SequenceLength - 1));
        }

        public LevelSequence Clone()
        {
            return new LevelSequence() { _sequence = new List<string>(this._sequence), _currentIndex = 0 };
        }

        public string ToString()
        {
            if (_sequence == null)
            {
                return null;
            }

            string tempSeqence = "";
            foreach (string current in _sequence)
            {
                tempSeqence += current;
            }
            return tempSeqence;
        }
    }
}