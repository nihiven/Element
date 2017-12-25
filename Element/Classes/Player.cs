using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Element.Classes;
using Element.Interfaces;

namespace Element
{
    /// <summary>
    /// This will hold all player logic and controls.
    /// </summary>
    public class Player : IComponent
    {
        public AnimatedSprite AnimatedSprite { get; set; }
        public Vector2 Position { get; set;  }
        public bool Active { get; set; }
        public int Health;
        public float Velocity { get;  }
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
            get { return this.AnimatedSprite.Width; }
        }

        /// <summary>
        /// Returns player height, assumes height of the player is the height of the player texture.
        /// </summary>
        public int Height
        {
            get { return this.AnimatedSprite.Height; }
        }
        
        public void Initialize()
        {
            this.Position = new Vector2();
            this.Active = true;
            this.Health = 100;
        }


        public void LoadContent(ContentManager content)
        {
            SpriteSheet spriteSheet = new SpriteSheet(content, "female_walkcycle", 4, 9);
            Animation walkUp = new Animation("female_walk_up", spriteSheet, 1, 9, FPS.TEN);
            Animation walkDown = new Animation("female_walk_down", spriteSheet, 19, 9, FPS.TEN);
            Animation walkLeft = new Animation("female_walk_left", spriteSheet, 10, 9, FPS.TEN);
            Animation walkRight = new Animation("female_walk_right", spriteSheet, 28, 9, FPS.TEN);

            this.AnimatedSprite = new AnimatedSprite();
            this.AnimatedSprite.AddAnimation(walkUp);
            this.AnimatedSprite.AddAnimation(walkDown);
            this.AnimatedSprite.AddAnimation(walkLeft);
            this.AnimatedSprite.AddAnimation(walkRight);
        }


        public void UnloadContent()
        {
            // unload unmanaged content
        }

        /// <summary>
        /// Update the player character and all children actors
        /// </summary>
        public void Update(GameTime gameTime)
        {
            this.Position += new Vector2(input.GetLeftThumbstickVector().X, -input.GetLeftThumbstickVector().Y) + new Vector2(input.GetRightThumbstickVector().X, -input.GetRightThumbstickVector().Y);


            int cardinal = input.GetRightThumbstickCardinal();

            if (cardinal == Cardinal.North)
                this.AnimatedSprite.SetAnimation("female_walk_up");

            if (cardinal == Cardinal.South)
                this.AnimatedSprite.SetAnimation("female_walk_down");

            if (cardinal == Cardinal.East)
                this.AnimatedSprite.SetAnimation("female_walk_right");

            if (cardinal == Cardinal.West)
                this.AnimatedSprite.SetAnimation("female_walk_left");

            this.AnimatedSprite.Update(gameTime);
        }

        /// <summary>
        /// Draw the player character and child actors
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            this.AnimatedSprite.Draw(spriteBatch, Position);
        }
    }
}
