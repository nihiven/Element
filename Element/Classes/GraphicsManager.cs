using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Element.Interfaces;

namespace Element
{
    class GraphicsManager : GraphicsDeviceManager, IComponent, IGraphics
    {
        public GraphicsManager(Game game) : base(game) { }

        public void Draw(SpriteBatch spriteBatch) { }

        public Vector2 GetViewPortSize()
        {
            return new Vector2(base.GraphicsDevice.Viewport.Width, base.GraphicsDevice.Viewport.Height);
        }

        public void Initialize() { }

        public void LoadContent(ContentManager content) { }

        public void UnloadContent() { }

        public void Update(GameTime gameTime) { }


    }
}
