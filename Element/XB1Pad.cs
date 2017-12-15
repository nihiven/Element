using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Element
{
    public static class ButtonState
    {
        public static double PRESSED = 1;
        public static double HELD = 2;
        public static double RELEASED = 4;
    }

    public class XB1Pad : IComponent
    {
        private GamePadState currentState;
        private GamePadState previousState;
        public List<Buttons> ButtonState; // right here

        public XB1Pad()
        {
            
        }

        public void Initialize()
        {
            ButtonState = new List<Buttons>();
            previousState = GamePad.GetState(PlayerIndex.One);
        }

        public void Update(GameTime gameTime)
        {
            // Check the device for Player One
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            currentState = GamePad.GetState(PlayerIndex.One);


            previousState = currentState;
        }

        public bool 
    }
}
