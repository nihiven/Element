using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Element
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartFrame { get; set; }
        public int AnimationFrames { get; set; }
        public int EndFrame { get; set; }
        private int currentFrame;
        public double TimePerFrame;

        // TODO: Need to allow to set frame animation order such as [1, 2, 1, 3]

        public AnimatedSprite(Texture2D texture, int rows, int columns, int startFrame, int animationFrames, double timePerFrame = 0.33)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            StartFrame = startFrame;
            AnimationFrames = animationFrames;
            currentFrame = startFrame;
            Width = Texture.Width / Columns;
            Height = Texture.Height / Rows;
            EndFrame = startFrame + (animationFrames - 1); // -1 since startFrame counts as animationFrame
        }

        public void Update(GameTime gameTime)
        {
            double timePerFrame = 0.33; // three times a second

            currentFrame = (int)(gameTime.TotalGameTime.TotalSeconds / timePerFrame);
            currentFrame = (StartFrame - 1) + (currentFrame % AnimationFrames);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(Width * column, Height * row, Width, Height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, Width, Height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
