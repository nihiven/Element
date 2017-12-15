using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Element
{
    interface IActor
    {
        void LoadContent(ContentManager content);
        void UnloadContent();
        void Update(GameTime gameTime, ref XB1Pad input);
        void Draw(SpriteBatch spriteBatch);
    }
}
