using Element.Classes;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Element.DestinyGuns
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

        private IInput input;

        public Weapon(IInput input, Guid guid, string itemId, string name, Vector2 spawnLocation)
        {
            this.input = input ?? throw new ArgumentNullException("input");
            this.Name = name;
            this.Guid = guid;
            this.Position = spawnLocation;
            this.Initialize();

            Dictionary<string, AnimatedSprite> anim = (Dictionary<string, AnimatedSprite>)ObjectManager.Get("animatedSprites");
            this.AnimatedSprite = anim[itemId];
        }

        public void Initialize()
        {
            this.BaseDamage = 10;
            this.BaseVelocity = 10;
            this.MagCount = 19;
            this.ReserveCount = 190;
        }

        public void Update(GameTime gameTime)
        {
            AnimatedSprite.Update(gameTime);

            if (input.GetButtonState(Buttons.RightTrigger) == ButtonState.Pressed)
            {
                // TODO: KEY MAPPING
                // get the angle to fire at from the right thumbstick
                double angle = Utilities.GetAngleFromVectors(new Vector2(0, 0), input.GetRightThumbstickVector());
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
