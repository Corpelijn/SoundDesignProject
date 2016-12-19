using Assets.Scripts.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI
{
    abstract class Person
    {
        #region "Fields"

        /// <summary>
        /// The name of the person
        /// </summary>
        protected string name;
        /// <summary>
        /// The gender of the person
        /// </summary>
        protected Gender gender;

        /// <summary>
        /// The gameobject to draw the person
        /// </summary>
        protected GameObject gameObject;
        /// <summary>
        /// The current position of the person
        /// </summary>
        protected Vector3 position;

        /// <summary>
        /// The hearing distance
        /// </summary>
        protected float hearing;
        /// <summary>
        /// The level of scared
        /// </summary>
        protected float scared;
        /// <summary>
        /// The level of willing to buy
        /// </summary>
        protected float buying;
        /// <summary>
        /// The level or angryness
        /// </summary>
        protected float angry;
        /// <summary>
        /// The level of sadness
        /// </summary>
        protected float sad;
        /// <summary>
        /// The walking speed
        /// </summary>
        protected float speed;
        /// <summary>
        /// Indicates if the player is walking
        /// </summary>
        protected bool walking;
        /// <summary>
        /// Indicates if the player is paranoid
        /// </summary>
        protected bool paranoid;
        /// <summary>
        /// Indicates if the player is currently being robbed
        /// </summary>
        protected bool robbed;
        /// <summary>
        /// Indicates if the player is running
        /// </summary>
        protected bool running;

        private bool hasPosition;
        private float timePassed;

        // Sound parameters
        /// <summary>
        /// The default pitch of the steps
        /// </summary>
        private float stepPitch;
        /// <summary>
        /// The interval of the sound effect
        /// </summary>
        protected float walkingInterval;

        #endregion

        #region "Constructors"

        protected Person()
        {
            if (!PRNG.IsInitialized)
            {
                PRNG.ChangeSeed(0);
            }

            // Generate some random values
            gender = (Gender)PRNG.GetNumber(0, 1);
            hearing = PRNG.GetFloatNumber(0, 1);
            scared = PRNG.GetFloatNumber(0, 1);
            buying = PRNG.GetFloatNumber(0, 1);
            angry = PRNG.GetFloatNumber(0, 1);
            sad = PRNG.GetFloatNumber(0, 1);
            speed = PRNG.GetFloatNumber(5, 10);
            
            walking = false;
            paranoid = false;
            robbed = false;
            running = false;

            walkingInterval = PRNG.GetFloatNumber(0.5f, 0.9f);
            stepPitch = PRNG.GetFloatNumber(0.5f, 1.5f);

            hasPosition = false;
            timePassed = 0f;

            // Generate a random name
            GenerateRandomName();
        }

        #endregion

        #region "Properties"

        public string FullName
        {
            get { return name; }
        }

        public Gender Gender
        {
            get { return gender; }
        }

        public bool HasPosition
        {
            get { return hasPosition; }
        }

        public float Hearing
        {
            get { return hearing; }
        }

        public float Scared
        {
            get { return scared; }
        }

        public float Buying
        {
            get { return buying; }
        }

        public float Angry
        {
            get { return angry; }
        }

        public float Sad
        {
            get { return sad; }
        }

        public float Speed
        {
            get { return speed; }
        }

        public bool Walking
        {
            get { return walking; }
        }

        public bool Paranoid
        {
            get { return paranoid; }
        }

        public bool Running
        {
            get { return running; }
        }

        public bool Robbed
        {
            get { return robbed; }
        }

        #endregion

        #region "Methods"

        private void GenerateRandomName()
        {
            // Load the names from prefabs
            string[] firstnames;
            string[] lastnames = PrefabSelector.INSTANCE.Surnames.text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (gender == Gender.Female)
            {
                firstnames = PrefabSelector.INSTANCE.FemaleNames.text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                firstnames = PrefabSelector.INSTANCE.MaleNames.text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }

            // Generate a random name
            name = firstnames[PRNG.GetNumber(0, firstnames.Length - 1)] + " " + lastnames[PRNG.GetNumber(0, lastnames.Length - 1)];
        }

        public void SetStartPosition(Vector3 startPosition)
        {
            if (!hasPosition)
            {
                position = startPosition;
                hasPosition = true;
            }
        }

        #endregion

        #region "Abstract/Virtual Methods"

        public virtual void Update()
        {
            timePassed += Time.deltaTime;
            if(timePassed >= walkingInterval)
            {
                AudioSource source = gameObject.GetComponent<AudioSource>();
                //source.pitch = PRNG.GetFloatNumber(stepPitch - 0.1f, stepPitch + 0.1f);
                source.Play();
                timePassed = 0f;
            }
        }

        public abstract void Trigger(TriggerType trigger);

        public virtual void Draw()
        {
            if (!hasPosition)
                return;

            if (gameObject == null)
            {
                gameObject = ObjectPool.Instantiate("person");
                gameObject.transform.parent = PersonManager.INSTANCE.PeopleTransform;
                gameObject.GetComponent<PersonIdentifier>().Person = this;
                gameObject.GetComponent<AudioSource>().clip = AudioManager.GetClip(AudioManager.INSTANCE.Footstep, 0f, 0f);
                gameObject.GetComponentInChildren<Renderer>().material.color = gender == Gender.Male ? Color.blue : Color.magenta;
            }

            gameObject.transform.position = position;
        }

        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"

        public static Person GenerateNew()
        {
            // Get all possible person types
            Assembly current = Assembly.GetExecutingAssembly();
            Type[] people = current.GetTypes().Where(x => x.Namespace == "Assets.Scripts.AI.People" && x.BaseType == typeof(Person)).ToArray();

            // Generate one of the people
            Person p = Activator.CreateInstance(people[PRNG.GetNumber(0, people.Length - 1)]) as Person;

            return p;
        }

        #endregion

        #region "Operators"



        #endregion
    }
}
