using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TexturePackerLoader;
using TexturePackerMonoGameDefinitions;

namespace Element.Classes
{
    public class Weapon : IGun
    {
        public Vector2 Position { get; set; }
        public Vector2 FirePosition { get; set; } // position at which the bullets are created

        public AnimatedSprite AnimatedSprite { get; set; }
        public SpriteSheet SpriteSheet { get; set; }
        public int Width => this.AnimatedSprite.Width;
        public int Height => this.AnimatedSprite.Height;
        public float Acceleration { get; set; }
        public float Velocity { get; set; }

        public float BaseDamage { get; set; }
        public float BaseVelocity { get; set; }
        public int MagCount { get; set; }
        public int ReserveCount { get; set; }

        public string Name { get; set; }
        public Guid Guid { get; set; }

        private readonly IInput _input;
        private readonly IContentManager _contentManager;


        //Testing 
        public bool PlayerClose;

        public Weapon(IInput input, IContentManager contentManager, Guid guid, string itemId, string name, Vector2 spawnLocation)
        {
            this._input = input ?? throw new ArgumentNullException("input");
            this._contentManager = contentManager ?? throw new ArgumentNullException("contentManager");

            this.Name = name;
            this.Guid = guid;
            this.Position = spawnLocation;
            this.BaseDamage = 10;
            this.BaseVelocity = 10;
            this.MagCount = 19;
            this.ReserveCount = 190;

            this._contentManager = contentManager;
            this.AnimatedSprite = this._contentManager.GetAnimatedSprite(itemId);
            this.SpriteSheet = this._contentManager.GetSpriteSheet("Guns");
            

            this.PlayerClose = false;
        }

        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            AnimatedSprite.Update(gameTime);

            if (_input.GetButtonState(Buttons.RightTrigger) == ButtonState.Pressed)
            {
                // TODO: KEY MAPPING
                // get the angle to fire at from the right thumbstick
                double angle = Utilities.GetAngleFromVectors(new Vector2(0, 0), _input.GetRightThumbstickVector());
                this.Fire(angle);
            }
        }

        public void Draw(SpriteRender spriteRender)
        {
            this.AnimatedSprite.Draw(spriteRender.spriteBatch, this.Position);
        }

        public void Attach(Vector2 position)
        {
            this.Position = position;
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, AnimatedSprite.Width, AnimatedSprite.Height);
            }
        }

        public void Fire(double angle)
        {
            if (this.MagCount > 0)
            {
                ObjectManager.Add("bullet", new Bullet(this.FirePosition, angle));
                this.MagCount -= 1;
            }
        }
    }
}
