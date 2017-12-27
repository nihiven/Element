using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Element
{
    public class AnimatedSprite
    {
        public int Width { get; set; } // width of the tile map in pixels
        public int Height { get; set; } // height of the tile map in pixels
        public int EndFrame { get; set; } // ending frame of this animation (start + number) - 1 
        public int CurrentFrame { get; set; } // the tile map frame the animation is currently on

        public string CurrentAnimationName = null;
        public Dictionary<string, Animation> Animations = new Dictionary<string, Animation>();

        private void Initialize()
        {
            // calculated
            this.CurrentFrame = this.Anim.StartFrame;
            this.Width = this.Anim.Sprites.Texture.Width / this.Anim.Sprites.Columns;
            this.Height = this.Anim.Sprites.Texture.Height / this.Anim.Sprites.Rows;
            this.EndFrame = this.Anim.StartFrame + (this.Anim.FrameCount - 1); // -1 since startFrame counts as animationFrame    
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = (int)(gameTime.TotalGameTime.TotalSeconds / this.Anim.SecondsPerFrame);
            CurrentFrame = (this.Anim.StartFrame - 1) + this.Anim.FrameOrder[this.CurrentFrame % this.Anim.FrameCount];
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            // set the destination within the tile map, passed to the spritebatch draw
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, this.Width, this.Height);

            // draw the selected frame
            spriteBatch.Draw(this.Anim.Sprites.Texture, destinationRectangle, this.CurrentSource(), Color.White);
        }

        public int CurrentRow()
        {
            // row will be the int result of the current frame divided by the number of columns
            return (int)((float)this.CurrentFrame / (float)this.Anim.Sprites.Columns);
        }

        public int CurrentColumn()
        {
            // column will be the remainder of the current frame divided by the number of columns (mod)
            return this.CurrentFrame % this.Anim.Sprites.Columns;
        }

        public Rectangle CurrentSource()
        {
            // the source within the tile map, passed to the spritebatch draw
            return new Rectangle(this.Width * this.CurrentColumn(), this.Height * this.CurrentRow(), this.Width, this.Height);
        }

        public void SetAnimation(string animationName)
        {
            if (Animations.ContainsKey(animationName))
            {
                if (CurrentAnimationName != animationName)
                {
                    CurrentAnimationName = animationName;
                    this.Initialize();
                }
            }
            else
                throw new ArgumentOutOfRangeException("animationName");
        }

        public void AddAnimation(Animation animation, bool makeActive = false)
        {
            this.Animations.Add(animation.Name, animation);

            if (makeActive || CurrentAnimationName == null)
                SetAnimation(animation.Name);
        }

        public Animation Anim
        {
            get { return this.Animations[CurrentAnimationName]; }
        }
    }
}
