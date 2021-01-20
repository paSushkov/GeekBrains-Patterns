using System.Collections.Generic;
using System.Linq;

namespace Asteroids.Tech.PlayerLoop
{
    public sealed class PlayerLoopProcessor : IPlayerLoopProcessor
    {
        private event PlayerLoopProcess UpdateHandlers;
        private event PlayerLoopProcess FixedUpdateHandlers;
        private event PlayerLoopProcess LateUpdateHandlers;
        private readonly Dictionary<IPlayerLoopSubscriptionController, IPlayerLoop> loopSubprocessors
            = new Dictionary<IPlayerLoopSubscriptionController, IPlayerLoop>();


        #region IUpdatable implementation

        public void Update(float deltaTime)
        {
            foreach (var subprocessor in loopSubprocessors)
            {
                subprocessor.Value.ProcessUpdate(deltaTime);
            }
            
            UpdateHandlers?.Invoke(deltaTime);
        }

        public void FixedUpdate(float deltaTime)
        {
            foreach (var subprocessor in loopSubprocessors)
            {
                subprocessor.Value.ProcessFixedUpdate(deltaTime);
            }

            FixedUpdateHandlers?.Invoke(deltaTime);
        }     
        
        public void LateUpdate(float deltaTime)
        {
            foreach (var subprocessor in loopSubprocessors)
            {
                subprocessor.Value.ProcessLateUpdate(deltaTime);
            }

            FixedUpdateHandlers?.Invoke(deltaTime);
        }

        #endregion

        
        #region IPlayerLoopProcessor implementation

        public void SubscribeToLoop(IPlayerLoop subProcessor)
        {
            Register(subProcessor);
        }

        public void SubscribeUpdate(PlayerLoopProcess process)
        {
            UpdateHandlers += process;
        }

        public void UnsubscribeFromLoop(IPlayerLoop subProcessor)
        {
            Unregister(subProcessor);
        }

        public void UnsubscribeUpdate(PlayerLoopProcess process)
        {
            UpdateHandlers -= process;
        }

        public void SubscribeFixedUpdate(PlayerLoopProcess process)
        {
            FixedUpdateHandlers += process;
        }

        public void UnsubscribeFixedUpdate(PlayerLoopProcess process)
        {
            FixedUpdateHandlers -= process;
        }

        public void SubscribeLateUpdate(PlayerLoopProcess process)
        {
            LateUpdateHandlers += process;
        }

        public void UnsubscribeLateUpdate(PlayerLoopProcess process)
        {
            LateUpdateHandlers -= process;
        }

        public void Shutdown()
        {
            var subscribers = loopSubprocessors.Keys.ToArray();
            
            for (var i = 0; i < subscribers.Length; i++)
            {
                subscribers[i].UnsubscribeFromLoop();
            }
        }

        #endregion


        #region Private methods
        
        private void Register(IPlayerLoop loopSubprocessor)
        {
            var loopSubprocessorSubscribtionController = loopSubprocessor?.PlayerLoopSubscriptionController;

            if (loopSubprocessor != null && loopSubprocessorSubscribtionController != null &&
                !loopSubprocessors.ContainsKey(loopSubprocessorSubscribtionController))
            {
                loopSubprocessors.Add(loopSubprocessorSubscribtionController, loopSubprocessor);
            }
        }

        private void Unregister(IPlayerLoop loopSubprocessor) 
        {
            var loopSubprocessorSubscribtionController = loopSubprocessor?.PlayerLoopSubscriptionController;

            if (loopSubprocessor != null && loopSubprocessorSubscribtionController != null &&
                !loopSubprocessors.ContainsKey(loopSubprocessorSubscribtionController))
            {
                loopSubprocessors.Remove(loopSubprocessorSubscribtionController);
            }
        }

        #endregion
    }
}