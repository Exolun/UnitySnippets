using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility;


namespace Commands
{
    public class RotateUnitCommand : ICommand
    {
        private float degreesPerSecond;
        private Func<Vector3> forwardGetter;
        private Func<Vector3> desiredForwardGetter;        
        private readonly Vector3 rotationAxis;
        private GameObject unit;

        public RotateUnitCommand(GameObject unit, Func<Vector3> desiredForwardGetter)
        {
            var config = unit.GetComponent<UnitCommandConfig>() as UnitCommandConfig;

            this.unit = unit;
            this.forwardGetter = config.GetForward;
            this.desiredForwardGetter = desiredForwardGetter;
            this.degreesPerSecond = config.TurningSpeed;
            this.rotationAxis = config.GetRotationAxis();
        }

        public void Do()
        {

            Vector3 forward = this.forwardGetter();
            Vector3 desiredForward = this.desiredForwardGetter();

            float angle = forward.SignedAngleBetween(desiredForward, this.rotationAxis);
            float turningAmount = Time.deltaTime * degreesPerSecond;

            if (angle < 1)
                turningAmount = -turningAmount;

            if (Math.Abs(turningAmount) > Math.Abs(angle))
                turningAmount = angle;

            this.unit.transform.Rotate(this.rotationAxis, turningAmount);
        }

        public bool IsComplete()
        {
            return this.forwardGetter() == this.desiredForwardGetter();
        }
    }
}
