using System.Collections.Generic;
using System.Linq;
using Asteroids.Tech.PlayerLoop;
using UnityEngine;

namespace Asteroids.Characteristics
{
    public class StatHolder : IStatHolder
    {
        #region Private data

        private readonly Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();
        private readonly List<IUpdatable> selfDynamicStats = new List<IUpdatable>();

        #endregion

        
        #region Properties

        public StatType[] ActiveStats => stats.Keys.ToArray();

        #endregion
        
        
        #region Stat management

        public bool TryGetStat(StatType type, out Stat stat)
        {
            if (stats.ContainsKey(type))
            {
                stat = stats [type];
                return true;
            }

            stat = null;
            return false;
        }
        
        public void AddStat(StatType type, Stat stat)
        {
            if (stats.ContainsKey(type))
            {
                Debug.LogWarning($"Attempt to add resource of type, which is already presented in {GetType().Name}");
                return;
            }
            
            stats?.Add(type, stat);
            stat.Initialize();
            
            if (stat is IUpdatable processor)
            {
                selfDynamicStats.Add(processor);
            }
        }
        
        public void RemoveStat(StatType type)
        {
            if (stats.TryGetValue(type, out var stat))
            {
                if (stat is IUpdatable dynamic && selfDynamicStats.Contains(dynamic))
                {
                    selfDynamicStats.Remove(dynamic);
                }

                stats.Remove(type);
            }
            else
            {
                Debug.LogWarning($"Attempt to remove resource of type, which is not presented in {GetType().Name}");
            }
        }
    
        public void Clear()
        {
            stats.Clear();
            selfDynamicStats.Clear();
        }

        #endregion
        
        
        #region IUpdatable implementation

        public void Update(float deltaTime)
        {
        }

        public void FixedUpdate(float deltaTime)
        {
            foreach (var processor in selfDynamicStats)
                processor.FixedUpdate(deltaTime);
        }

        public void LateUpdate(float deltaTime)
        {
        }

        #endregion
    }
}