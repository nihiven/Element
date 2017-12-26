using System;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Element.DestinyGuns
{
    interface IDraw
    {
        Vector2 Position { get; }
        AnimatedSprite animatedSprite { get; }
        void Draw(SpriteBatch spriteBatch);
    }

    interface IGun : IComponent, IDraw, IMoveable
    {

    }

    class JadeRabbit : IGun
    {
        public Vector2 Position => throw new NotImplementedException();
        public Vector2 MinPosition => throw new NotImplementedException();
        public Vector2 MaxPosition => throw new NotImplementedException();
        public AnimatedSprite animatedSprite => throw new NotImplementedException();

        public int Width => throw new NotImplementedException();
        public int Height => throw new NotImplementedException();
        public float Acceleration => throw new NotImplementedException();
        public float Velocity => throw new NotImplementedException();

        private IInput input;

        JadeRabbit(IInput input)
        {
            this.input = input ?? throw new ArgumentNullException("input");
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
