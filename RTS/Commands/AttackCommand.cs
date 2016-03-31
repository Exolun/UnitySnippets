﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    public class AttackCommand : ICommand
    {
        private GameObject unit;
        private Func<Vector3> attackTargetGetter;
        private RotateTurretCommand rotTurretCmd;

        public AttackCommand(GameObject unit, Func<Vector3> attackTargetGetter, bool attackGround)
        {
            this.unit = unit;
            this.attackTargetGetter = attackTargetGetter;

            this.rotTurretCmd = new RotateTurretCommand(unit, attackTargetGetter);
        }

        public void Do()
        {
            rotTurretCmd.Do();

            ///Turret is aimed, ready to fire
            if(rotTurretCmd.IsComplete())
            {

            }
        }

        public bool IsComplete()
        {
            return false;
        }
    }
}
