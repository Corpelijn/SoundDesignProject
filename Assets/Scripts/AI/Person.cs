﻿using Assets.Scripts.AI.People;
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

        /// <summary>
        /// The pitch of the voice
        /// </summary>
        protected float voice;

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

        //private AudioSource walkingSource;
        //protected AudioSource talkingSource;
        protected AudioSourceManager sourceManager;

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
            voice = gender == Gender.Male ? PRNG.GetFloatNumber(0.90f, 1.00f) : PRNG.GetFloatNumber(1.05f, 1.15f);

            walking = false;
            paranoid = false;
            robbed = false;
            running = false;

            walkingInterval = PRNG.GetFloatNumber(0.5f, 0.9f);
            stepPitch = PRNG.GetFloatNumber(1f, 1.2f, 3);

            hasPosition = false;
            timePassed = 0f;

            // Generate a random name
            GenerateRandomName();
        }

        protected Person(Person person)
        {
            // Copy the values
            name = person.name;
            gender = person.gender;
            hearing = person.hearing;
            scared = person.scared;
            buying = person.buying;
            angry = person.angry;
            sad = person.sad;
            speed = person.speed;
            voice = person.voice;

            walking = person.walking;
            paranoid = person.paranoid;
            robbed = person.robbed;
            running = person.running;

            walkingInterval = person.walkingInterval;
            stepPitch = person.stepPitch;

            hasPosition = person.hasPosition;
            timePassed = person.timePassed;

            gameObject = person.gameObject;
            position = person.position;
            sourceManager = new AudioSourceManager(gameObject);
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

        public GameObject GameObject
        {
            get { return gameObject; }
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

        public float Voice
        {
            get { return voice; }
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

        private void GenerateAudio()
        {
            //AudioSource[] sources = gameObject.GetComponents<AudioSource>();
            //walkingSource = sources[0];
            //talkingSource = sources[1];

            //talkingSource.pitch = voice;

            //walkingSource.clip = AudioManager.GetClip(AudioManager.GetFragment("footstep"), stepPitch, 0f);
            //talkingSource.clip = AudioManager.GetClip(AudioManager.GetFragment("sorry"), voice, 0f);
        }

        #endregion

        #region "Abstract/Virtual Methods"

        public virtual void Update()
        {
            timePassed += Time.deltaTime;
            if (timePassed >= walkingInterval)
            {
                //walkingSource.pitch = PRNG.GetFloatNumber(stepPitch - 0.1f, stepPitch + 0.1f);
                //walkingSource.Play();

                float pitch = PRNG.GetFloatNumber(stepPitch - 0.1f, stepPitch + 0.1f);
                sourceManager.PlaySound(AudioManager.GetFragment("footstep"), pitch);
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
                sourceManager = new AudioSourceManager(gameObject);
                gameObject.transform.parent = PersonManager.INSTANCE.PeopleTransform;
                gameObject.GetComponent<PersonIdentifier>().Person = this;
                gameObject.GetComponentInChildren<Renderer>().material.color = gender == Gender.Male ? Color.blue : Color.magenta;

                //GenerateAudio();
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
