using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IComponent
    {
        void Initialize();
        void Update(GameTime gameTime);
        void Draw(SpriteRender spriteRender);
        void LoadContent(ContentManager content);
        void UnloadContent();
    }
}
