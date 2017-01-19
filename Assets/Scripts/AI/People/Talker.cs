using Assets.Scripts.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.People
{
    class Talker : Person
    {
        #region "Fields"

        private Person talksTo;
        private AudioSourceInfo current;
        private bool signalOtherToStartTalking;
        private float conversationDuration;
        private float timePassed;

        #endregion

        #region "Constructors"

        public Talker(Person original) : base(original)
        {
            conversationDuration = PRNG.GetFloatNumber(5, 15);
            timePassed = 0;
        }

        public Talker()
        {
            timePassed = 0;
        }

        #endregion

        #region "Properties"

        public Person TalksTo
        {
            get { return talksTo; }
            set { talksTo = value; conversationDuration = (talksTo as Talker).conversationDuration; }
        }

        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override void Trigger(TriggerType trigger)
        {
            if (gameObject == null)
                return;

            if (trigger == TriggerType.PersonTalk || trigger == TriggerType.PersonStartTalking)
            {
                // Play a random text for a conversation
                string[] randomTextForConversation = new string[] { "zin1", "zin2", "zin3", "zin4", "zin5", "zin6", "zin7", "zin8", "zin9", "zin10", "goed", "redelijk", "slecht", "hoeishet", "metjou", "straks" };
                AudioFragment tekst = AudioManager.GetFragment(randomTextForConversation[PRNG.GetNumber(0, randomTextForConversation.Length - 1)]);
                current = sourceManager.PlaySound(tekst, voice);
                signalOtherToStartTalking = true;
            }
            else if (trigger == TriggerType.PersonSaysBye)
            {
                // Play a random text to end the conversation
                string[] randomTextForConversation = new string[] { "totziens", "doei", "ciya" };
                AudioFragment tekst = AudioManager.GetFragment(randomTextForConversation[PRNG.GetNumber(0, randomTextForConversation.Length - 1)]);
                current = sourceManager.PlaySound(tekst, voice);
            }
        }

        public override void Update()
        {
            if (gameObject == null)
                return;

            if (talksTo == null || talksTo.GetType() != typeof(Talker) || timePassed >= conversationDuration)
            {
                Trigger(TriggerType.PersonSaysBye);
                PersonManager.INSTANCE.UpdatePerson(this, new Walker(this));
            }

            timePassed += Time.deltaTime;

            if (current == null)
                return;

            if (!current.IsBusy && signalOtherToStartTalking)
            {
                talksTo.Trigger(TriggerType.PersonTalk);
                signalOtherToStartTalking = false;
            }
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
