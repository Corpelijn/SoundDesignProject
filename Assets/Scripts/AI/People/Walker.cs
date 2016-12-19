using Assets.Scripts.LevelGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.People
{
    class Walker : Person
    {
        #region "Fields"

        private Vector3? destination;
        private Vector3 startPosition;
        private float interpolation;
        private Vector3 lastPosition;

        #endregion

        #region "Constructors"

        public Walker()
        {
            destination = null;
            interpolation = 0f;
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        public void Walk()
        {
            // Check if we have a destination
            if (destination == null)
            {
                // Generate a new destination
                GenerateDestination();
            }

            // Let the person walk towards the destination
            Vector3 newPosition = Vector3.Lerp(startPosition, destination.Value, interpolation);
            if (!LevelGenerator.LevelGenerator.INSTANCE.Grid.IsWalkable(newPosition.x, newPosition.z))
            {
                destination = null;
            }
            else
            {
                lastPosition = position;
                position = newPosition;
            }

            if (gameObject != null && destination.HasValue)
            {
                gameObject.transform.LookAt(destination.Value);
                gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, 0);
            }

            interpolation += speed * Time.deltaTime;

            // Check if we have reached our destination
            if (interpolation >= 1f)
            {
                destination = null;
            }
        }

        private void GenerateDestination()
        {
            Grid grid = LevelGenerator.LevelGenerator.INSTANCE.Grid;
            float halfWidth = grid.Width / 2f;
            float halfHeight = grid.Height / 2f;

            while (!destination.HasValue || !grid.IsWalkable(destination.Value.x, destination.Value.z))
            {
                destination = new Vector3(PRNG.GetFloatNumber(-halfWidth, halfWidth), 0, PRNG.GetFloatNumber(-halfHeight, halfHeight));
            }

            interpolation = 0f;


            // Calculate the total time to walk the distance
            float time = PersonManager.INSTANCE.TimePerSquare;
            float distanceTime = Vector3.Distance(position, destination.Value) * (time < 0f ? 0.5f : time);
            speed = 1f / distanceTime;

            startPosition = position;

            Debug.DrawLine(destination.Value, destination.Value + Vector3.up, Color.red, 3, false);
        }

        private void CollideWithOtherPerson()
        {
            // Play the sound for the collision with the other

            // Give the walker a new position to walk to
            position = lastPosition;
            destination = null;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override void Update()
        {
            base.Update();

            if (HasPosition)
                Walk();
        }

        public override void Trigger(TriggerType trigger)
        {
            // If a walker has a collision, do a walking update
            if(trigger == TriggerType.Collision)
            {
                CollideWithOtherPerson();
            }
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
