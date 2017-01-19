using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Audio.Effects
{
    /// <summary>
    /// http://stackoverflow.com/questions/9805407/normalizing-audio-how-to-convert-a-float-array-to-a-byte-array
    /// </summary>
    class AudioNormalizer
    {
        #region "Fields"



        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"



        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"

        /// <summary>
        ///  Normalizes the audio
        /// </summary>
        /// <param name="samples">The input/output audio samples</param>
        public static void Normalize(float[] samples)
        {
            // Loop through the samples
            float biggest = -1f;
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] = samples[i] * 10f;
                if (samples[i] > biggest)
                    biggest = samples[i];
            }

            // Get the offset
            float offset = 0.9f - biggest;

            // Normalize the audio
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] = samples[i] + offset;
            }
        }

        #endregion

        #region "Operators"



        #endregion
    }
}
