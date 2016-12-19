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
        public Gender Gender;
        public float Hearing;
        public float Scared;
        public float Buying;
        public float Angry;
        public float Sad;
        public float Speed;
        public bool Walking;
        public bool Paranoid;
        public bool Robbed;
        public bool Running;

        private Person person;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"

        public Person Person
        {
            set { person = value; SetEditorValues(); }
        }

        #endregion

        #region "Methods"

        private void SetEditorValues()
        {
            name = person.FullName + " (" + person.Gender + ")";

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
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void OnTriggerEnter(Collider collider)
        {
            person.Trigger(TriggerType.Collision);
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
