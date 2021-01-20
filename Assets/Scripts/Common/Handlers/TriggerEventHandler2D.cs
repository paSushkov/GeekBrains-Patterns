using UnityEngine;

namespace Asteroids.Common
{
    public delegate void TriggerEventHandler2D (Collider2D other);
    public delegate void CollisionEventHandler2D (Collision2D other);
}