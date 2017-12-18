using Microsoft.Xna.Framework.Content;

namespace Element
{
    interface IContent
    {
        void LoadContent(ContentManager content);
        void UnloadContent(ContentManager content);
    }
}
