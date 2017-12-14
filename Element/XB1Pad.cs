using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Element
{
    class XB1Pad
    {
        private GamePadState gamePadState;

        public XB1Pad()
        {
            
        }

        void update(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(PlayerIndex.One);
        }
    }
}
