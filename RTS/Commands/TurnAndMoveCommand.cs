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

        public TurnAndMoveCommand(GameObject unit, Vector3 target)
        {
            this.rotCommand = new RotateUnitCommand(unit, () => { return (target - unit.transform.position).normalized; });
            this.moveCommand = new MoveUnitCommand(unit, target);
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
