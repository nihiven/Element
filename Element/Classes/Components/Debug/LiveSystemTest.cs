using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using TexturePackerLoader;

namespace Element.Classes
{
    // kill the individual 'ItemDebug' class and move all in-game testing here

    class LiveSystemTest
    {
        private class TestItem : ICollideable, IMoveable
        {
            // IDrawable
            public bool Enabled => true;
            public Vector2 Position { get => this._position; } 
            public SpriteFrame SpriteFrame => throw new System.NotImplementedException();

            // ICollideable
            public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            // IMoveable
            public Vector2 Direction;
            public float Acceleration => 10;
            public float Velocity => 10;
            public Vector2 MinPosition => new Vector2(0);
            public Vector2 MaxPosition => new Vector2(_graphics.GetViewPortSize.X, _graphics.GetViewPortSize.Y); // TODO: Figure out where to get this. Probably the map component?? for now... screen bounds

            // class
            public int Width => 100;
            public int Height => 100;
            private Vector2 _position;

            // dependencies
            IGraphics _graphics;


            public TestItem(IGraphics graphics)
            {
                _graphics = graphics ?? throw new ArgumentNullException("IGraphics");
            }

            public void Update(GameTime gameTime)
            {
                throw new NotImplementedException();
            }
        }

        public bool Enabled => true;
        

        public void Draw(SpriteRender spriteRender)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public void LoadContent(ContentManager content)
        {
            throw new System.NotImplementedException();
        }

        public void UnloadContent()
        {
            throw new System.NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
