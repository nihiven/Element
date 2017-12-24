using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Element.Interfaces;

namespace Element
{
    /// <summary>
    /// This will hold all player logic and controls.
    /// </summary>
    public class Player
    {
        public AnimatedSprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public bool Active { get; set; }
        public int Health;
        private readonly IInput input;

        public Player(IInput input)
        {
            this.input = input ?? throw new ArgumentNullException("input");
        }

        /// <summary>
        /// Returns player width, assumes width of the player is the width of the player texture.
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
        
        public void Initialize()
        {
            Position = new Vector2();
            Active = true;
            Health = 100;
        }


        public void LoadContent(ContentManager content)
        {
            SpriteSheet spriteSheet = new SpriteSheet(content, "female_walkcycle", 4, 9);
            Animation walkAnimation = new Animation("female_walk", spriteSheet, 19, 9, FPS.TEN);
            Sprite = new AnimatedSprite(walkAnimation);
        }


        public void UnloadContent(ContentManager content)
        {
            // what to do here?
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
