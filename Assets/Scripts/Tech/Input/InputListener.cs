using Asteroids.Tech.Input.Intefaces;
using UnityEngine;

namespace Tech.Input
{
    [CreateAssetMenu(menuName = "Input/InputListener")]
    public class InputListener : ScriptableObject, IInputListener
    {
        [SerializeField] private string horizontalAxis = "Horizontal";
        [SerializeField] private string verticalAxis = "Vertical";
        [SerializeField] private string cancelAxis = "Cancel";
        [SerializeField] private string fireAxis = "Fire1";
        [SerializeField] private string accelerationAxis = "Acceleration";
        private IInputTranslator translator; 
            
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public float Cancel { get; private set; }
        public float Fire1 { get; private set; }
        public float Acceleration { get; private set; }

        
        public void Initialize(IInputTranslator translator)
        {
            this.translator = translator;
            translator.SubscribeToAxisInput(horizontalAxis, GetHorizontal);
            translator.SubscribeToAxisInput(verticalAxis, GetVertical);
            translator.SubscribeToAxisInput(cancelAxis, GetCancel);
            translator.SubscribeToAxisInput(fireAxis, GetFire1);
            translator.SubscribeToAxisInput(accelerationAxis, GetAcceleration);
        }

        public void Shutdown()
        {
            if (translator == null) return;
            translator.UnsubscribeFromAxisInput(horizontalAxis, GetHorizontal);
            translator.UnsubscribeFromAxisInput(verticalAxis, GetVertical);
            translator.UnsubscribeFromAxisInput(cancelAxis, GetCancel);
            translator.UnsubscribeFromAxisInput(fireAxis, GetFire1);
            translator.UnsubscribeFromAxisInput(accelerationAxis, GetAcceleration);
        }

        private void GetHorizontal(float value)
        {
            Horizontal = value;
        }

        private void GetVertical(float value)
        {
            Vertical = value;
        }

        private void GetCancel(float value)
        {
            Cancel = value;
        }

        private void GetFire1(float value)
        {
            Fire1 = value;
        }

        private void GetAcceleration(float value)
        {
            Acceleration = value;
        }
    }
}