namespace Asteroids.Tech.Input.Intefaces
{
    public interface IInputListener
    {
        float Horizontal { get;}
        float Vertical { get;}
        float Cancel { get;}
        float Fire1 { get;}
        float Acceleration { get;}
        void Initialize(IInputTranslator translator);
        void Shutdown();
    }
}