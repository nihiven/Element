using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Element
{
    class Sprite : IDrawHandler
    {
        public Vector2 Position { get; set; }
        public int Width { get; set; } // width of the tile map in pixels
        public int Height { get; set; } // height of the tile map in pixels
        public Rectangle rectangle;
        private SpriteSheet sprite;

        Sprite()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.Texture, Position, rectangle, Color.White);
        }
    }
}
