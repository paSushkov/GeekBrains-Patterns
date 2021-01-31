using UnityEngine;

namespace Asteroids
{
    internal sealed class AccelerationMove : MoveTransform
    {
        private readonly float _acceleration;

        public bool IsAccelerated { get; private set; }

        public AccelerationMove(Transform transform, float speed, float acceleration) : base(transform, speed)
        {
            _acceleration = acceleration;
        }

        public void AddAcceleration()
        {
            if (IsAccelerated) return;
            Speed += _acceleration;
            IsAccelerated = true;

        }

        public void RemoveAcceleration()
        {
            if (!IsAccelerated) return;
            Speed -= _acceleration;
            IsAccelerated = false;

        }
    }
}
