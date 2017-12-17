using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Element
{
    class SoundEffects : IActor
    {
        public Dictionary<string, SoundEffect> soundEffects;

        public void Initialize()
        {
            soundEffects = new Dictionary<string, SoundEffect>();
        }

        public void Update(GameTime gameTime, ref XB1Pad input)
        {

            if (input.GetButtonState(Buttons.A) == ButtonState.PRESSED)
            {
                soundEffects["button"].Play(0.2f, 0.0f, -0.25f);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void LoadContent(ContentManager content)
        {
            soundEffects.Add("button", content.Load<SoundEffect>("soundEffects/button"));
        }

        public void UnloadContent()
        {

        }
    }

}
