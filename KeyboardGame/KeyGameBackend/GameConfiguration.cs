using System;
using System.Configuration;

namespace KeyGameModel
{
    public class GameConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("MaxSequenceLength", IsRequired = true, DefaultValue = 6)]
        public int MaxSequenceLength
        {
            get
            {
                return (int)this["MaxSequenceLength"];
            }
            set
            {
                this["MaxSeqenceLength"] = value;
            }
        }

        [ConfigurationProperty("BasePointsPerLetter", IsRequired = true, DefaultValue = 50)]
        public int BasePointsPerLetter
        {
            get
            {
                return (int)this["BasePointsPerLetter"];
            }
            set
            {
                this["BasePointsPerLetter"] = value;
            }
        }

        [ConfigurationProperty("ExpectedMsPerLetter", IsRequired = true, DefaultValue = 500)]
        public int ExpectedMsPerLetter
        {
            get
            {
                return (int)this["ExpectedMsPerLetter"];
            }
            set
            {
                this["ExpectedMsPerLetter"] = value;
            }
        }

        [ConfigurationProperty("MaxSpeedBonusPerLetter", IsRequired = true, DefaultValue = 200)]
        public int MaxSpeedBonusPerLetter
        {
            get
            {
                return (int)this["MaxSpeedBonusPerLetter"];
            }
            set
            {
                this["MaxSpeedBonusPerLetter"] = value;
            }
        }

        [ConfigurationProperty("AdditionalLetterBonus", IsRequired = true, DefaultValue = 0.10)]
        public double AdditionalLetterBonus
        {
            get
            {
                return (double)this["AdditionalLetterBonus"];
            }
            set
            {
                this["AdditionalLetterBonus"] = value;
            }
        }

        [ConfigurationProperty("ChainingBonus", IsRequired = true, DefaultValue = 0.10)]
        public double ChainingBonus
        {
            get
            {
                return (double)this["ChainingBonus"];
            }
            set
            {
                this["ChainingBonus"] = value;
            }
        }

        [ConfigurationProperty("ChainingBonusCap", IsRequired = true, DefaultValue = 0.50)]
        public double ChainingBonusCap
        {
            get
            {
                return (double)this["ChainingBonusCap"];
            }
            set
            {
                this["ChainingBonusCap"] = value;
            }
        }

        public GameConfiguration()
        {

        }
    }
}