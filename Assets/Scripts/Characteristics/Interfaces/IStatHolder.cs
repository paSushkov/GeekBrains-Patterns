using Asteroids.Tech.PlayerLoop;

namespace Asteroids.Characteristics
{
    public interface IStatHolder : IUpdatable
    {
        bool TryGetStat(StatType type, out Stat stat);
        void AddStat(StatType type, Stat stat);
        void RemoveStat(StatType type);
        void Clear();
    }
}