using System;
using System.Collections.Generic;
using System.IO;

namespace KeyGameModel
{
    public class Level
    {
        private String _name = String.Empty;
        private String _description = String.Empty;
        private String _characters = String.Empty;
        private List<LevelSequence> _sequences = null;
        private int _index = 0;
        private Random _rand = new Random();

        public String Name
        {
            get
            {
                return _name;
            }
        }

        public String Description
        {
            get
            {
                return _description;
            }
        }

        public String Characters
        {
            get
            {
                return _characters;
            }
        }

        public int SequenceCount
        {
            get
            {
                return _sequences.Count;
            }
        }

        public bool EndOfLevel
        {
            get
            {
                return _index >= _sequences.Count;
            }
        }

        public LevelSequence NextSequence
        {
            get
            {
                if (_index < _sequences.Count)
                {
                    return _sequences[_index++];
                }

                return null;
            }
        }

        public Level(String fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                String line = String.Empty;
                String key;
                String value;
                int splitterIndex;

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    splitterIndex = line.IndexOf('=');
                    key = line.Substring(0, splitterIndex).ToLowerInvariant().Trim();
                    value = line.Substring(splitterIndex + 1);
                    switch (key)
                    {
                        case "name": this._name = value; break;
                        case "description": this._description = value; break;
                        case "characters": this._characters = value; break;
                        case "sequence":
                            {
                                String[] tokens = value.Split(' ');
                                this._sequences = new List<LevelSequence>();

                                foreach (String token in tokens)
                                {
                                    this._sequences.Add(new LevelSequence(token));
                                }
                                break;
                            }
                    }
                }
            }
            RandomizeSequence();
        }

        public double GetTheoreticalTopScore(GameConfiguration config)
        {
            double baseScore = 0; 
            double speedBonus = 0;
            double chainBonus = 0;

            foreach (LevelSequence sequence in _sequences)
            {
                if (sequence.SequenceLength <= config.MaxSequenceLength)
                {
                    baseScore += sequence.GetFullBaseScore(config);
                    speedBonus += config.MaxSpeedBonusPerLetter * sequence.SequenceLength;
                }
            }

            chainBonus = (baseScore + speedBonus) * config.ChainingBonusCap;

            return baseScore + speedBonus + chainBonus;
        }

        private void RandomizeSequence()
        {
            List<LevelSequence> randomizeSequences = new List<LevelSequence>();
            int count = _sequences.Count;

            for (int i = 0; i < count; i++)
            {
                int index = _rand.Next(_sequences.Count);

                randomizeSequences.Add(_sequences[index]);
                _sequences.RemoveAt(index);
            }

            _sequences = randomizeSequences;
        }
    }
}