using System;
using System.Collections.Generic;
using System.Text;

namespace ExerciseGenerator
{
    public class Generator
    {
        /// <remarks>
        /// Special characters:
        /// \l = [capslock] = Caps Lock
        /// \m = [numlock] = Num Lock
        /// \f = [shift] = Shift
        /// \a = [alt] = Alt
        /// \c = [ctrl] = Ctrl
        /// \s = [space] = Space
        /// \t = [tab] = Tab
        /// \b = [backspace] = Backspace
        /// \d = [delete] = Delete
        /// \p = [pagedown] = Page Down
        /// \u = [pageup] = Page Up
        /// \h = [home] = Home
        /// \n = [end] = End
        /// \i = [insert] = Insert
        /// \e = [esc] = [escape] = Escape
        /// \r = [enter] = [return] = Return/Enter
        /// </remarks>
        

        private Random _rand;
        private List<string> _validCharacters;
        
        public string ValidCharacters {
            get
            {
                StringBuilder builder = new StringBuilder();

                foreach (string c in _validCharacters)
                {
                    builder.Append(c);
                }

                return builder.ToString();
            }

            set
            {
                _validCharacters = new List<string>();
                ValidCharInputReader inputReader = new ValidCharInputReader(value);

                string ch;

                while (!string.IsNullOrEmpty(ch = inputReader.ReadNextCharacter()))
                {
                    switch (ch)
                    {
                        case "[capslock]": ch = @"\l"; break;
                        case "[numlock]": ch = @"\m"; break;
                        case "[shift]": ch = @"\f"; break;
                        case "[alt]": ch = @"\a"; break;
                        case "[ctrl": ch = @"\c"; break;
                        case "[space]": ch = @"\s"; break;
                        case "[tab]": ch = @"\t"; break;
                        case "[backspace]": ch = @"\b"; break;
                        case "[delete]": ch = @"\d"; break;
                        case "[pagedown]": ch = @"\p"; break;
                        case "[pageup]": ch = @"\u"; break;
                        case "[home]": ch = @"\h"; break;
                        case "[end]": ch = @"\n"; break;
                        case "[insert]": ch = @"\i"; break;
                        case "[esc]":
                        case "[escape]": ch = @"\e"; break;
                        case "[enter]":
                        case "[return]": ch = @"\r"; break;
                        default: break;
                    }
                    if (ch.StartsWith("["))
                    {
                        throw new ArgumentException("Invalid character detected: " + ch);
                    }
                    if (!_validCharacters.Contains(ch))
                    {
                        _validCharacters.Add(ch);
                    }
                }
            }
        }
        public int MaxSequenceLength { get; set; }
        public int MaxNumberOfSequences { get; set; }
        public int MinNumberOfSequences { get; set; }

        public Generator(string validCharacters, int maxSequenceLength, int maxNumberOfSequences, int minNumberOfSequences)
        {
            this.ValidCharacters = validCharacters;
            this.MaxSequenceLength = maxSequenceLength;
            this.MaxNumberOfSequences = maxNumberOfSequences;
            this.MinNumberOfSequences = minNumberOfSequences;
            this._rand = new Random();
        }

        public Generator(string validCharacters, int maxSequenceLength, int maxNumberOfSequences, int minNumberOfSequences, int seed)
        {
            this._rand = new Random(seed);
        }

        public void SetSeed(int seed)
        {
            this._rand = new Random(seed);
        }

        public string Generate()
        {
            StringBuilder sequenceBuilder = new StringBuilder();
            int numberOfSequences = _rand.Next(this.MinNumberOfSequences, this.MaxNumberOfSequences + 1);

            do
            {
                sequenceBuilder.Length = 0;

                for (int i = 0; i < numberOfSequences; i++)
                {
                    sequenceBuilder.AppendFormat("{0} ", GenerateSequence());
                }

            } while (!Validate(sequenceBuilder.ToString()));

            return sequenceBuilder.ToString().Trim();
        }

        private bool Validate(string exerciseSequences)
        {
            foreach (string c in _validCharacters)
            {
                if (!exerciseSequences.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        private string GenerateSequence()
        {
            int sequenceLength = _rand.Next(1, this.MaxSequenceLength + 1);
            StringBuilder sequence = new StringBuilder();
            int charIndex;

            for (int i = 0; i < sequenceLength; i++)
            {
                charIndex = _rand.Next(_validCharacters.Count);
                sequence.Append(_validCharacters[charIndex]);
            }

            return sequence.ToString();
        }
    }
}
