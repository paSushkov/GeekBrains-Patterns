using Asteroids.Tech.Input.Handlers;
using Asteroids.Tech.PlayerLoop;

namespace Asteroids.Tech.Input.Intefaces
{
    public interface IInputTranslator
    {
        void SubscribeToAxisInput(string axisName, AxisInputHandler handler);
        void UnsubscribeFromAxisInput(string axisName, AxisInputHandler handler);
        void Initialize(IPlayerLoopProcessor loopProcessor);
        void Shutdown();
    }
}