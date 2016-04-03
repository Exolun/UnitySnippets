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
        /// Projectile to use when firing
        /// </summary>
        public GameObject Projectile;

        /// <summary>
        /// Determines firing distance for the unit
        /// </summary>
        public float AttackRange = 500;

        /// <summary>
        /// Delay in milliseconds between attacks for unit
        /// </summary>
        public float AttackDelay = 1000;

        /// <summary>
        /// Units per second that projectiles fired move at
        /// </summary>
        public float ProjectileVelocity = 500;

        /// <summary>
        /// Degrees per second this unit's turret can turn
        /// </summary>
        public float TurretTurningSpeed = 90;

        /// <summary>
        /// The natural relative axis to consider the 'forward' direction for the unit's turret (for aiming)
        /// </summary>
        public string TurretForwardDirection = "left";

        DateTime? lastFireTime = null;

        public void FireIfReady(Vector3 direction)
        {
            if(lastFireTime == null || DateTime.Now - ((DateTime)lastFireTime) > TimeSpan.FromMilliseconds(AttackDelay))
            {
                this.fire(direction);
                this.lastFireTime = DateTime.Now;
            }
        }

        private void fire(Vector3 direction)
        {
            var flare = Instantiate(this.TurretFlare, this.TurretBone.transform.position, this.TurretBone.transform.rotation);
            Destroy(flare, this.AttackDelay / 1000);

            var shot = Instantiate(this.Projectile);
            var shotObj = ((GameObject)shot);
            shotObj.transform.position = this.TurretBone.transform.position;
            shotObj.transform.rotation = Quaternion.FromToRotation(-Vector3.back, direction);

            var projController = (shotObj).GetComponent<ProjectileController>();
            projController.Direction = direction;
            projController.Velocity = this.ProjectileVelocity;
            projController.Lifetime = 1.0f;
        }
    }
}
