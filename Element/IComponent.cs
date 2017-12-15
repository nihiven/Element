using Microsoft.Xna.Framework;

namespace Element
{
    interface IComponent
    {
        void Initialize();
        void Update(GameTime gameTime);
    }
}
