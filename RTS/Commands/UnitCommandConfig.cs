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
        /// Degrees per second this unit's turret can turn
        /// </summary>
        public float TurretTurningSpeed = 270;

        public string ForwardDirection = "left";

        void Start()
        {

        }

        public Vector3 GetForward()
        {
            if(ForwardDirection.ToLower() == "left")
            {
                return -this.gameObject.transform.right;
            }
            else if (ForwardDirection.ToLower() == "right")
            {
                return this.transform.right;
            }
            else if (ForwardDirection.ToLower() == "up")
            {
                return this.transform.up;
            }
            else if (ForwardDirection.ToLower() == "down")
            {
                return -this.transform.up;
            }
            else if (ForwardDirection.ToLower() == "forward")
            {
                return this.transform.forward;
            }
            else
            {
                return -this.transform.forward;
            }
        }
    }
}
