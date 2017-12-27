using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Element.Interfaces;

namespace Element
{
    class SoundEffects : IComponent
    {
        public Dictionary<string, SoundEffect> soundEffects;
        private readonly IInput input;

        public SoundEffects(IInput input)
        {
            this.input = input ?? throw new ArgumentNullException("input");
        }

        public void Initialize()
        {
            soundEffects = new Dictionary<string, SoundEffect>();
        }

        public void LoadContent(ContentManager content)
        {
            soundEffects.Add("button", content.Load<SoundEffect>("soundEffects/button"));
        }

        public void UnloadContent() { }

        public void Update(GameTime gameTime)
        {
            if (input.GetButtonState(Buttons.A) == ButtonState.Pressed)
            {
                soundEffects["button"].Play(0.2f, 0.0f, -0.20f);
            }
        }

        public void Draw(SpriteBatch spriteBatch) { }
    }
}
