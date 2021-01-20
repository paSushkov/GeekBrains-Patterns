using System.Collections.Generic;
using System.Linq;
using Asteroids.Common;
using UnityEngine;

namespace Asteroids.Management
{
    public class TransformRegistry : ITransformRegistry
    {
        #region Private data

        private Dictionary<IHaveTransform, Transform> transformRegistry = new Dictionary<IHaveTransform, Transform>();

        #endregion
        
        
        #region ITransformRegistry implementation

        public void RegisterTransform(IHaveTransform owner, Transform property)
        {
            if (!transformRegistry.ContainsKey(owner))
                transformRegistry.Add(owner, property);
        }

        public void DismissTransform(IHaveTransform owner)
        {
            if (!transformRegistry.ContainsKey(owner)) 
                return;
            
            var transform = transformRegistry[owner];
            if (transform)
                Object.Destroy(transform.gameObject);
                    
            transformRegistry.Remove(owner);
        }

        public bool TryGetOwner(Transform transform, out IHaveTransform owner)
        {
            if (transformRegistry.ContainsValue(transform))
            {
                var temp = transformRegistry.FirstOrDefault(pair => pair.Value == transform);
                owner = temp.Key;
                return owner!=null;
            }

            owner = null;
            return false;
        }
        
        #endregion

    }
}