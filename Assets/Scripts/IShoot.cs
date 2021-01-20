using UnityEngine;

namespace Asteroids
{
    public interface IShoot
    {
        void Shoot();
        void Shoot(Rigidbody2D bullet, float force);
    }
}