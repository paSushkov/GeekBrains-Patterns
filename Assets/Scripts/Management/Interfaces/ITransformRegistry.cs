using Asteroids.Common;
using UnityEngine;

namespace Asteroids.Management
{
    public interface ITransformRegistry
    {
        void RegisterTransform(IHaveTransform owner, Transform property);
        void DismissTransform(IHaveTransform owner);
        bool TryGetOwner(Transform transform, out IHaveTransform owner);
    }
}