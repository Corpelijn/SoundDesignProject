using Assets.Scripts.LevelGenerator.GridObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.LevelGenerator
{
    class Grid
    {
        #region "Fields"

        private List<GridObject> gridObjects;
        private int width;
        private int height;
        private int distanceFromOriginX;
        private int distanceFromOriginY;

        #endregion

        #region "Constructors"

        public Grid()
        {
            gridObjects = new List<GridObject>();
        }

        #endregion

        #region "Properties"

        public int Width
        {
            get { Normalize(); return width; }
        }

        public int Height
        {
            get { Normalize(); return height; }
        }

        #endregion

        #region "Methods"

        public void AddGridObject(GridObject gridObject)
        {
            bool exists = gridObjects.FirstOrDefault(x => x.X == gridObject.X && x.Y == gridObject.Y && x.GetType() == gridObject.GetType()) != null;
            if (!exists)
                gridObjects.Add(gridObject);
        }

        public bool IsWalkable(float x, float y)
        {
            int xPos = Mathf.FloorToInt(x - distanceFromOriginX + width / 2f);
            int yPos = Mathf.FloorToInt(y - distanceFromOriginY + height / 2f);

            GridObject obj = gridObjects.FirstOrDefault(f => f.X == xPos && f.Y == yPos);
            if (obj != null)
            {
                return true;
            }

            return false;
        }

        public void Draw()
        {
            Normalize();

            foreach (GridObject gridObject in gridObjects)
            {
                // Update the GridObject
                gridObject.Update();

                // Check if the GridObject is already drawn, otherwise draw it.
                if (!gridObject.IsDrawn)
                    gridObject.Draw(new Vector2(-width / 2f, -height / 2f), new Vector2(distanceFromOriginX, distanceFromOriginY));
            }
        }

        public void Normalize()
        {
            int minX = gridObjects.Min(x => x.X);
            int minY = gridObjects.Min(y => y.Y);
            int maxX = gridObjects.Max(x => x.X + x.Width);
            int maxY = gridObjects.Max(y => y.Y + y.Height);

            width = maxX - minX;
            height = maxY - minY;
            distanceFromOriginX = Math.Abs(minX);
            distanceFromOriginY = Math.Abs(minY);
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
