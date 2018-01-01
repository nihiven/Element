using Element.Classes;
using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IGun : IItem
    {
        WeaponModifiers Modifiers { get; }

        // base numbers
        double BaseDamage { get; }
        double BaseVelocity { get; }
        double BaseRange { get; }
        int BaseRPM { get; }
        double BaseRPS { get; }
        double BaseFiringDelay { get; }
        double BaseReloadDelay { get; }
        int BaseMagSize { get; }
        int BaseReserveSize { get; }

        // current status
        int MagCount { get; set; }
        int ReserveCount { get; set; }

        // sprite related
        Vector2 FirePosition { get; }

        // methods
        void Fire(double angle);
        void Update(GameTime gameTime);
        void Draw(SpriteRender spriteRender);
    }
}
