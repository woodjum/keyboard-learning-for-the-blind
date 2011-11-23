using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace KeyboardLesson
{
    public class KeySoundMapping
    {
        private static Dictionary<string, string> _mappings;
        private static KeySoundMapping _instance = null;

        private KeySoundMapping()
        {
            _mappings = new Dictionary<string, string>();

            _mappings.Add("a", "<pron sym=\"a 1\">a</pron>");
            _mappings.Add("b", "<pron sym=\"bo 1\">b</pron>");
            _mappings.Add("c", "<pron sym=\"ci 1\">c</pron>");
            _mappings.Add("d", "<pron sym=\"de 1\">d</pron>");
            _mappings.Add("e", "<pron sym=\"e 1\">e</pron>");
            _mappings.Add("f", "<pron sym=\"fo 1\">f</pron>");
            _mappings.Add("g", "<pron sym=\"ge 1\">g</pron>");
            _mappings.Add("h", "<pron sym=\"he 1\">h</pron>");
            _mappings.Add("i", "<pron sym=\"i 1\">i</pron>");
            _mappings.Add("j", "<pron sym=\"ji 1\">j</pron>");
            _mappings.Add("k", "<pron sym=\"ke 1\">k</pron>");
            _mappings.Add("l", "<pron sym=\"le 1\">l</pron>");
            _mappings.Add("m", "<pron sym=\"mo 1\">m</pron>");
            _mappings.Add("n", "<pron sym=\"ne 1\">n</pron>");
            _mappings.Add("o", "<pron sym=\"o 1\">o</pron>");
            _mappings.Add("p", "<pron sym=\"po 1\">p</pron>");
            _mappings.Add("q", "<pron sym=\"qi 1\">q</pron>");
            _mappings.Add("r", "<pron sym=\"ri 1\">r</pron>");
            _mappings.Add("s", "<pron sym=\"si 1\">s</pron>");
            _mappings.Add("t", "<pron sym=\"te 1\">t</pron>");
            _mappings.Add("u", "<pron sym=\"wu 1\">u</pron>");
            _mappings.Add("v", "<pron sym=\"yu 1\">v</pron>");
            _mappings.Add("w", "<pron sym=\"wo 1\">w</pron>");
            _mappings.Add("x", "<pron sym=\"xi 1\">x</pron>");
            _mappings.Add("y", "<pron sym=\"ya 1\">y</pron>");
            _mappings.Add("z", "<pron sym=\"zi 1\">z</pron>");
        }

        public static KeySoundMapping GetInstance()
        {
            if (_instance == null)
            {
                _instance = new KeySoundMapping();
            }

            return _instance;
        }

        public string this[char key]
        {
            get
            {
                return this[key.ToString()];
            }
        }

        public string this[string key]
        {
            get
            {
                string keyLowered = key.ToLowerInvariant();
                
                if (Regex.IsMatch(keyLowered, @"^[a-z]$"))
                {
                    return _mappings[keyLowered];
                }
                else
                {
                    return key;
                }
            }
        }
    }
}