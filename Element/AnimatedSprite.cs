using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Element
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; } // tile map
        public int Rows { get; set; } // number of rows in the tile map
        public int Columns { get; set; } // number of columns in the tile map
        public int Width { get; set; } // width of the tile map in pixels
        public int Height { get; set; } // height of the tile map in pixels
        public int StartFrame { get; set; } // starting frame for this sprite in the tile map
        public int FrameCount { get; set; } // number of frames in this animation
        public int EndFrame { get; set; } // ending frame of this animation (start + number) - 1 
        public double TimePerFrame { get; set; } // amount of time each frame should be displayed

        private int currentFrame; // the tile map frame the animation is currently on
        private int[] FrameOrder; // determines the order to play the frames from the tile map, values are relative to the first frame, ie: [0, 1, 2, 0, 2, 3]
    

        // TODO: Need to allow to set frame animation order such as [1, 2, 1, 3]

        public AnimatedSprite(Texture2D texture, int rows, int columns, int startFrame, int frameCount, double timePerFrame = 0.33)
        {
            // parameters
            Texture = texture;
            Rows = rows;
            Columns = columns;
            StartFrame = startFrame;
            FrameCount = frameCount;
            TimePerFrame = timePerFrame;

            // calculated
            currentFrame = startFrame;
            Width = Texture.Width / Columns;
            Height = Texture.Height / Rows;
            EndFrame = startFrame + (frameCount - 1); // -1 since startFrame counts as animationFrame

            FrameOrder = new int[FrameCount];
                for (int i = 0; i < FrameCount; i++)
                    FrameOrder[i] = i;
        
        }

        public AnimatedSprite(Texture2D texture, int rows, int columns, int startFrame, int[] frameOrder, double timePerFrame = 0.33)
        {
            // parameters
            Texture = texture;
            Rows = rows;
            Columns = columns;
            StartFrame = startFrame;
            FrameOrder = frameOrder;
            TimePerFrame = timePerFrame;
            FrameOrder = frameOrder;

            // calculated
            FrameCount = frameOrder.Length;
            currentFrame = startFrame;
            Width = Texture.Width / Columns;
            Height = Texture.Height / Rows;
            EndFrame = startFrame + (FrameCount - 1); // -1 since startFrame counts as animationFrame

        }

        public void Update(GameTime gameTime)
        {
            currentFrame = (int)(gameTime.TotalGameTime.TotalSeconds / TimePerFrame);
            currentFrame = (StartFrame - 1) + FrameOrder[currentFrame % FrameCount];
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            // set the destination within the tile map, passed to the spritebatch draw
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, Width, Height);

            // draw the selected frame
            spriteBatch.Draw(Texture, destinationRectangle, CurrentSource(), Color.White);
        }

        public int CurrentRow()
        {
            // row will be the int result of the current frame divided by the number of columns
            return (int)((float)currentFrame / (float)Columns);
        }

        public int CurrentColumn()
        {
            // column will be the remainder of the current frame divided by the number of columns (mod)
            return currentFrame % Columns;
        }

        public Rectangle CurrentSource()
        {
            // the source within the tile map, passed to the spritebatch draw
            return new Rectangle(Width * CurrentColumn(), Height * CurrentRow(), Width, Height);
        }
    }
}
