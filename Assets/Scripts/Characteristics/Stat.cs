using UnityEngine;
using System.Collections.Generic;
using Asteroids.Common;
using Asteroids.Effects;


namespace Asteroids.Characteristics
{
    public class Stat
    {
        #region Private data

        private float minValue;
        private float maxValue;
        private float normalValue;
        private float currentValue;
        private List<ExtraValue> extraValues;

        #endregion


        #region Properties

        public float MinValue => minValue;
        public float MaxValue => maxValue;

        public float CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Clamp(value, minValue, maxValue);
                CurrentChanged?.Invoke(currentValue);
            }
        }

        public float DefaultValue
        {
            get => normalValue;
            set => normalValue = Mathf.Clamp(value, minValue, maxValue);
        }

        #endregion


        #region Class life cycles

        public Stat (MinMaxCurrent minMaxCurrent)
        {
            minValue = minMaxCurrent.MinValue;
            SetMaxValue(minMaxCurrent.MaxValue);
            DefaultValue = CurrentValue = minMaxCurrent.CurrentValue;
        }
        
        public Stat(float minValue, float maxValue, float current)
        {
            this.minValue = minValue;
            SetMaxValue(maxValue);
            DefaultValue = CurrentValue = current;
        }

        #endregion


        #region Public methods

        public virtual void Initialize()
        {
            extraValues = new List<ExtraValue>();
            CurrentValue = normalValue;
        }

        public void SetMaxValue(float value)
        {
            if (value < minValue || Mathf.Approximately(value, minValue))
                maxValue = minValue;
            else
                maxValue = value;
            MinMaxChanged?.Invoke(minValue, maxValue);

            if (currentValue > maxValue)
                CurrentValue = maxValue;
        }

        public void SetMinValue(float value)
        {
            if (value > maxValue || Mathf.Approximately(value, maxValue))
                minValue = maxValue;
            else
                minValue = value;
            MinMaxChanged?.Invoke(minValue, maxValue);

            if (currentValue < minValue)
                CurrentValue = minValue;
        }

        public void AddExtraValue(ref ExtraValue extraValue)
        {
            extraValues.Add(extraValue);
            CurrentValue += extraValue.Value;
        }

        public void RemoveExtraValue(ref ExtraValue extraValue)
        {
            if (extraValues.Contains(extraValue))
            {
                extraValues.Remove(extraValue);
            }

            currentValue = normalValue;
            for (var i = 0; i < extraValues.Count; i++)
            {
                currentValue += extraValues[i].Value;
            }
            // To trigger clamp-process and shoot event
            CurrentValue += 0;
        }

        #endregion


        #region Events

        public event MinMaxChanged MinMaxChanged;
        public event ValueChanged CurrentChanged;
        
        #endregion
    }
}