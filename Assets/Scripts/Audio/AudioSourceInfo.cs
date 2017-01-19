using Assets.Scripts.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    class AudioSourceInfo
    {
        #region "Fields"

        private AudioSource source;

        #endregion

        #region "Constructors"

        public AudioSourceInfo(GameObject obj)
        {
            source = obj.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.rolloffMode = AudioRolloffMode.Linear;
            source.dopplerLevel = 0;
            source.loop = false;
            source.spatialBlend = 1f;
            if (PersonManager.INSTANCE.DemoMode)
                source.maxDistance = 30;
            else
                source.maxDistance = 10;
            source.minDistance = 1;
        }

        public AudioSourceInfo(AudioSource source)
        {
            this.source = source;
        }

        #endregion

        #region "Properties"

        public bool IsBusy
        {
            get { return source.isPlaying; }
        }

        #endregion

        #region "Methods"

        public void Play(AudioFragment fragment, float pitch)
        {
            source.outputAudioMixerGroup = fragment.OutputGroup;
            source.clip = fragment.AudioClip;
            source.pitch = pitch;

            source.Play();
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
