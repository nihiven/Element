using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using TexturePackerLoader;

namespace Element.Classes
{
    public interface IProjectile
    {

    }

    public class Bullet : IProjectile
    {
        public double Angle { get; set; }
        public double Velocity { get; set; } // pixels per second
        public double Range { get; set; } // pixels per second
        public double Traveled { get; }
        public Vector2 Position { get; set; }
        public AnimatedSprite AnimatedSprite { get; set; }

        private IGun _gun;
        private double _traveled;

        private IContentManager _contentManager;

        public Bullet(IContentManager contentManager, IGun gun, Vector2 position, double angle)
        {
            this._gun = gun ?? throw new ArgumentNullException("gun");
            this._contentManager = contentManager ?? throw new ArgumentNullException("contentManager");

            this.Position = this._gun.FirePosition;
            this.Angle = angle;
            this.Velocity = gun.BaseVelocity;
            this.Range = gun.BaseRange;
            this.Traveled = 0;

            this.AnimatedSprite = _contentManager.GetAnimatedSprite("bullet");
        }

        public void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {
            Vector2 angleVector = Utilities.GetVectorFromAngle(this.Angle);
            double movementFactor = this.Velocity * gameTime.ElapsedGameTime.TotalSeconds;
            this._traveled += movementFactor;
            this.Position += new Vector2((int)movementFactor, (int)movementFactor) * angleVector;
        }

        public void Draw(SpriteRender spriteRender)
        {
            this.AnimatedSprite.Draw(spriteRender.spriteBatch, this.Position);
        }

        public void LoadContent(ContentManager content)
        {
            
        }
    }
}
