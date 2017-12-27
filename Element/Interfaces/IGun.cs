
namespace Element.Interfaces
{
    public interface IGun : IItem //TODO: , IDoesDamage?
    {
        float BaseDamage { get; }
        float BaseVelocity { get; }
        int MagCount { get; }
        int ReserveCount { get; }
        void Fire(double angle);
    }
}
