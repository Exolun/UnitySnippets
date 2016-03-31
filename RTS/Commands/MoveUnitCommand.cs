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
        private Func<Vector3> forwardGetter;
        private float speed;
        private float radius;

        /// <summary>
        /// Creates a MoveUnitCommand that translates a unit to the specified position
        /// at the specified velocity (speed)
        /// </summary>
        /// <param name="unit">Unit to move</param>
        /// <param name="target">Target position to move to</param>
        /// <param name="speed">Velocity in world units per second to move at</param>
        public MoveUnitCommand(GameObject unit, Vector3 target)
        {
            var config = unit.GetComponent<UnitCommandConfig>() as UnitCommandConfig;

            this.unit = unit;
            this.target = target;
            this.speed = config.MovementSpeed;
            this.forwardGetter = config.GetForward;
            this.radius = config.UnitRadius;
        }

        public void Do()
        {                  
            var moveAmount = this.forwardGetter() * speed * Time.deltaTime;
            this.unit.transform.position = this.unit.transform.position + moveAmount;
        }

        public bool IsComplete()
        {
            return Vector3.Distance(this.unit.transform.position, target) < this.radius;
        }
    }
}
