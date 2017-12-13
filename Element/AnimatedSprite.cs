using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Element
{
    public class AnimatedSprite
    {
        public Animation Anim { get; set; } // tile map
        public int Width { get; set; } // width of the tile map in pixels
        public int Height { get; set; } // height of the tile map in pixels
        public int EndFrame { get; set; } // ending frame of this animation (start + number) - 1 
        public int CurrentFrame { get; set; } // the tile map frame the animation is currently on

        public AnimatedSprite(Animation animation)
        {
            // calculated
            Anim = animation;
            CurrentFrame = Anim.StartFrame;
            Width = Anim.Sprites.Texture.Width / Anim.Sprites.Columns;
            Height = Anim.Sprites.Texture.Height / Anim.Sprites.Rows;
            EndFrame = Anim.StartFrame + (Anim.FrameCount - 1); // -1 since startFrame counts as animationFrame    
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = (int)(gameTime.TotalGameTime.TotalSeconds / Anim.SecondsPerFrame);
            CurrentFrame = (Anim.StartFrame - 1) + Anim.FrameOrder[CurrentFrame % Anim.FrameCount];
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            // set the destination within the tile map, passed to the spritebatch draw
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, Width, Height);

            // draw the selected frame
            spriteBatch.Draw(Anim.Sprites.Texture, destinationRectangle, CurrentSource(), Color.White);
        }

        public int CurrentRow()
        {
            // row will be the int result of the current frame divided by the number of columns
            return (int)((float)CurrentFrame / (float)Anim.Sprites.Columns);
        }

        public int CurrentColumn()
        {
            // column will be the remainder of the current frame divided by the number of columns (mod)
            return CurrentFrame % Anim.Sprites.Columns;
        }

        public Rectangle CurrentSource()
        {
            // the source within the tile map, passed to the spritebatch draw
            return new Rectangle(Width * CurrentColumn(), Height * CurrentRow(), Width, Height);
        }
    }
}
