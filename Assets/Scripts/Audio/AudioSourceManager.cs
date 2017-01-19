using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    class AudioSourceManager
    {
        #region "Fields"

        private GameObject gameObject;
        private List<AudioSourceInfo> sources;

        #endregion

        #region "Constructors"

        public AudioSourceManager(GameObject parent)
        {
            gameObject = parent;
            AudioSource[] sources = parent.GetComponentsInChildren<AudioSource>();
            this.sources = new List<AudioSourceInfo>();
            foreach (AudioSource source in sources)
            {
                this.sources.Add(new AudioSourceInfo(source));
            }
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        public AudioSourceInfo PlaySound(AudioFragment fragment, float pitch)
        {
            if (fragment == null)
                return null;

            AudioSourceInfo source = GetSource();
            if (source == null)
            {
                source = new AudioSourceInfo(gameObject);
                sources.Add(source);
            }

            source.Play(fragment, pitch);
            return source;
        }

        public AudioSourceInfo GetSource()
        {
            return sources.FirstOrDefault(x => !x.IsBusy);
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
