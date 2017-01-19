using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    class AudioManager : MonoBehaviour
    {
        #region "Fields"

        public AudioFragment[] fragments = null;

        #endregion

        #region "Constructors"



        #endregion

        #region "Singleton"

        private static AudioManager instance;

        public static AudioManager INSTANCE
        {
            get { return instance; }
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void Awake()
        {
            instance = this;
        }

        #endregion

        #region "Static Methods"

        //public static AudioClip GetClip(AudioClip source, float pitch, float speed)
        //{
        //    // Create a new AudioClipBaker to create the new AudioClip
        //    AudioClipBaker baker = new AudioClipBaker(source, pitch, speed);
        //    AudioClip newClip = baker.Bake();
        //    return newClip;
        //}

        public static AudioFragment GetFragment(string name)
        {
            Profiler.BeginSample("Fetch AudioFragment");
            AudioFragment fragment = INSTANCE.fragments.FirstOrDefault(x => x.Name == name);
            if (fragment == null && name != "")
            {
                Debug.Log("Cannot find the AudioFragment \'" + name + "\'");
            }

            Profiler.EndSample();
            return fragment;
        }

        #endregion

        #region "Operators"



        #endregion
    }
}
