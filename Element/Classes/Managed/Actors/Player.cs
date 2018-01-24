using Element.Classes;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, AnimatedSprite.Width, AnimatedSprite.Height);
        public Rectangle MoveConstraint => _moveConstraint; // player's valid movement box

        // IOwner
        public Vector2 WeaponAttachPosition { get => this.Position + new Vector2(15, 30); }

        public Vector2 DropPosition { get => new Vector2(this.Position.X + (this.AnimatedSprite.Width / 2), this.Position.Y + this.AnimatedSprite.Height); }
        public Vector2 PickupPosition { get => new Vector2(this.Position.X + (this.AnimatedSprite.Width / 2), this.Position.Y + this.AnimatedSprite.Height); }

        // IPlayer
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

        private Rectangle _moveConstraint;
        private Vector2 _position;
        private readonly IInput _input;
        private IActiveGear _activeGear;
        private IItemManager _itemManager;
        private IInventory _inventory;

        private Rectangle _rect;

        /// <summary>
        /// Player constructor, accepts an object that implements IInput interface
        /// </summary>
        public Player(IInput input, AnimatedSprite animatedSprite, IActiveGear activeGear, IItemManager itemManager, IInventory inventory)
        {
            _input = input ?? throw new ArgumentNullException(ComponentStrings.Input);
            _activeGear = activeGear ?? throw new ArgumentNullException(ComponentStrings.ActiveGear);
            _itemManager = itemManager ?? throw new ArgumentNullException(ComponentStrings.ItemManager);
            _inventory = inventory ?? throw new ArgumentNullException(ComponentStrings.Inventory);

            AnimatedSprite = animatedSprite;

            _moveConstraint = new Rectangle(0, 0, 1280, 720);
            Position = new Vector2(_moveConstraint.X, _moveConstraint.Y);
            BaseHealth = 100.0f;
            BaseShield = 100.0f;
            BasePickupRadius = 50;
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
                _rect = Utilities.ClampRect(BoundingBox, MoveConstraint);
                _position.X = _rect.X;
                _position.Y = _rect.Y;

                _activeGear.WeaponPosition = WeaponAttachPosition;
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
            this.Position += new Vector2(this._input.LeftThumbstickVector.X, -this._input.LeftThumbstickVector.Y) * new Vector2(this.BaseVelocity);

            // cardinal direction will determine which animation is used
            // animations should be the same number of frames
            // the animation will change on .SetAnimation(), but the current frame will remain the same
            // so the animations should be synced on the spritesheet in order to move smoothly from one to another
            int cardinal = _input.RightThumbstickCardinal;

            switch (cardinal)
            {
                case Cardinal.North:
                    this.AnimatedSprite.SetAnimation("female_walk_up");
                    break;
                case Cardinal.South:
                    this.AnimatedSprite.SetAnimation("female_walk_down");
                    break;
                case Cardinal.East:
                    this.AnimatedSprite.SetAnimation("female_walk_right");
                    break;
                case Cardinal.West:
                    this.AnimatedSprite.SetAnimation("female_walk_left");
                    break;
            }

            if (this.Position != oldPosition)
                this.AnimatedSprite.Update(gameTime);

            // pick up items
            if (_input.GetButtonState(Buttons.A) == ButtonState.Pressed)
            {
                List<IItem> items = _itemManager.ItemsInVicinity(PickupPosition, BasePickupRadius); //  TODO: replace base with final value
                if (items.Count > 0)
                {
                    _inventory.Add(items[0]);
                    _itemManager.Remove(items[0]);
                }
            }

            if (_input.GetButtonState(Buttons.RightTrigger) == ButtonState.Pressed || _input.GetButtonState(Buttons.RightTrigger) == ButtonState.Held)
            {
                if (_activeGear.Weapon != null)
                    _activeGear.Weapon.Fire(_input.RightThumbstickAngle);
            }
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