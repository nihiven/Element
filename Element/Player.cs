using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Element
{
    /// <summary>
    /// This will hold all player logic and controls.
    /// </summary>
    public class Player
    {
        public AnimatedSprite Sprite;
        public Vector2 Position;
        public bool Active;
        public int Health;

        /// <summary>
        /// Returns player width, assumes width of the player is the height of the player texture.
        /// </summary>
        public int Width
        {
            get { return Sprite.Width; }
        }

        /// <summary>
        /// Returns player height, assumes height of the player is the height of the player texture.
        /// </summary>
        public int Height
        {
            get { return Sprite.Height; }
        }

        /// <summary>
        /// Set initial values
        /// </summary>
        public void Initialize(ContentManager contentManager, Vector2 position)
        {
            SpriteSheet spriteSheet = new SpriteSheet(contentManager, "female_walkcycle", 4, 9);
            Animation walkAnimation = new Animation("female_walk", spriteSheet, 19, 9, FPS.TEN);
            Sprite = new AnimatedSprite(walkAnimation);
            Position = position;
            Active = true;
            Health = 100;
        }

        /// <summary>
        /// Update the player character and all children actors
        /// </summary>
        public void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
        }

        /// <summary>
        /// Draw the player character and child actors
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position);
        }

    }

}
