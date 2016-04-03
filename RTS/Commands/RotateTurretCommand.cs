using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility;

namespace Commands
{
    public class RotateTurretCommand : ICommand
    {
        private GameObject unit;
        private Func<Vector3> attackTargetGetter;
        private GameObject turret;
        private UnitCommandConfig config;
        private UnitAttackConfig attackConfig;
        private Vector3 rotationAxis;
        private float negation = 1.0f;

        public RotateTurretCommand(GameObject unit, Func<Vector3> attackTargetGetter)
        {
            this.attackConfig = unit.GetComponent<UnitAttackConfig>() as UnitAttackConfig;
            this.config = unit.GetComponent<UnitCommandConfig>() as UnitCommandConfig;

            this.unit = unit;
            this.attackTargetGetter = attackTargetGetter;
            this.turret = unit.transform.FindChild("Turret").gameObject;
            this.rotationAxis = this.config.GetRotationAxis();

            this.setNegation();
        }

        private void setNegation()
        {
            var dir = -(this.turret.transform.position - this.attackTargetGetter()).normalized;
            float angle = Vector3.Angle((-this.turret.transform.right), dir);
            float turningAmount = Mathf.Clamp(Time.deltaTime * this.attackConfig.TurretTurningSpeed, 0, angle);

            this.turret.transform.Rotate(this.rotationAxis, turningAmount);
            float newAngle = Vector3.Angle((-this.turret.transform.right), dir);

            if(newAngle > angle)
            {
                this.negation = -1.0f;
            }

            this.turret.transform.Rotate(this.rotationAxis, -turningAmount);
        }

        public Vector3 AttackDirection()
        {
            return -(this.turret.transform.position - this.attackTargetGetter()).normalized;
        }
        

        public void Do()
        {
            var dir = -(this.turret.transform.position - this.attackTargetGetter()).normalized;
            float angle = Vector3.Angle((-this.turret.transform.right), dir);
            float turningAmount = Mathf.Clamp(Time.deltaTime * this.attackConfig.TurretTurningSpeed, 0, angle);
            this.turret.transform.Rotate(this.rotationAxis, turningAmount * negation);
        }

        public bool IsComplete()
        {
            var dir = -(this.turret.transform.position - this.attackTargetGetter()).normalized;
            float angle = Vector3.Angle((-this.turret.transform.right), dir);

            return Math.Abs(angle) < .5;
        }
    }
}
