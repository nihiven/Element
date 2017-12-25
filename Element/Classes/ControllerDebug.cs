using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Element.Interfaces;

namespace Element.Classes
{
    // TODO: convert buttons to this class
    class ButtonInfo
    {
        public Buttons button;
        public Vector2 position;
        public int frame;

        ButtonInfo(Buttons button, Vector2 position, int frame)
        {
            this.button = button;
            this.position = position;
            this.frame = frame;
        }
    }

    class ControllerDebug : IComponent
    {
        public Vector2 Size;
        public Vector2 Position { get; set; }
        private SpriteSheet sprite;
        public List<Buttons> buttons;
        public Dictionary<Buttons, Vector2> positions;
        public Dictionary<Buttons, int> frames;
        public bool Enabled { get; set; }
        private readonly IInput input;
        private readonly IGraphics graphics;
        private SpriteFont font;

        public ControllerDebug(IInput input, IGraphics graphics)
        {
            this.input = input ?? throw new ArgumentNullException("input");
            this.graphics = graphics ?? throw new ArgumentNullException("graphics");
            this.Enabled = true;
        }

        public void Initialize()
        {
            Size = new Vector2(242.0f, 103.0f);
            Position = new Vector2(graphics.GetViewPortSize().X - Size.X - 5, 5);

            buttons = new List<Buttons>
            {
                Buttons.A,
                Buttons.B,
                Buttons.Y,
                Buttons.X,
                Buttons.RightShoulder,
                Buttons.LeftShoulder,
                Buttons.RightTrigger,
                Buttons.LeftTrigger,
                Buttons.RightStick,
                Buttons.LeftStick,
                Buttons.Back,
                Buttons.Start,
                Buttons.DPadUp,
                Buttons.DPadDown,
                Buttons.DPadLeft,
                Buttons.DPadRight
        };

            frames = new Dictionary<Buttons, int>
            {
                { Buttons.A, 13 },
                { Buttons.B, 10 },
                { Buttons.Y, 12 },
                { Buttons.X, 11 },
                { Buttons.RightShoulder, 1 },
                { Buttons.LeftShoulder, 2 },
                { Buttons.RightTrigger, 3 },
                { Buttons.LeftTrigger, 4 },
                { Buttons.RightStick, 14 },
                { Buttons.LeftStick, 5 },
                { Buttons.Back, 15 },
                { Buttons.Start, 6 },
                { Buttons.DPadUp, 7 },
                { Buttons.DPadDown, 8 },
                { Buttons.DPadLeft, 17 },
                { Buttons.DPadRight, 16 }
            };

            positions = new Dictionary<Buttons, Vector2>
            {
                { Buttons.Y, new Vector2(105, 15) }, // good
                { Buttons.A, new Vector2(105, 69) }, // good
                { Buttons.B, new Vector2(130, 42) }, // good
                { Buttons.X, new Vector2(80, 42) }, // good
                { Buttons.RightShoulder, new Vector2(209, 5) },
                { Buttons.LeftShoulder, new Vector2(0, 5) }, // good
                { Buttons.RightTrigger, new Vector2(176, 0) },
                { Buttons.LeftTrigger, new Vector2(33, 0) }, // good
                { Buttons.RightStick, new Vector2(209, 35) },
                { Buttons.LeftStick, new Vector2(0, 35) }, // good
                { Buttons.Back, new Vector2(67, 5) },
                { Buttons.Start, new Vector2(143, 5) },
                { Buttons.DPadUp, new Vector2(30, 50) },
                { Buttons.DPadDown, new Vector2(30, 76) },
                { Buttons.DPadLeft, new Vector2(13, 63) },
                { Buttons.DPadRight, new Vector2(47, 63) }
            };
        }

        public void LoadContent(ContentManager content)
        {
            sprite = new SpriteSheet(content, "controllerDebug/Xbox360PixelPadtrans", 4, 9);
            font = content.Load<SpriteFont>("Arial");
        }
        
        public void UnloadContent() { }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Enabled)
            { 
                foreach (Buttons button in buttons)
                {
                    if (input.GetButtonState(button) == ButtonState.HELD || input.GetButtonState(button) == ButtonState.PRESSED)
                        sprite.Draw(spriteBatch, frames[button], Position + positions[button]);
                    else
                        sprite.Draw(spriteBatch, frames[button]+18, Position + positions[button]);
                }

                spriteBatch.DrawString(font, input.GetLeftThumbstickVector().X.ToString(), new Vector2(10, 10), Color.Yellow);
                spriteBatch.DrawString(font, input.GetLeftThumbstickVector().Y.ToString(), new Vector2(10, 35), Color.Yellow);
                spriteBatch.DrawString(font, Cardinal.String(input.GetRightThumbstickCardinal()), new Vector2(10, 60), Color.Yellow);
            }
        }
    }
}
