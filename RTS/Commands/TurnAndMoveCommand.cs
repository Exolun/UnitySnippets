using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    public class TurnAndMoveCommand : ICommand
    {
        private RotateUnitCommand rotCommand;
        private MoveUnitCommand moveCommand;

        public TurnAndMoveCommand(GameObject unit, Func<Vector3> forwardGetter, Vector3 target, Vector3 rotationAxis, float degreesPerSecond, float moveSpeed)
        {
            this.rotCommand = new RotateUnitCommand(unit, forwardGetter, () => {
                if(Vector3.Distance(unit.transform.position, target) > 3)
                {
                    return (target - unit.transform.position).normalized;
                }

                return forwardGetter();

            }, rotationAxis, degreesPerSecond);
            this.moveCommand = new MoveUnitCommand(unit, forwardGetter, target, moveSpeed);
        }

        public void Do()
        {
            this.rotCommand.Do();
            this.moveCommand.Do();
        }

        public bool IsComplete()
        {
            return this.rotCommand.IsComplete() && this.moveCommand.IsComplete();
        }
    }
}
