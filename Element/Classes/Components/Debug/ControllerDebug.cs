using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Element.Interfaces;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IControllerDebug : IDraw
    {
    }
}

namespace Element.Classes
{
    // TODO: convert buttons to this class
    class ControllerDebug : IControllerDebug
    {
        public Vector2 Size;
        public Vector2 Position { get; set; }
        private SpriteSheetJB sprite;
        private SpriteFont font;
        public List<Buttons> buttons;
        public Dictionary<Buttons, Vector2> positions;
        public Dictionary<Buttons, int> frames;
        public bool Enabled { get; set; }

        private readonly IInput _input;
        private readonly IGraphics _graphics;
        private readonly IContentManager _contentManager;

        public ControllerDebug(IInput input, IContentManager contentManager, IGraphics graphics)
        {
            this._input = input ?? throw new ArgumentNullException("input");
            this._contentManager = contentManager ?? throw new ArgumentNullException("contentManager");
            this._graphics = graphics ?? throw new ArgumentNullException("graphics");

            this.Enabled = true;
            this.sprite = _contentManager.GetSpriteSheetJB("controllerDebug");
            this.font = _contentManager.GetFont("Arial");

            Size = new Vector2(242.0f, 103.0f);
            Position = new Vector2(_graphics.GetViewPortSize.X - Size.X - 5, 5);

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

        public void Draw(SpriteRender spriteRender)
        {
            if (this.Enabled)
            { 
                foreach (Buttons button in buttons)
                {
                    if (_input.GetButtonState(button) == ButtonState.Held || _input.GetButtonState(button) == ButtonState.Pressed)
                        sprite.Draw(spriteRender.spriteBatch, frames[button], Position + positions[button]);
                    else
                        sprite.Draw(spriteRender.spriteBatch, frames[button]+18, Position + positions[button]);
                }

                spriteRender.spriteBatch.DrawString(font, _input.LeftThumbstickVector.X.ToString(), new Vector2(10, 10), Color.Yellow);
                spriteRender.spriteBatch.DrawString(font, _input.LeftThumbstickVector.Y.ToString(), new Vector2(10, 35), Color.Yellow);
                spriteRender.spriteBatch.DrawString(font, Cardinal.String(_input.RightThumbstickCardinal), new Vector2(10, 60), Color.Yellow);
            }
        }
    }
}
