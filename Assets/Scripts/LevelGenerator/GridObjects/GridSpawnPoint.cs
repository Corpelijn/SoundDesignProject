using Assets.Scripts.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.LevelGenerator.GridObjects
{
    class GridSpawnPoint : GridRoad
    {
        #region "Fields"

        private bool available;

        #endregion

        #region "Constructors"

        public GridSpawnPoint(int x, int y) : base(x, y)
        {
            PersonManager.INSTANCE.AddSpawnPoint(this);
            available = true;
        }

        #endregion

        #region "Properties"

        public bool IsAvailable
        {
            get { return available; }
        }

        #endregion

        #region "Methods"

        public void Spawn(Person p)
        {
            p.SetStartPosition(gameObject.transform.position + new Vector3(.5f, 0f, .5f));
            available = false;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override void Update()
        {
            if (gameObject != null)
            {
                Vector3 origin = gameObject.transform.position;
                Ray[] rays = new Ray[] {
                            new Ray(origin, Vector3.up),
                            new Ray(origin + new Vector3(1f, 0f, 0f), Vector3.up),
                            new Ray(origin + new Vector3(1f, 0, 1f), Vector3.up),
                            new Ray(origin + new Vector3(0f, 0, 1f), Vector3.up)
                            };

                RaycastHit hit;
                foreach (Ray ray in rays)
                {
                    bool raycastHit = Physics.Raycast(ray, out hit);
                    if (raycastHit)
                    {
                        available = false;
                        return;
                    }
                }

                available = true;
            }
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
