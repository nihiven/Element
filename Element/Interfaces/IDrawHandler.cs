using Microsoft.Xna.Framework;


namespace Element
{
    interface IDrawHandler
    {
        int DrawOrder { get; }
        bool Visible { get; }
        AnimatedSprite sprite { get; }

        void Draw(GameTime gameTime);
    }
}
