using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Element.Interfaces;

namespace Element
{
    class GraphicsManager : GraphicsDeviceManager, IGraphics
    {
        public GraphicsManager(Game game) : base(game) { }

        public Vector2 GetViewPortSize()
        {
            return new Vector2(base.GraphicsDevice.Viewport.Width, base.GraphicsDevice.Viewport.Height);
        }
    }
}
