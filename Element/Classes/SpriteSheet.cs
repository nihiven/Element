using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Element
{
    public class SpriteSheet
    {
        public String ContentName { get; set; }
        public Texture2D Texture { get; set; } // tile map
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int FrameWidth;
        public int FrameHeight;

        public SpriteSheet(ContentManager contentManager, string contentName, int rows, int columns)
        {
            ContentName = contentName;
            Texture = contentManager.Load<Texture2D>(ContentName);
            Rows = rows;
            Columns = columns;
            FrameWidth = Texture.Width / Columns;
            FrameHeight = Texture.Height / Rows;
        }

        public void Draw(SpriteBatch spriteBatch, int frame, Vector2 Position)
        {
            spriteBatch.Draw(Texture, Position, rectangle(frame-1), Color.White);
        }

        public Rectangle rectangle(int frame)
        {
            return new Rectangle(FrameWidth * CurrentColumn(frame), FrameHeight * CurrentRow(frame), FrameWidth, FrameHeight);
        }

        public int CurrentRow(int frame)
        {
            // row will be the int result of the current frame divided by the number of columns
            return (int)((float)frame / (float)Columns);
        }

        public int CurrentColumn(int frame)
        {
            // column will be the remainder of the current frame divided by the number of columns (mod)
            return frame % Columns;
        }
    }
}
