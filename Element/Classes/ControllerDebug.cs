using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Element
{
    class ControllerDebug : IComponent, IContent, IInputHandler, IDrawHandler
    {
        public Vector2 Position;
        private SpriteSheet sprite;
        public List<Buttons> buttons;
        public Dictionary<Buttons, int> states;
        public Dictionary<Buttons, Vector2> positions;

        public void Initialize()
        {
            Position = new Vector2();

            buttons = new List<Buttons>();
            buttons.Add(Buttons.A);

            states = new Dictionary<Buttons, int>();
            foreach(Buttons button in buttons)
            {
                states.Add(button, ButtonState.NONE);
            }

            positions = new Dictionary<Buttons, Vector2>();
            positions.Add(Buttons.A, new Vector2(50, 50));
        }

        public void LoadContent(ContentManager content)
        {
            sprite = new SpriteSheet(content, "controllerDebug/Xbox360PixelPad", 3, 9);
        }

        public void UnloadContent(ContentManager content)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Input(XB1Pad input)
        {
            foreach (Buttons button in buttons)
            {
                states[button] = input.GetButtonState(button);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Buttons button in buttons)
            {
                if (states[button] == ButtonState.HELD || states[button] == ButtonState.PRESSED)
                {
                    sprite.Draw(spriteBatch, 13, positions[button]);
                }
            }
            
        }

    }
}
