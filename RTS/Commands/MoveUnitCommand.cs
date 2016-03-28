using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    /// <summary>
    /// Command that dictates an object move from one point to another
    /// </summary>
    public class MoveUnitCommand : ICommand
    {
        private GameObject unit;
        private Vector3 target;
        private float speed;        

        /// <summary>
        /// Creates a MoveUnitCommand that translates a unit to the specified position
        /// at the specified velocity (speed)
        /// </summary>
        /// <param name="unit">Unit to move</param>
        /// <param name="target">Target position to move to</param>
        /// <param name="speed">Velocity in world units per second to move at</param>
        public MoveUnitCommand(GameObject unit, Vector3 target, float speed)
        {
            this.unit = unit;
            this.target = target;
            this.speed = speed;
        }

        public void Do()
        {
            var direction = (this.unit.transform.position - target).normalized;      
                  
            var moveAmount = direction * speed * Time.deltaTime;
            this.unit.transform.position = this.unit.transform.position - moveAmount;
        }

        public bool IsComplete()
        {
            // A bit cheesy, but we will consider being within .5 units of the target position 
            // to count as arriving at the destination
            return Vector3.Distance(this.unit.transform.position, target) < .5;
        }
    }
}
