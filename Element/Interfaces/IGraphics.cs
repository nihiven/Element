using Microsoft.Xna.Framework;

namespace Element.Interfaces
{
    public interface IGraphics
    {
        Vector2 GetViewPortSize { get; }
        Vector2 GetViewPortCenter { get; }
    }
}
