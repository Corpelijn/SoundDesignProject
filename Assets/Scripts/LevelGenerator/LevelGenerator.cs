using Assets.Scripts.LevelGenerator.GridObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.LevelGenerator
{
    class LevelGenerator : MonoBehaviour
    {
        #region "Fields"

        public string Seed = "test";

        private Grid grid;
        private int squareWidth;
        private int squareHeight;
        private int roadWidth;
        private int parkingSpots;

        public Transform ParentObject;

        #endregion

        #region "Constructors"



        #endregion

        #region "Singleton"

        private static LevelGenerator instance;

        public static LevelGenerator INSTANCE
        {
            get { return instance; }
        }

        #endregion

        #region "Properties"

        public Grid Grid
        {
            get { return grid; }
        }

        #endregion

        #region "Methods"

        private void GenerateLevel()
        {
            GenerateSquare();
            GenerateRoads();
        }

        private void GenerateSquare()
        {
            squareWidth = PRNG.GetNumber(10, 20);
            squareHeight = PRNG.GetNumber(10, 20);

            for (int i = 0; i < squareWidth; i++)
            {
                for (int j = 0; j < squareHeight; j++)
                {
                    grid.AddGridObject(new GridStone(i, j));
                }
            }
        }

        private void GenerateRoads()
        {
            roadWidth = PRNG.GetNumber(2, 4);
            parkingSpots = PRNG.GetNumber(0, 1);
            const int margin = 10;

            for (int i = -roadWidth - margin; i < squareWidth + roadWidth + parkingSpots + margin; i++)
            {
                for (int j = -roadWidth; j < 0; j++)
                {
                    if (i == -roadWidth - margin || i == squareWidth + roadWidth + parkingSpots + margin - 1)
                    {
                        grid.AddGridObject(new GridSpawnPoint(i, j));
                        grid.AddGridObject(new GridSpawnPoint(i, j + squareWidth + roadWidth));
                    }
                    else
                    {
                        grid.AddGridObject(new GridRoad(i, j));
                        grid.AddGridObject(new GridRoad(i, j + squareWidth + roadWidth));
                    }
                }
            }

            for (int i = -roadWidth - margin; i < squareHeight + roadWidth + parkingSpots + margin; i++)
            {
                for (int j = -roadWidth; j < 0; j++)
                {
                    if (i == -roadWidth - margin || i == squareHeight + roadWidth + parkingSpots + margin - 1)
                    {
                        grid.AddGridObject(new GridSpawnPoint(j, i));
                        grid.AddGridObject(new GridSpawnPoint(j + squareHeight + roadWidth, i));
                    }
                    else
                    {
                        grid.AddGridObject(new GridRoad(j, i));
                        grid.AddGridObject(new GridRoad(j + squareHeight + roadWidth, i));
                    }
                }
            }
        }

        private void GenerateMarketStands()
        {
            int amount = PRNG.GetNumber(5, 10);

            for (int i = 0; i < amount; i++)
            {

            }
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void Awake()
        {
            instance = this;

            if (ParentObject == null)
                ParentObject = transform;
        }

        public void Start()
        {
            PRNG.ChangeSeed(Seed);
            grid = new Grid();

            GenerateLevel();
        }

        public void Update()
        {
            grid.Draw();
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
