using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Element.Classes
{
    public class Weapon : IGun
    {
        public Vector2 Position { get; set; }
        public Vector2 FirePosition { get; set; } // position at which the bullets are created

        public AnimatedSprite AnimatedSprite { get; set; }

        public int Width => throw new NotImplementedException();
        public int Height => throw new NotImplementedException();
        public float Acceleration => throw new NotImplementedException();
        public float Velocity => throw new NotImplementedException();

        public float BaseDamage { get; set; }
        public float BaseVelocity { get; set; }
        public int MagCount { get; set; }
        public int ReserveCount { get; set; }

        public string Name { get; set; }
        public Guid Guid { get; set; }

        private readonly IInput _input;
        private readonly IContentManager _contentManager;

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

        public void Draw(SpriteBatch spriteBatch)
        {
            this.AnimatedSprite.Draw(spriteBatch, this.Position);
        }

        public void Attach(Vector2 position)
        {
            this.Position = position;
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
