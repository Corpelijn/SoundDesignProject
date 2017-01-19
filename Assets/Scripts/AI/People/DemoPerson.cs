using Assets.Scripts.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.People.Demo
{
    class DemoPerson : Person
    {
        #region "Fields"



        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"



        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override void Update()
        {
            
        }

        public override void Trigger(TriggerType trigger)
        {
            if(trigger == TriggerType.Click)
            {
                // Play the sound for the collision with the other
                AudioFragment tekst = AudioManager.GetFragment("pardon");
                sourceManager.PlaySound(tekst, voice);
            }
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
