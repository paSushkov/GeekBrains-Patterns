namespace Asteroids.Tech.PlayerLoop
{
    public interface IUpdatable
    {
        void Update(float deltaTime);
        void FixedUpdate(float deltaTime);
        void LateUpdate(float deltaTime);
    }
}