using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Element.Interfaces;
using TexturePackerLoader;

namespace Element
{
    public static class ButtonState
    {
        public const int None = 0;
        public const int Pressed = 1;
        public const int Held = 2;
        public const int Released = 4;
    }

    public class XB1Pad : IComponent, IInput
    {
        private GamePadState currentState;
        private GamePadState previousState;
        private List<Buttons> buttons;
        private Dictionary<Buttons, int> states;

        public XB1Pad()
        {
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

            states = new Dictionary<Buttons, int>();
            foreach (Buttons button in buttons)
                states[button] = ButtonState.None;

            previousState = GamePad.GetState(PlayerIndex.One);
        }

        public void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {
            // Check the device for Player One
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            // If there a controller attached, handle it
            if (capabilities.IsConnected)
            {
                currentState = GamePad.GetState(PlayerIndex.One);

                foreach (Buttons button in buttons)
                    updateState(button, currentState.IsButtonDown(button));

                previousState = currentState;
            }
        }

        private void updateState(Buttons button, bool isButtonDown)
        {
            if (isButtonDown)
            {
                switch (states[button])
                {
                    case ButtonState.None:
                        states[button] = ButtonState.Pressed;
                        break;
                    case ButtonState.Pressed:
                        states[button] = ButtonState.Held;
                        break;
                    case ButtonState.Held:
                        break;
                    case ButtonState.Released:
                        states[button] = ButtonState.Pressed;
                        break;
                    default:
                        throw new System.InvalidOperationException("button has not been initialized");
                }
            }
            else
            {
                switch (states[button])
                {
                    case ButtonState.None:
                        break;
                    case ButtonState.Pressed:
                        states[button] = ButtonState.Released;
                        break;
                    case ButtonState.Held:
                        states[button] = ButtonState.Released;
                        break;
                    case ButtonState.Released:
                        states[button] = ButtonState.None;
                        break;
                    default:
                        throw new System.InvalidOperationException("button has not been initialized");
                }

            }
        }

        public int GetButtonState(Buttons button)
        {
            if (states.ContainsKey(button))
            {
                return states[button];
            }
            else
            {
                throw new System.ArgumentException("Button is not tracked", "button");
            }
        }

        public Vector2 GetLeftThumbstickVector()
        {
            return currentState.ThumbSticks.Left;
        }

        public Vector2 GetRightThumbstickVector()
        {
            return currentState.ThumbSticks.Left;
        }

        public int GetLeftThumbstickCardinal()
        {
            return Utilities.GetCardinalDirection(currentState.ThumbSticks.Left);
        }

        public int GetRightThumbstickCardinal()
        {
            return Utilities.GetCardinalDirection(currentState.ThumbSticks.Right);
        }

        public void Draw(SpriteRender spriteRender) { }

        public void LoadContent(ContentManager content) { }

        public void UnloadContent() { }
    }
}
