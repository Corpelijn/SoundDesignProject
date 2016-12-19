using Assets.Scripts.LevelGenerator.GridObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI
{
    class PersonManager : MonoBehaviour
    {
        #region "Fields"

        public int TotalPeople = 30;
        public float TimePerSquare = 0.5f;
        public Transform PeopleTransform;
        

        private List<Person> people;
        private List<GridSpawnPoint> spawnPoints;

        #endregion

        #region "Constructors"



        #endregion

        #region "Singleton"

        private static PersonManager instance;

        public static PersonManager INSTANCE
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

        private void SpawnPerson(Person person)
        {
            if (spawnPoints.Count == 0)
                return;

            GridSpawnPoint spawnPoint = spawnPoints[0];
            if (spawnPoint.IsAvailable)
            {
                spawnPoint.Spawn(person);
            }

            spawnPoints.Remove(spawnPoint);
            spawnPoints.Add(spawnPoint);
        }

        private void GenerateNewPerson()
        {
            Person p = Person.GenerateNew();
            if (p != null)
                people.Add(p);
        }

        private void UpdatePeople()
        {
            foreach (Person p in people)
            {
                if (!p.HasPosition)
                {
                    SpawnPerson(p);
                }
                p.Update();
                p.Draw();
            }
        }

        public void AddSpawnPoint(GridSpawnPoint spawnpoint)
        {
            spawnPoints.Add(spawnpoint);
            Shuffle(spawnPoints);
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void Awake()
        {
            instance = this;

            people = new List<Person>();
            spawnPoints = new List<GridSpawnPoint>();

            if (PeopleTransform == null)
                PeopleTransform = transform;
        }

        public void Update()
        {
            if(TotalPeople > people.Count)
            {
                GenerateNewPerson();
            }

            UpdatePeople();
        }

        #endregion

        #region "Static Methods"

        private static System.Random rng = new System.Random();

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        #endregion

        #region "Operators"



        #endregion
    }
}
