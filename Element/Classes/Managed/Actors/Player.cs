﻿using Element.Classes;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IPlayer: IDraw, IUpdate, ICollideable, IMoveable
    {
        // stats
        float Health { get; }
        float Shield { get; }
        float Acceleration { get; }
        float Velocity { get; }

        // inventory
        IInventory Inventory { get; }
        IWeapon EquippedWeapon { get; }

        Vector2 Position { get; }
        Vector2 DropPosition { get; }
        Vector2 PickupPosition { get; }
        Vector2 WeaponAttachPosition { get; }

        void EquipWeapon(IWeapon gun);
        void RemoveItem(IItem item);
        void Pickup(IItem item);
        void Drop(IItem item);
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

        // IOwner
        public Vector2 WeaponAttachPosition { get => this.Position + new Vector2(15, 30); }
        public Vector2 DropPosition { get => new Vector2(this.Position.X + (this.AnimatedSprite.Width / 2), this.Position.Y + this.AnimatedSprite.Height); }
        public Vector2 PickupPosition { get => new Vector2(this.Position.X + (this.AnimatedSprite.Width / 2), this.Position.Y + this.AnimatedSprite.Height); }

        // IPlayer
        public Vector2 MinPosition { get; set; } // top left corner of the player's movement box
        public Vector2 MaxPosition { get; set; } // bottom right corner of the player's movement box
        
        public AnimatedSprite AnimatedSprite { get; set; }
        public IWeapon EquippedWeapon { get; set; }
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
        public Player(IInput input, IContentManager contentManager, IInventory inventory)
        {
            this._input = input ?? throw new ArgumentNullException("input");
            this._contentManager = contentManager ?? throw new ArgumentNullException("contentManager");

            this.Inventory = inventory;
            this.AnimatedSprite = this._contentManager.GetAnimatedSprite("female");

            this.MinPosition = new Vector2(0, 0);
            this.MaxPosition = new Vector2(1280, 720); // TODO: tie to something!
            this.Position = this.MinPosition;
            this.BaseHealth = 100.0f;
            this.BaseShield = 100.0f;
            this.BasePickupRadius = 30;
            this.BaseVelocity = 5;

            // items
            this.EquippedWeapon = null;
        }

        public bool Enabled
        {
            get => this._enabled;
            private set => this._enabled = value;
        }

        /// <summary>
        /// The player's current position realative to the viewport.
        /// This will clamp a users position to MinPosition and MaxPosition
        /// </summary>
        public Vector2 Position
        {
            get { return this._position; }
            set
            {
                // clamp min an max values
                float x = (value.X < MinPosition.X) ? MinPosition.X: value.X;
                float y = (value.Y < MinPosition.Y) ? MinPosition.Y: value.Y;

                x = (x > MaxPosition.X - this.Height) ? MaxPosition.X - this.Width : x;
                y = (y > MaxPosition.Y - this.Width) ? MaxPosition.Y - this.Height : y;

                this._position = new Vector2(x, y);
            } 
        }


        /// <summary>
        /// Returns player width, assumes width of the player is the width of the player texture.
        /// </summary>
        public int Width => (this.AnimatedSprite != null) ? this.AnimatedSprite.Width : 0;

        /// <summary>
        /// Returns player height, assumes height of the player is the height of the player texture.
        /// </summary>
        public int Height => (this.AnimatedSprite != null) ? this.AnimatedSprite.Height : 0;
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, AnimatedSprite.Width, AnimatedSprite.Height);

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

            // inventory
            this.Inventory.Update(gameTime);

            if (EquippedWeapon != null)
                this.EquippedWeapon.Update(gameTime);
        }

        // player method
        public void EquipWeapon(IWeapon gun)
        {
            this.EquippedWeapon = gun;
            _contentManager.GetSoundEffect("Equip").Play();
        }

        public void RemoveItem(IItem item)
        {
            // just remove it from the player
            this.EquippedWeapon = (IWeapon)this.Inventory.SelectedItem;
        }

  
        /// <summary>
        /// Draw the player character and child actors
        /// </summary>
        public void Draw(SpriteRender spriteRender)
        {
            // the animated sprite draws its current frame by default
            this.AnimatedSprite.Draw(spriteRender.spriteBatch, Position);

            // draw attached items
            if (this.EquippedWeapon != null)
            {
                this.EquippedWeapon.Draw(spriteRender);
            }

            // inventory
            this.Inventory.Draw(spriteRender);
        }

        public void Pickup(IItem item)
        {
            if (item is IWeapon && this.EquippedWeapon == null)
                this.EquippedWeapon = (IWeapon)item;
        }

        public void Drop(IItem item)
        {
            if (item == this.EquippedWeapon)
                this.EquippedWeapon = (IWeapon)this.Inventory.SelectedItem;
        }
    }
}