using Asteroids.Tech;
using Asteroids.Tech.Input.Intefaces;
using Asteroids.Tech.PlayerLoop;
using UnityEngine;

namespace Asteroids
{
    internal sealed class Player : IPlayerLoop
    {
        private IPlayerLoopProcessor playerLoopProcessor;
        private IInputListener inputListener;
        private Camera camera;
        private Transform cameraTransform; 
        private Ship ship;
        private Vector3 offset = new Vector3(0, 0, -10);

        public void Initialize(Ship playerShip, Camera camera)
        {
            inputListener = ServiceLocator.Resolve<IInputListener>();
            playerLoopProcessor = ServiceLocator.Resolve<IPlayerLoopProcessor>();
            PlayerLoopSubscriptionController = new PlayerLoopSubscriptionController();
            PlayerLoopSubscriptionController.Initialize(this, playerLoopProcessor);
            PlayerLoopSubscriptionController.SubscribeToLoop();
            ship = playerShip;
            this.camera = camera;
            cameraTransform = camera.transform;
            cameraTransform.SetParent(null);
        }

        public void Shutdown()
        {
            PlayerLoopSubscriptionController.Shutdown();
        }


        #region IPlayerLoop implementation

        public IPlayerLoopSubscriptionController PlayerLoopSubscriptionController { get; private set; }

        public void ProcessUpdate(float deltaTime)
        {

        }

        public void ProcessFixedUpdate(float fixedDeltaTime)
        {
            ship.StatHolder.FixedUpdate(fixedDeltaTime);
            
            if (ship.IsAlive)
            {
                var direction = Input.mousePosition - camera.WorldToScreenPoint(ship.GameTransform.position);
                ship.Rotation(direction);
            }

            if (inputListener.Horizontal > 0 || inputListener.Vertical >0)
                ship.Move(inputListener.Horizontal, inputListener.Vertical, fixedDeltaTime);


            if (inputListener.Acceleration > 0)
                ship.AddAcceleration();
            else 
                ship.RemoveAcceleration();


            if (inputListener.Fire1>0)
                ship.Shoot();
        }

        public void ProcessLateUpdate(float fixedDeltaTime)
        {
            if (ship.IsAlive)
            {
                cameraTransform.position = ship.GameTransform.position + offset;
                cameraTransform.rotation = ship.GameTransform.rotation;
            }

        }

        #endregion
        
    }
}