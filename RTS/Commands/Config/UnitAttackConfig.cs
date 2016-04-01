using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commands
{
    public class UnitAttackConfig : MonoBehaviour
    {
        /// <summary>
        /// Game object that hosts the particle system that displays the turret flare
        /// </summary>
        public GameObject TurretFlare;

        /// <summary>
        /// Game object representing the position where the turret flare should explode from
        /// </summary>
        public GameObject TurretBone;

        /// <summary>
        /// Game object hosting the particle system for the explosion caused by projectile impacts for this unit
        /// </summary>
        public GameObject ImpactExplosion;

        /// <summary>
        /// Determines firing distance for the unit
        /// </summary>
        public float AttackRange = 500;

        /// <summary>
        /// Delay in milliseconds between attacks for unit
        /// </summary>
        public float AttackDelay = 500;

        /// <summary>
        /// Degrees per second this unit's turret can turn
        /// </summary>
        public float TurretTurningSpeed = 90;

        /// <summary>
        /// The natural relative axis to consider the 'forward' direction for the unit's turret (for aiming)
        /// </summary>
        public string TurretForwardDirection = "left";

        DateTime? lastFireTime = null;

        public void FireIfReady()
        {
            if(lastFireTime == null || DateTime.Now - ((DateTime)lastFireTime) > TimeSpan.FromMilliseconds(AttackDelay))
            {
                this.fire();
                this.lastFireTime = DateTime.Now;
            }
        }

        private void fire()
        {
            var flare = Instantiate(this.TurretFlare, this.TurretBone.transform.position, this.TurretBone.transform.rotation);
            Destroy(flare, this.AttackDelay / 1000);
        }
    }
}
