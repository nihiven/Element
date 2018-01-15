using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IProjectile : IDraw, IUpdate
    {

    }
}

namespace Element.Classes
{

    public class Bullet : IEntity, IProjectile
    {
        public bool Expired => (_traveled > _range) ? true : false; // expire bullet when it goes beyond it's max range

        public double Angle => _angle;
        public double Velocity => _velocity; // pixels per second
        public double Range => _range; // pixels per second
        public double Traveled => _traveled;
        public Vector2 Position => _position;
        public AnimatedSprite AnimatedSprite => _animatedSprite;

        private double _angle;
        private double _velocity;
        private double _range;
        private double _traveled;
        private Vector2 _position;
        private AnimatedSprite _animatedSprite;

        public Bullet(AnimatedSprite animatedSprite, Vector2 position, double angle, double velocity, double range)
        {
            _position = position;
            _angle = angle;
            _velocity = velocity;
            _range = range;
            _traveled = 0;
            _animatedSprite = animatedSprite;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 angleVector = Utilities.GetVectorFromAngle(_angle);
            double movementFactor = _velocity * gameTime.ElapsedGameTime.TotalSeconds;
            _traveled += movementFactor;
            _position += new Vector2((int)movementFactor, (int)movementFactor) * angleVector;
        }

        public void Draw(SpriteRender spriteRender)
        {
            _animatedSprite.Draw(spriteRender.spriteBatch, _position);
        }

        public void Dispose()
        {
            // TODO: What do we do here?
        }
    }
}
