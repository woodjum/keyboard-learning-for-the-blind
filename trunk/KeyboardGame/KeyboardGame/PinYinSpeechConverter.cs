using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardGame
{
    public static class PinYinSpeechConverter
    {
        public static string Convert(string input)
        {
            
            KeySoundMapping mappings = KeySoundMapping.GetInstance();
            StringBuilder convertedOutput = new StringBuilder();
            StringBuilder subsegment = new StringBuilder();

            subsegment.Append("<s>");

            foreach (char c in input)
            {
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                {
                    if (subsegment == null)
                    {
                        subsegment = new StringBuilder();
                    }
                    subsegment.Append("</s>");
                    subsegment.Append(mappings[c]);
                    convertedOutput.Append(subsegment);
                    subsegment = null;
                }
                else
                {
                    if (subsegment == null)
                    {
                        subsegment = new StringBuilder();
                        subsegment.Append("<s>");
                    }
                    subsegment.Append(c);
                }
            }

            if (subsegment != null)
            {
                subsegment.Append("</s>");
                convertedOutput.Append(subsegment);
            }

            return convertedOutput.ToString();
        }
    }
}
