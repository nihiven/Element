using Microsoft.Xna.Framework;
using Element.Interfaces;

namespace Element
{
    class Grafix : GraphicsDeviceManager, IGraphics
    {
        public Grafix(Game game) : base(game) { }
        public Vector2 GetViewPortSize => new Vector2(base.GraphicsDevice.Viewport.Width, base.GraphicsDevice.Viewport.Height);
        public Vector2 GetViewPortCenter => new Vector2(base.GraphicsDevice.Viewport.Width/2, base.GraphicsDevice.Viewport.Height/2);
    }
}
