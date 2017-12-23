using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Element
{
    class ControllerDebug : IComponent
    {
        public Vector2 Size;
        public Vector2 Position { get; set; }
        private SpriteSheet sprite;
        public List<Buttons> buttons;
        public Dictionary<Buttons, Vector2> positions;
        public Dictionary<Buttons, int> frames;
        private readonly IInput input;

        public ControllerDebug(IInput input)
        {
            this.input = input ?? throw new ArgumentNullException("input");
        }

        public void Initialize()
        {
            Size = new Vector2(100.0f, 75.0f);

            Position = new Vector2();

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
                { Buttons.Y, new Vector2(150, 55) }, // good
                { Buttons.A, new Vector2(150, 109) }, // good
                { Buttons.B, new Vector2(175, 82) }, // good
                { Buttons.X, new Vector2(125, 82) }, // good
                { Buttons.RightShoulder, new Vector2(264, 45) },
                { Buttons.LeftShoulder, new Vector2(45, 45) }, // good
                { Buttons.RightTrigger, new Vector2(226, 40) },
                { Buttons.LeftTrigger, new Vector2(78, 40) }, // good
                { Buttons.RightStick, new Vector2(264, 75) },
                { Buttons.LeftStick, new Vector2(45, 75) }, // good
                { Buttons.Back, new Vector2(112, 45) },
                { Buttons.Start, new Vector2(188, 45) },
                { Buttons.DPadUp, new Vector2(75, 90) },
                { Buttons.DPadDown, new Vector2(75, 116) },
                { Buttons.DPadLeft, new Vector2(58, 103) },
                { Buttons.DPadRight, new Vector2(92, 103) }
            };
        }

        public void LoadContent(ContentManager content)
        {
            sprite = new SpriteSheet(content, "controllerDebug/Xbox360PixelPad", 3, 9);
        }
        
        public void UnloadContent() { }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Buttons button in buttons)
            {
                if (input.GetButtonState(button) == ButtonState.HELD || input.GetButtonState(button) == ButtonState.PRESSED)
                    sprite.Draw(spriteBatch, frames[button], positions[button]);
            }
            
        }
    }
}
