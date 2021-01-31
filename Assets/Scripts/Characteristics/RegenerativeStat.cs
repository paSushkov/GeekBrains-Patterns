using Asteroids.Common;
using Asteroids.Tech.PlayerLoop;

namespace Asteroids.Characteristics
{
    public sealed class RegenerativeStat : Stat, IUpdatable
    {
        #region Events

        public event ValueChanged RegenChanged;

        #endregion


        #region Private data

        private float defaultRegenerationAmount;
        private float currentRegenerationAmount;

        #endregion


        #region Properties

        public float DefaultRegenerationAmount => defaultRegenerationAmount;

        public float CurrentRegenerationAmount
        {
            get => currentRegenerationAmount;
            set
            {
                currentRegenerationAmount = value;
                RegenChanged?.Invoke(currentRegenerationAmount);
            }
        }

        #endregion


        #region Class life cycles

        public RegenerativeStat(
            MinMaxCurrent minMaxCurrent,
            float defaultRegenerationAmount
        ) : base(minMaxCurrent)
        {
            this.defaultRegenerationAmount = currentRegenerationAmount = defaultRegenerationAmount;
        }

        #endregion


        #region Public metods

        public override void Initialize()
        {
            base.Initialize();
            CurrentRegenerationAmount = defaultRegenerationAmount;
        }

        #endregion

        
        #region Private metods

        private void Regenerate(float deltaTime)
        {
            if (currentRegenerationAmount > 0 && CurrentValue < MaxValue ||
                currentRegenerationAmount < 0 && CurrentValue > MinValue)
                CurrentValue += currentRegenerationAmount * deltaTime;
        }

        #endregion


        #region IUpdatable implementation

        public void Update(float deltaTime)
        {
        }

        public void FixedUpdate(float deltaTime)
        {
            Regenerate(deltaTime);
        }

        public void LateUpdate(float deltaTime)
        {
        }

        #endregion
    }
}