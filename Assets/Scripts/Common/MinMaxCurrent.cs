using System;
using UnityEngine;

namespace Asteroids.Common
{
    [Serializable]
    public struct MinMaxCurrent
    {
        #region Private data

        [SerializeField] private float minValue;
        [SerializeField] private float maxValue;
        [SerializeField] private float currentValue;

        #endregion
        
        
        #region Properties

        public float MinValue => minValue;
        public float MaxValue => maxValue;
        public float CurrentValue => currentValue;
        
        #endregion
        

        #region Class life cycles

        public MinMaxCurrent(float minValue, float maxValue, float currentValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.currentValue = currentValue;

        }

        #endregion
    }
}