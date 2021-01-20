using Asteroids.Management;
using UnityEngine;

namespace Asteroids.Common
{
    public interface IHaveTransform
    {
        Transform GameTransform { get; }
        ITransformRegistry TransformRegistryBind { get; }

        void RegisterAsTransformOwner();
        void DisposeTransform();

    }
}