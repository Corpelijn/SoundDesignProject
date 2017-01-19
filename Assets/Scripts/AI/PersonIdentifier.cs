using Assets.Scripts.AI.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI
{
    class PersonIdentifier : MonoBehaviour
    {
        #region "Fields"

        public string Name = "";
        public string Type;
        public Gender Gender;
        public float Hearing;
        public float Scared;
        public float Buying;
        public float Angry;
        public float Sad;
        public float Speed;
        public float Voice;
        public bool Walking;
        public bool Paranoid;
        public bool Robbed;
        public bool Running;

        private Person person;

        private float demoTime;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"

        public Person Person
        {
            get { return person; }
            set { person = value; SetEditorValues(); }
        }

        #endregion

        #region "Methods"

        public void SetEditorValues()
        {
            name = person.FullName + " (" + person.Gender + ")";

            Type = person.GetType().Name;
            Name = person.FullName;
            Gender = person.Gender;
            Hearing = person.Hearing;
            Scared = person.Scared;
            Buying = person.Buying;
            Angry = person.Angry;
            Sad = person.Sad;
            Speed = person.Speed;
            Walking = person.Walking;
            Paranoid = person.Paranoid;
            Robbed = person.Robbed;
            Running = person.Running;
            Voice = person.Voice;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void OnTriggerEnter(Collider collider)
        {
            PersonIdentifier identifier = collider.GetComponentInChildren<PersonIdentifier>();
            if (identifier != null)
            {
                person.Trigger(TriggerType.PersonCollision);
                int rand = PRNG.GetNumber(0, 100);
                if (rand < 15 && identifier.person.GetType() == typeof(Walker) && person.GetType() == typeof(Walker))
                {
                    Talker t1 = new Talker(person);
                    Talker t2 = new Talker(identifier.person);
                    t1.TalksTo = t2;
                    t2.TalksTo = t1;
                    PersonManager.INSTANCE.UpdatePerson(person, t1);
                    PersonManager.INSTANCE.UpdatePerson(identifier.person, t2);
                    SetEditorValues();
                    identifier.SetEditorValues();
                    t1.Trigger(TriggerType.PersonStartTalking);
                }
            }
            else
                person.Trigger(TriggerType.Collision);
        }

        public void OnMouseOver()
        {
            if (PersonManager.INSTANCE.DemoMode)
            {
                if (demoTime >= 0.5f)
                {
                    person.Trigger(TriggerType.Click);
                    demoTime = 0f;
                }
            }
        }

        public void Update()
        {
            demoTime += Time.deltaTime;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
