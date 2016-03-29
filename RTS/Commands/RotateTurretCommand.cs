using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    public class RotateTurretCommand : ICommand
    {
        private GameObject unit;
        private Func<Vector3> attackTargetGetter;
        private GameObject turret;

        public RotateTurretCommand(GameObject unit, Func<Vector3> attackTargetGetter)
        {
            this.unit = unit;
            this.attackTargetGetter = attackTargetGetter;
            this.turret = unit.transform.FindChild("Turret").gameObject;
        }

        public void Do()
        {


            throw new NotImplementedException();
        }

        public bool IsComplete()
        {
            throw new NotImplementedException();
        }
    }
}
