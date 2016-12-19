using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.LevelGenerator.GridObjects
{
    abstract class GridObject
    {
        #region "Fields"

        private int width;
        private int height;
        private int x;
        private int y;
        private bool isDrawn;
        protected GameObject gameObject;

        #endregion

        #region "Constructors"

        protected GridObject(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            isDrawn = false;
        }

        #endregion

        #region "Properties"

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public bool IsDrawn
        {
            get { return isDrawn; }
            protected set { isDrawn = value; }
        }

        public GameObject GameObject
        {
            get { return gameObject; }
        }

        #endregion

        #region "Methods"

        public abstract void Draw(Vector2 origin, Vector2 relativeDistance);

        public virtual void Update()
        {

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
