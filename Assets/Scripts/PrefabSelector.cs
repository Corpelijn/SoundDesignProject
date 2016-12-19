using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class PrefabSelector : MonoBehaviour
    {
        #region "Fields"

        public TextAsset FemaleNames;
        public TextAsset MaleNames;
        public TextAsset Surnames;

        #endregion

        #region "Constructors"



        #endregion

        #region "Singleton"

        private static PrefabSelector instance;

        public static PrefabSelector INSTANCE
        {
            get
            {
                return instance;
            }
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



        #endregion

        #region "Operators"



        #endregion
    }
}
