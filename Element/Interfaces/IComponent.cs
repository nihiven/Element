using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Element.Interfaces
{
    public interface IComponent
    {
        void Initialize();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void LoadContent(ContentManager content);
        void UnloadContent();
    }
}
