using System;
using System.Collections.Generic;
using Asteroids.Tech.Input;
using Asteroids.Tech.Input.Handlers;
using Asteroids.Tech.Input.Intefaces;
using Asteroids.Tech.PlayerLoop;
using UnityEngine;

namespace Tech.Input
{
    public class InputTranslator : IInputTranslator, IPlayerLoop 
    {
                private readonly Dictionary<string, AxisInput> _axesInputs = new Dictionary<string, AxisInput>();
        private readonly List<string> blacklist = new List<string>();

        #region IInputTranslator implementation

        public void Initialize(IPlayerLoopProcessor loopProcessor)
        {
            PlayerLoopSubscriptionController?.Initialize(this, loopProcessor);
            PlayerLoopSubscriptionController?.SubscribeToLoop();
        }

        public void Shutdown()
        {
            PlayerLoopSubscriptionController?.Shutdown();
            blacklist.Clear();
            _axesInputs.Clear();
        }

        public void SubscribeToAxisInput(string axisName, AxisInputHandler handler)
        {
            if (blacklist.Contains(axisName))
                return;

            try
            {
                UnityEngine.Input.GetAxis(axisName);
            }
            catch (ArgumentException)
            {
                blacklist.Add(axisName);
                Debug.LogError($"Requested input from unregistered axis. \"{axisName}\" is added to black ist");
                return;
            }
            
            if (!_axesInputs.ContainsKey(axisName))
                _axesInputs.Add(axisName, new AxisInput());
            
            if (_axesInputs.TryGetValue(axisName, out var axisInput))
                axisInput.AddListener(handler);
        }

        public void UnsubscribeFromAxisInput(string axisName, AxisInputHandler handler)
        {
            if (_axesInputs.TryGetValue(axisName, out var axisInput))
                axisInput.RemoveListener(handler);
        }

        #endregion

        
        #region IPlayerLoop implementation

        public IPlayerLoopSubscriptionController PlayerLoopSubscriptionController { get; private set; } =
            new PlayerLoopSubscriptionController();

        public void ProcessUpdate(float deltaTime)
        {
            foreach (var axis in _axesInputs)
            {
                var value = UnityEngine.Input.GetAxis(axis.Key);
                axis.Value.Broadcast(value);
            }
        }

        public void ProcessFixedUpdate(float fixedDeltaTime)
        {
        }

        public void ProcessLateUpdate(float fixedDeltaTime)
        {
        }

        #endregion
    }
}