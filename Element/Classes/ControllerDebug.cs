using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Element
{
    class ControllerDebug : IComponent, IInputHandler, IUpdateable, IDrawHandler
    {
        private Vector2 background;
        private Vector2 buttonA;

        public void Initialize()
        {
            
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

        }

        public void UpdateInput(GameTime gameTime, ref XB1Pad input)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

    }
}
