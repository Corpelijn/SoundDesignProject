using Assets.Scripts.Audio.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    /// <summary>
    /// Bakes a new AudioClip form a original
    /// </summary>
    class AudioClipBaker
    {
        #region "Fields"

        private AudioClip source;
        private float pitch;
        private float speed;

        #endregion

        #region "Constructors"

        public AudioClipBaker(AudioClip source, float pitch, float speed)
        {
            this.source = source;
            this.pitch = pitch;
            this.speed = speed;
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        /// <summary>
        /// Bake a new AudioClip from the original source with the given parameters
        /// </summary>
        /// <returns>The new AudioClip baked with the given parameters</returns>
        public AudioClip Bake()
        {
            // Create a new AudioClip
            AudioClip ajusted = AudioClip.Create("Footstep", source.samples, source.channels, source.frequency, false);

            // Read the samples from the source clip
            float[] samples = new float[source.samples];
            source.GetData(samples, 0);

            // Pitch the samples acording to the given pitch
            Profiler.BeginSample("Bake single AudioClip");
            PitchShifter.PitchShift(pitch, samples.Length, 32, 32, ajusted.frequency, samples);
            Profiler.EndSample();
            
            // Normalize the audio
            AudioNormalizer.Normalize(samples);

            // Write the ajusted samples to the new AudioClip
            ajusted.SetData(samples, 0);

            return ajusted;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
