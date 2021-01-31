using Asteroids.Characteristics;
using Asteroids.Common;
using Asteroids.Management;
using Asteroids.PlayerData;
using Asteroids.Tech;
using UnityEngine;

namespace Asteroids
{
    internal sealed class Ship : IMove, IRotation, IShoot, IHaveTransform, IHaveStats
    {
        public ValueChanged HealthChanged;
        
        private readonly IMove _moveImplementation;
        private readonly IRotation _rotationImplementation;
        private readonly IShoot shooter;
        private ColliderListener2D colliderListener;
        private Stat healthStat;

        public float Speed => _moveImplementation.Speed;
        public bool IsAlive { get; private set; }

        public Ship(IMove moveImplementation, IRotation rotationImplementation, IShoot shootImplementation,
            Transform transform, Stat health,
            ShipMarkUp shipGameObjectMarkUp)
        {
            _moveImplementation = moveImplementation;
            _rotationImplementation = rotationImplementation;
            shooter = shootImplementation;
            
            GameTransform = transform;
            TransformRegistryBind = ServiceLocator.Resolve<ITransformRegistry>();
            TransformRegistryBind.RegisterTransform(this, GameTransform);

            StatHolder = new StatHolder();
            healthStat = health;
            healthStat.CurrentChanged += ProcessHealthChange;
            StatHolder.AddStat(StatType.Health, healthStat);

            colliderListener = shipGameObjectMarkUp.ColliderListener;
            colliderListener.EnterCollider += ProcessCollisions;
            IsAlive = true;
        }

        public void Move(float horizontal, float vertical, float deltaTime)
        {
            if (IsAlive)
                _moveImplementation.Move(horizontal, vertical, deltaTime);
        }

        public void Rotation(Vector3 direction)
        {
            if (IsAlive)
                _rotationImplementation.Rotation(direction);
        }

        public void AddAcceleration()
        {
            if (IsAlive && _moveImplementation is AccelerationMove accelerationMove)
            {
                accelerationMove.AddAcceleration();
            }
        }

        public void RemoveAcceleration()
        {
            if (IsAlive && _moveImplementation is AccelerationMove accelerationMove)
            {
                accelerationMove.RemoveAcceleration();
            }
        }
        
        private void ProcessCollisions(Collision2D other)
        {
            if (IsAlive)
                healthStat.CurrentValue--;
        }

        private void ProcessHealthChange(float currentHealth)
        {
            if (!(currentHealth > 0))
            {
                IsAlive = false;
                DisposeTransform();
            }

            HealthChanged?.Invoke(currentHealth);
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
            if (IsAlive)
                shooter.Shoot();
        }

        public void Shoot(Rigidbody2D bullet, float force)
        {
            if (IsAlive)
                shooter.Shoot(bullet, force);
        }

        #endregion

    }
}
