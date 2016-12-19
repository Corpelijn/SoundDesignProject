using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.LevelGenerator.GridObjects
{
    class GridRoad : GridObject
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        public GridRoad(int x, int y) : base(x, y, 1, 1)
        {

        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override void Draw(Vector2 origin, Vector2 relativeDistance)
        {
            gameObject = ObjectPool.Instantiate("road");
            gameObject.transform.position = new Vector3(origin.x + X + relativeDistance.x, 0, origin.y + Y + relativeDistance.y);
            gameObject.transform.parent = LevelGenerator.INSTANCE.ParentObject;

            IsDrawn = true;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
