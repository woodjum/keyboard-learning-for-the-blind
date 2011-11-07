using System;
using System.Collections.Generic;
using System.Text;

namespace ExerciseGenerator
{
    public class ValidCharInputReader
    {
        string _input;
        int _index;

        public ValidCharInputReader(string input)
        {
            _input = input.ToLowerInvariant();
            _index = 0;
        }

        public string ReadNextCharacter()
        {
            if (_index >= _input.Length)
            {
                return null;
            }
            else
            {
                string buffer = string.Empty;
                switch (_input[_index])
                {
                    case ' ': buffer = @"\s"; _index++; break;
                    case '\t': buffer = @"\t"; _index++; break;
                    case '\\': buffer = _input.Substring(_index, 2); _index += 2; break;
                    case '[': buffer = _input.Substring(_index, _input.IndexOf(']', _index) - _index + 1); _index += buffer.Length; break;
                    default: buffer += _input[_index]; _index++; break;
                }

                return buffer;
            }
        }
    }
}
