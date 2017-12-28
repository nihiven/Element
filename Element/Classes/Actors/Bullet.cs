using Microsoft.Xna.Framework;
using Element;
using Element.Interfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Element.Classes
{
    class Bullet : IComponent
    {
        public double Angle { get; set; }
        public float Velocity { get; set; } // pixels per second
        public Vector2 Position { get; set; }
        public AnimatedSprite AnimatedSprite { get; set; }

        public Bullet(Vector2 position, double angle)
        {
            this.Position = position;
            this.Angle = angle;
            this.Velocity = 10;
        }

        public void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {
            Vector2 angleVector = Utilities.GetVectorFromAngle(this.Angle);
            double movementFactor = this.Velocity * gameTime.ElapsedGameTime.TotalSeconds;
            this.Position += new Vector2((float)movementFactor, (float)movementFactor);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.AnimatedSprite.Draw(spriteBatch, this.Position);
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void UnloadContent()
        {
            throw new System.NotImplementedException();
        }
    }
}
