using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Element.Interfaces
{
    public interface IDraw
    {
        Vector2 Position { get; }
        AnimatedSprite AnimatedSprite { get; }
    }
}
