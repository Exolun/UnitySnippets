using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    public enum UnitDisposition
    {
        /// <summary>
        /// Will attack nearby enemies, but does not seek out fights
        /// </summary>
        Defensive,

        /// <summary>
        /// Will attack nearby enemies and chase them until they are out of range or dead
        /// </summary>
        Aggressive,

        /// <summary>
        /// Will not return fire or attack nearby enemies
        /// </summary>
        Passive,
        
        /// <summary>
        /// Will not move unless orders to do otherwise are issued
        /// </summary>
        HoldPosition,

        /// <summary>
        /// Under orders to attack a target
        /// </summary>
        AttackTarget
    }
}
