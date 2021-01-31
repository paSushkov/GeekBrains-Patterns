using Asteroids.Tech.PlayerLoop;
using UnityEngine;

namespace Asteroids
{
    internal sealed class Player : IPlayerLoop
    {
        private Camera _camera;
        private Ship ship;

        public void Initialize(IPlayerLoopProcessor playerLoopProcessor, Ship playerShip, Camera camera)
        {
            PlayerLoopSubscriptionController = new PlayerLoopSubscriptionController();
            PlayerLoopSubscriptionController.Initialize(this, playerLoopProcessor);
            PlayerLoopSubscriptionController.SubscribeToLoop();
            ship = playerShip;
            _camera = camera;
        }

        #region IPlayerLoop implementation

        public IPlayerLoopSubscriptionController PlayerLoopSubscriptionController { get; private set; }

        public void ProcessUpdate(float deltaTime)
        {
            var direction = Input.mousePosition - _camera.WorldToScreenPoint(ship.GameTransform.position);
            ship.Rotation(direction);

            ship.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ship.AddAcceleration();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                ship.RemoveAcceleration();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                ship.Shoot();
            }
        }

        public void ProcessFixedUpdate(float fixedDeltaTime)
        {
            ship.StatHolder.FixedUpdate(fixedDeltaTime);
        }

        public void ProcessLateUpdate(float fixedDeltaTime)
        {
        }

        #endregion
    }
}