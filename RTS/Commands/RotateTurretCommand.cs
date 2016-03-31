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

        public RotateTurretCommand(GameObject unit, Func<Vector3> attackTargetGetter)
        {
            this.attackConfig = unit.GetComponent<UnitAttackConfig>() as UnitAttackConfig;
            this.config = unit.GetComponent<UnitCommandConfig>() as UnitCommandConfig;

            this.unit = unit;
            this.attackTargetGetter = attackTargetGetter;
            this.turret = unit.transform.FindChild("Turret").gameObject;
            this.rotationAxis = this.config.GetRotationAxis();
        }

        public void Do()
        {
            Vector3 target = this.attackTargetGetter();

            float angle = target.SignedAngleBetween((-this.turret.transform.right), this.rotationAxis);
            if (Math.Abs(angle) < 1)
                return;

            float turningAmount = Time.deltaTime * this.attackConfig.TurretTurningSpeed;
            if (Math.Abs(turningAmount) > Math.Abs(angle))
            {
                turningAmount = angle;
            }
            else
            {
                if (angle < 0)
                    turningAmount = -turningAmount;
            }            

            this.turret.transform.Rotate(this.rotationAxis, turningAmount);
        }

        public bool IsComplete()
        {
            float angle = Vector3.Angle(this.attackTargetGetter(), (-this.turret.transform.right));
            return Math.Round(angle) < 1;
        }
    }
}
