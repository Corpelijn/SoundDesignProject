using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.LevelGenerator.GridObjects
{
    class GridStone : GridObject
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        public GridStone(int x, int y) : base(x, y, 1, 1)
        {

        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        public override void Draw(Vector2 origin, Vector2 relativeDistance)
        {
            gameObject = ObjectPool.Instantiate("stonefloor");
            gameObject.transform.position = new Vector3(origin.x + X + relativeDistance.x, 0, origin.y + Y + relativeDistance.y);
            gameObject.transform.parent = LevelGenerator.INSTANCE.ParentObject;

            IsDrawn = true;

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
