using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Element
{
    public static class ButtonState
    {
        public const int NONE = 0;
        public const int PRESSED = 1;
        public const int HELD = 2;
        public const int RELEASED = 4;
    }

    public static class ButtonStateText
    {
        //public const string;
    }

    public class XB1Pad : IComponent, IInput
    {
        private GamePadState currentState;
        private GamePadState previousState;
        private List<Buttons> buttons;
        private Dictionary<Buttons, int> states;

        public void Initialize()
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
                states[button] = ButtonState.NONE;

            previousState = GamePad.GetState(PlayerIndex.One);
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
                {
                    updateState(button, currentState.IsButtonDown(button));
                }

                previousState = currentState;
            }
        }

        private void updateState(Buttons button, bool isButtonDown)
        {
            if (isButtonDown)
            {
                switch (states[button])
                {
                    case ButtonState.NONE:
                        states[button] = ButtonState.PRESSED;
                        break;
                    case ButtonState.PRESSED:
                        states[button] = ButtonState.HELD;
                        break;
                    case ButtonState.HELD:
                        break;
                    case ButtonState.RELEASED:
                        states[button] = ButtonState.PRESSED;
                        break;
                    default:
                        throw new System.InvalidOperationException("button has not been initialized");
                }
            }
            else
            {
                switch (states[button])
                {
                    case ButtonState.NONE:
                        break;
                    case ButtonState.PRESSED:
                        states[button] = ButtonState.RELEASED;
                        break;
                    case ButtonState.HELD:
                        states[button] = ButtonState.RELEASED;
                        break;
                    case ButtonState.RELEASED:
                        states[button] = ButtonState.NONE;
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

        public void Draw(SpriteBatch spriteBatch) { }

        public void LoadContent(ContentManager content) { }

        public void UnloadContent() { }
    }
}
