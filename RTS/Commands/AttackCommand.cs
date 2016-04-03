using System;
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
        private UnitAttackConfig attackConfig;
        private RotateTurretCommand rotTurretCmd;
        private TurnAndMoveCommand turnMoveCmd;

        public AttackCommand(GameObject unit, Func<Vector3> attackTargetGetter, bool attackGround)
        {
            this.unit = unit;
            this.attackTargetGetter = attackTargetGetter;
            this.attackConfig = unit.GetComponent<UnitAttackConfig>();

            this.rotTurretCmd = new RotateTurretCommand(unit, attackTargetGetter);

            this.setTurnMoveCmd();
        }

        private bool isInAttackRange()
        {
            return Vector3.Distance(unit.transform.position, this.attackTargetGetter()) < this.attackConfig.AttackRange;
        }

        private void setTurnMoveCmd()
        {
            if(turnMoveCmd == null && !this.isInAttackRange())
            {
                this.turnMoveCmd = new TurnAndMoveCommand(unit, this.attackTargetGetter());
            }
            else if(turnMoveCmd != null && this.isInAttackRange())
            {
                this.turnMoveCmd = null;
            }            
        }

        public void Do()
        {
            this.setTurnMoveCmd();

            if (this.turnMoveCmd != null)
            {
                this.turnMoveCmd.Do();
            }

            if (this.rotTurretCmd.IsComplete())
            {
                ///Turret is aimed, ready to fire
                if (this.isInAttackRange())
                {
                    this.attackConfig.FireIfReady(this.rotTurretCmd.AttackDirection());
                }
            }
            else
            {
                this.rotTurretCmd.Do();
            }            
        }

        public bool IsComplete()
        {
            return false;
        }
    }
}
