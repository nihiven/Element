using Element.Classes;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using System;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IPlayer : IDraw, IUpdate, ICollideable, IMoveable
    {
        // stats
        float Health { get; }
        float Shield { get; }

        // inventory
        IInventory Inventory { get; }

        Vector2 DropPosition { get; }
        Vector2 PickupPosition { get; }
        Vector2 WeaponAttachPosition { get; }
    }
}

namespace Element
{
    /// <summary>
    /// This will hold all player logic and controls.
    /// </summary>
    ///
    public class Player : IPlayer
    {
        private bool _enabled;

        // IMoveable
        public int Width => (this.AnimatedSprite != null) ? this.AnimatedSprite.Width : 0;
        public int Height => (this.AnimatedSprite != null) ? this.AnimatedSprite.Height : 0;


        // ICollideable
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, AnimatedSprite.Width, AnimatedSprite.Height);

        // IOwner
        public Vector2 WeaponAttachPosition { get => this.Position + new Vector2(15, 30); }

        public Vector2 DropPosition { get => new Vector2(this.Position.X + (this.AnimatedSprite.Width / 2), this.Position.Y + this.AnimatedSprite.Height); }
        public Vector2 PickupPosition { get => new Vector2(this.Position.X + (this.AnimatedSprite.Width / 2), this.Position.Y + this.AnimatedSprite.Height); }

        // IPlayer
        public Vector2 MinPosition { get; set; } // top left corner of the player's movement box
        public Vector2 MaxPosition { get; set; } // bottom right corner of the player's movement box

        public AnimatedSprite AnimatedSprite { get; set; }
        public IInventory Inventory { get; }

        // base stats
        public float BaseHealth { get; set; }

        public float BaseShield { get; set; }
        public float BaseAcceleration { get; set; }
        public float BaseVelocity { get; set; }
        public float BasePickupRadius { get; set; }

        // adjusted stats
        public float Health { get => this.BaseHealth; }

        public float Shield { get => this.BaseShield; }
        public float Acceleration { get => this.BaseAcceleration; }
        public float Velocity { get => this.BaseVelocity; }

        private Vector2 _position;
        private readonly IInput _input;
        private readonly IContentManager _contentManager;

        /// <summary>
        /// Player constructor, accepts an object that implements IInput interface
        /// </summary>
        public Player(IInput input, IContentManager contentManager)
        {
            _input = input ?? throw new ArgumentNullException(ComponentStrings.Input);
            _contentManager = contentManager ?? throw new ArgumentNullException(ComponentStrings.ContentManager);

            AnimatedSprite = _contentManager.GetAnimatedSprite("female");

            MinPosition = new Vector2(0, 0);
            MaxPosition = new Vector2(1280, 720); // TODO: tie to something!
            Position = MinPosition;
            BaseHealth = 100.0f;
            BaseShield = 100.0f;
            BasePickupRadius = 30;
            BaseVelocity = 5;
        }

        public bool Enabled
        {
            get => _enabled;
            private set => _enabled = value;
        }

        /// <summary>
        /// The player's current position realative to the viewport.
        /// This will clamp a users position to MinPosition and MaxPosition
        /// </summary>
        public Vector2 Position
        {
            get => _position;
            set
            {
                // clamp min an max values
                float x = (value.X < MinPosition.X) ? MinPosition.X : value.X;
                float y = (value.Y < MinPosition.Y) ? MinPosition.Y : value.Y;

                x = (x > MaxPosition.X - Height) ? MaxPosition.X - Width : x;
                y = (y > MaxPosition.Y - Width) ? MaxPosition.Y - Height : y;

                _position = new Vector2(x, y);
            }
        }

        /// <summary>
        /// Update the player character and all children actors
        /// </summary>
        public void Update(GameTime gameTime)
        {
            Vector2 oldPosition = this.Position;
            // update to a new position
            // movement is constrained to MinPosition and MaxPosition in the setter
            this.Position += new Vector2(this._input.GetLeftThumbstickVector().X, -this._input.GetLeftThumbstickVector().Y) * new Vector2(this.BaseVelocity);

            // cardinal direction will determine which animation is used
            // animations should be the same number of frames
            // the animation will change on .SetAnimation(), but the current frame will remain the same
            // so the animations should be synced on the spritesheet in order to move smoothly from one to another
            int cardinal = _input.GetRightThumbstickCardinal();

            if (cardinal == Cardinal.North)
                this.AnimatedSprite.SetAnimation("female_walk_up");

            if (cardinal == Cardinal.South)
                this.AnimatedSprite.SetAnimation("female_walk_down");

            if (cardinal == Cardinal.East)
                this.AnimatedSprite.SetAnimation("female_walk_right");

            if (cardinal == Cardinal.West)
                this.AnimatedSprite.SetAnimation("female_walk_left");

            if (this.Position != oldPosition)
                this.AnimatedSprite.Update(gameTime);
        }

        /// <summary>
        /// Draw the player character and child actors
        /// </summary>
        public void Draw(SpriteRender spriteRender)
        {
            // the animated sprite draws its current frame by default
            this.AnimatedSprite.Draw(spriteRender.spriteBatch, Position);
        }
    }
}