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

        public TurnAndMoveCommand(GameObject unit, Vector3 target, Vector3 rotationAxis, UnitCommandConfig config)
        {
            this.rotCommand = new RotateUnitCommand(unit, config.GetForward, () => { return (target - unit.transform.position).normalized; }, rotationAxis, config.TurningSpeed);
            this.moveCommand = new MoveUnitCommand(unit, config.GetForward, target, config.MovementSpeed, config.UnitRadius);
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
