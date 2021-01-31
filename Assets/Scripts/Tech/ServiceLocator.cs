using System;
using System.Collections.Generic;

namespace Asteroids.Tech
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> serviceСontainer = new Dictionary<Type, object>();

        public static void SetService<T>(T value) where T : class
        {
            var typeValue = typeof(T);
            if (!serviceСontainer.ContainsKey(typeValue))
            {
                serviceСontainer[typeValue] = value;
            }
        }

        public static T Resolve<T>()
        {
            var type = typeof(T);
            if (serviceСontainer.ContainsKey(type))
            {
                return (T) serviceСontainer[type];
            }

            return default;
        }
    }
}