using UnityEngine;

namespace Asteroids
{
    public class ShipShoot : IShoot
    {
        private Transform[] barrels;
        private Rigidbody2D defaultBullet;
        private float shootForce;


        #region Class life circle

        public ShipShoot(Transform[] barrels, Rigidbody2D defaultBullet, float defaultForce)
        {
            this.barrels = barrels;
            this.defaultBullet = defaultBullet;
            shootForce = defaultForce;
        }

        #endregion
        
        
        #region IShoot implementation

        public void Shoot()
        {
            Shoot(defaultBullet, shootForce);
        }

        public void Shoot(Rigidbody2D bullet, float force)
        {
            foreach (var barrel in barrels)
            {
                var temAmmunition = Object.Instantiate(bullet, barrel.position, barrel.rotation);
                temAmmunition.AddForce(barrel.up * force);
            }
        }
        
        #endregion

    }
}