using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    public class UnitCommandConfig : MonoBehaviour
    {
        /// <summary>
        /// Degrees per second this unit can turn
        /// </summary>
        public float TurningSpeed = 180;

        /// <summary>
        /// World units per second this unit can move
        /// </summary>
        public float MovementSpeed = 100;

        /// <summary>
        /// The relative axis to consider the 'forward' direction for the unit (for movement purposes)
        /// </summary>
        public string ForwardDirection = "left";

        /// <summary>
        /// The relative direction for rotating the unit to face something
        /// </summary>
        public string RotationAxis = "forward";

        /// <summary>
        /// Approximate size of the unit
        /// </summary>
        public float UnitRadius = 45;

        /// <summary>
        /// Disposition of the unit (determines behavior)
        /// </summary>
        public UnitDisposition Disposition = UnitDisposition.Defensive;        

        public Vector3 GetRotationAxis() {
            return this.getRelativeDirectionFromString(this.RotationAxis);
        }
        
        public Vector3 GetForward()
        {
            return this.getRelativeDirectionFromString(this.ForwardDirection);
        }

        private Vector3 getRelativeDirectionFromString(string direction)
        {
            string dirLower = direction.ToLower();

            if (dirLower == "left")
            {
                return -this.gameObject.transform.right;
            }
            else if (dirLower == "right")
            {
                return this.gameObject.transform.right;
            }
            else if (dirLower == "up")
            {
                return this.gameObject.transform.up;
            }
            else if (dirLower == "down")
            {
                return -this.gameObject.transform.up;
            }
            else if (dirLower == "forward")
            {
                return this.gameObject.transform.forward;
            }
            else
            {
                return -this.gameObject.transform.forward;
            }
        }
    }
}
