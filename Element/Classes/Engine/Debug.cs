using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Element.Interfaces;


namespace Element
{
    class Debug : IComponent
    {
        private SpriteFont font;
        private List<String> Messages;
        private IContentManager _contentManager;

        public Debug(IContentManager contentManager)
        {
            this._contentManager = contentManager;
            font = _contentManager.GetFont("Arial");
        }

        public void Initialize()
        {

        }

        public void LoadContent(ContentManager content)
        {
            
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime, ref XB1Pad input)
        {
           int a = input.GetButtonState(Buttons.A);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int x = 15;
            int y = 15;

            foreach (String message in Messages)
            {
                spriteBatch.DrawString(font, message, new Vector2(x, y), Color.Black);
                y += 15;
            }
            
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
