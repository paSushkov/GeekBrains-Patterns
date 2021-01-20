using Asteroids.Characteristics;
using Asteroids.Common;
using Asteroids.Management;
using Asteroids.PlayerData;
using UnityEngine;

namespace Asteroids
{
    internal sealed class Ship : IMove, IRotation, IShoot, IHaveTransform, IHaveStats
    {
        private readonly IMove _moveImplementation;
        private readonly IRotation _rotationImplementation;
        private readonly IShoot shooter;
        private ColliderListener2D colliderListener;
        private Stat healthStat;

        public float Speed => _moveImplementation.Speed;

        public Ship(IMove moveImplementation, IRotation rotationImplementation, IShoot shootImplementation,
            Transform transform, ITransformRegistry transformRegistry, Stat health,
            ShipMarkUp shipGameObjectMarkUp)
        {
            _moveImplementation = moveImplementation;
            _rotationImplementation = rotationImplementation;
            shooter = shootImplementation;
            
            GameTransform = transform;
            TransformRegistryBind = transformRegistry;
            TransformRegistryBind.RegisterTransform(this, GameTransform);

            StatHolder = new StatHolder();
            healthStat = health;
            healthStat.CurrentChanged += ProcessHealthChange;
            StatHolder.AddStat(StatType.Health, healthStat);

            colliderListener = shipGameObjectMarkUp.ColliderListener;
            colliderListener.EnterCollider += ProcessCollisions;
        }

        public void Move(float horizontal, float vertical, float deltaTime)
        {
            _moveImplementation.Move(horizontal, vertical, deltaTime);
        }

        public void Rotation(Vector3 direction)
        {
            _rotationImplementation.Rotation(direction);
        }

        public void AddAcceleration()
        {
            if (_moveImplementation is AccelerationMove accelerationMove)
            {
                accelerationMove.AddAcceleration();
            }
        }

        public void RemoveAcceleration()
        {
            if (_moveImplementation is AccelerationMove accelerationMove)
            {
                accelerationMove.RemoveAcceleration();
            }
        }
        
        private void ProcessCollisions(Collision2D other)
        {
            healthStat.CurrentValue--;
        }

        private void ProcessHealthChange(float currentHealth)
        {
            if (currentHealth <= 0)
                DisposeTransform();
        }

        
        #region IHaveStats implementation

        public IStatHolder StatHolder { get; private set; }

        #endregion
        
        
        #region IHaveTransform implementation

        public Transform GameTransform { get; private set; }
        public ITransformRegistry TransformRegistryBind { get; private set; }

        public void RegisterAsTransformOwner()
        {
            TransformRegistryBind.RegisterTransform(this, GameTransform);
        }

        public void DisposeTransform()
        {
            colliderListener.EnterCollider -= ProcessCollisions;
            TransformRegistryBind.DismissTransform(this);
        }

        #endregion

        
        #region IShoot implementation

        public void Shoot()
        {
            shooter.Shoot();
        }

        public void Shoot(Rigidbody2D bullet, float force)
        {
            shooter.Shoot(bullet, force);
        }

        #endregion

    }
}
