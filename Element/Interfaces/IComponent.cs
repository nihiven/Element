using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IComponent
    {
        // properties
        bool Enabled { get; }

        // methods
        void Initialize();
        void Update(GameTime gameTime);
        void Draw(SpriteRender spriteRender);
        void LoadContent(ContentManager content);
        void UnloadContent();
    }
}
