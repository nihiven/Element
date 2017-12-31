using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IGun : IItem //TODO: , IDoesDamage?
    {
        // base numbers
        float BaseDamage { get; }
        float BaseVelocity { get; }
        float BaseRange { get; }
        int BaseRPM { get; }
        int BaseMagSize { get; }
        int BaseReserveSize { get; }

        // sprite related
        Vector2 FirePosition { get; }

        // methods
        void Fire(double angle);
        void Update(GameTime gameTime);
        void Draw(SpriteRender spriteRender);
    }
}
