using System;
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

        public SpriteSheet(ContentManager contentManager, string contentName, int rows, int columns)
        {
            ContentName = contentName;
            Texture = contentManager.Load<Texture2D>(ContentName);
            Rows = rows;
            Columns = columns;
        }

    }
}
