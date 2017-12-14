using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Element
{
    interface IActor
    {
        void LoadContent(ContentManager content);
        void UnloadContent();

        void Update(GameTime gameTime);

        //  void Think(float seconds);
        //  void UpdatePhysics(float seconds);

        void Draw(SpriteBatch spriteBatch);

    //    void Touched(IActor by);

        Vector2 Position { get; }
     //   Rectangle BoundingBox { get; }
    }
}
