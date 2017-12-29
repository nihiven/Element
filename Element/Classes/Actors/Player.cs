using Element.Classes;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TexturePackerLoader;

namespace Element
{
    /// <summary>
    /// This will hold all player logic and controls.
    /// </summary>
    public class Player : IComponent, IMoveable, IAttachable
    {
        public Vector2 MinPosition { get; set; } // top left corner of the player's movement box
        public Vector2 MaxPosition { get; set; } // bottom right corner of the player's movement box
        public AnimatedSprite AnimatedSprite { get; set; }
        public IItem ItemEquiped { get; set; }


        // inventory
        // TODO: make this a component
        public List<IItem> Inventory;
        public bool ShowInventory;
        public double InventoryTimeout;
        public int InventorySelected;
        public int InventoryMaxItems;

        // stats
        public bool Active { get; set; }
        public int Health { get; set; }
        public float Acceleration { get; set; }
        public float Velocity { get; set; }
        public float PickupRadius { get; set; }

        private Vector2 _position;
        private readonly IInput _input;
        private readonly IContentManager _contentManager;
        private IItemManager _itemManager;

        // IAttachPoints
        public List<IItem> Attachments;
        public Vector2 HighArmor { get => this.Position; set => this.HighArmor = value; }
        public Vector2 MidArmor { get => this.Position; set => this.HighArmor = value; }
        public Vector2 LowArmor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 HighWeapon { get => this.Position; set => this.HighArmor = value; }
        public Vector2 MidWeapon { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 LowWeapon { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 HighPet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 MidPet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 LowPet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Player constructor, accepts an object that implements IInput interface
        /// </summary>
        public Player(IInput input, IContentManager contentManager, IItemManager itemManager)
        {
            this._input = input ?? throw new ArgumentNullException("input");
            this._contentManager = contentManager ?? throw new ArgumentNullException("contentManager");
            this._itemManager = itemManager ?? throw new ArgumentNullException("itemManager");

            this.AnimatedSprite = this._contentManager.GetAnimatedSprite("female");

            this.MinPosition = new Vector2(0, 0);
            this.MaxPosition = new Vector2(640, 480); // TODO: tie to something. maybe the map?
            this.Position = this.MinPosition;
            this.Active = true;
            this.Health = 100;
            this.PickupRadius = 30;

            // items
            this.ItemEquiped = null;

            // inventory 
            this.Inventory = new List<IItem>(32);
            this.ShowInventory = false;
            this.InventoryTimeout = 5;
            this.InventorySelected = 0;
            this.InventoryMaxItems = 5;
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

        public Vector2 PickupPosition
        {
            get { return new Vector2(this.Position.X + (this.AnimatedSprite.Width / 2), this.Position.Y + this.AnimatedSprite.Height); }
        }

        /// <summary>
        /// Returns player width, assumes width of the player is the width of the player texture.
        /// </summary>
        public int Width
        {
            get { return (this.AnimatedSprite != null) ? this.AnimatedSprite.Width : 0;  }
        }

        /// <summary>
        /// Returns player height, assumes height of the player is the height of the player texture.
        /// </summary>
        public int Height
        {
            get { return (this.AnimatedSprite != null) ? this.AnimatedSprite.Height : 0; }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, AnimatedSprite.Width, AnimatedSprite.Height);
            }
        }

        // TODO: uncouple the recalc from the intialization
        // move the recalc to it's own function so it can be called when screen size changes
        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {

        }


        // TODO: load this from a file??
        /// <summary>
        /// Load the art assests for the player
        /// </summary>
        public void LoadContent(ContentManager content)
        {
           
        }

        /// <summary>
        /// Unload unmanaged assests (none yet)
        /// </summary>
        public void UnloadContent()
        {
            // unload unmanaged content
        }

        /// <summary>
        /// Update the player character and all children actors
        /// </summary>
        public void Update(GameTime gameTime)
        {
            Vector2 oldPosition = this.Position;
            // update to a new position
            // movement is constrained to MinPosition and MaxPosition in the setter
            this.Position += new Vector2(_input.GetLeftThumbstickVector().X, -_input.GetLeftThumbstickVector().Y) + new Vector2(_input.GetRightThumbstickVector().X, -_input.GetRightThumbstickVector().Y);

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

            if (this.ShowInventory)
            {
                this.InventoryTimeout -= gameTime.ElapsedGameTime.TotalSeconds;
                if (this.InventoryTimeout <= 0)
                {
                    this.InventoryClose();
                }
            }


            if (_input.GetButtonState(Buttons.LeftShoulder) == ButtonState.Pressed)
            {
                this.InventoryOpen();

                if (this.Inventory.Count > 0)
                {
                    this.InventorySelected--;
                    this.InventorySelected = (this.InventorySelected < 0) ? this.Inventory.Count - 1 : this.InventorySelected;
                    _contentManager.GetSoundEffect("Inv_Vertical").Play(1, 0.1f, -0.6f);
                }
            }

            if (_input.GetButtonState(Buttons.RightShoulder) == ButtonState.Pressed)
            {
                this.InventoryOpen();

                if (this.Inventory.Count > 0)
                {
                    this.InventorySelected++;
                    this.InventorySelected = (this.InventorySelected > Inventory.Count - 1) ? 0 : this.InventorySelected;
                    _contentManager.GetSoundEffect("Inv_Vertical").Play(1, -0.2f, -0.6f); ;
                }
            }


            if (_input.GetButtonState(Buttons.X) == ButtonState.Pressed)
            {
                if (this.Inventory.Count > 0)
                {
                    this.InventoryOpen();
                    this.InventorySelectedItem().Position = this.Position;
                    this._itemManager.Add(this.InventorySelectedItem());
                    this.Inventory.Remove(this.InventorySelectedItem());
                    
                    // TODO: if you drop your equipped weapon, remove it from your player

                    if (InventorySelected != 0)
                    {
                        InventorySelected--;
                    }
                    
                }
            }

            if (_input.GetButtonState(Buttons.Y) == ButtonState.Pressed)
            {
                this.InventoryToggle();
            }

            // look for items near me ?
            if (_input.GetButtonState(Buttons.A) == ButtonState.Pressed)
            {
                List<IItem> nearbyItems = this._itemManager.GetItemsInVicinity(this.PickupPosition, 30);
                
                if(nearbyItems.Count > 0)
                {
                    if (this.Inventory.Count < this.InventoryMaxItems)
                    {
                        IItem item = nearbyItems[0];
                        this.Inventory.Add(item);
                        this._itemManager.Remove(item);
                        _contentManager.GetSoundEffect("Inv_Pickup").Play();

                        if (this.ItemEquiped == null)
                            this.EquipItem(item);
                    }
                    else
                    {
                        _contentManager.GetSoundEffect("buzzer1").Play();
                    }
                }
            }
        }

        // player method
        public void EquipItem(IItem item)
        {
            this.ItemEquiped = item;
            _contentManager.GetSoundEffect("Equip").Play();
        }

        public IItem InventorySelectedItem()
        {
            return (this.Inventory.Count > 0) ? this.Inventory[this.InventorySelected] : null;
        }

        public void InventoryToggle()
        {
            if (this.ShowInventory)
                this.InventoryClose();
            else
                this.InventoryOpen();
        }

        public void InventoryOpen()
        {
            if (!this.ShowInventory)
            {
                this.ShowInventory = true;
                _contentManager.GetSoundEffect("Inv_Open").Play(1, 0, -0.6f);
            }
            this.InventoryTimeout = 5;
        }

        public void InventoryClose()
        {
            if (this.ShowInventory)
            {
                this.ShowInventory = false;
                _contentManager.GetSoundEffect("Inv_Open").Play(1, -0.2f, -0.6f); ;
            }
            this.InventoryTimeout = 0;
        }

        /// <summary>
        /// Draw the player character and child actors
        /// </summary>
        public void Draw(SpriteRender spriteRender)
        {
            // draw inventory first
            if (this.ShowInventory)
            {
                Utilities.DrawRectangle(new Rectangle(5, 95, 125, 40 + (this.Inventory.Count * 30)), Color.DarkSlateGray, spriteRender.spriteBatch);

                // draw title
                int y = 100;
                spriteRender.spriteBatch.DrawString(_contentManager.GetFont("Arial"), "Inventory", new Vector2(10, y), Color.Orange);

                // draw selected rectangle
                IItem selected = (this.Inventory.Count > 0) ? Inventory[InventorySelected] : null;

                // draw inventory
                foreach (IItem item in this.Inventory)
                {
                    y += 32;

                    if (item.Equals(selected))
                    { 
                        Utilities.DrawRectangle(new Rectangle(12-1, y-1, item.AnimatedSprite.Width+2, item.AnimatedSprite.Height+2), Color.Red, spriteRender.spriteBatch);
                    }

                    if (item.Equals(this.ItemEquiped))
                    {
                        Utilities.DrawRectangle(new Rectangle(12, y, item.AnimatedSprite.Width, item.AnimatedSprite.Height), Color.Green, spriteRender.spriteBatch);
                    }

                    item.AnimatedSprite.Draw(spriteRender.spriteBatch, new Vector2(12, y));

                }
            }


            // draw Player
            // the animated sprite draws its current frame by default
            this.AnimatedSprite.Draw(spriteRender.spriteBatch, Position);

            // draw attached items
            if (this.ItemEquiped != null)
            {
                this.ItemEquiped.AnimatedSprite.Draw(spriteRender.spriteBatch, new Vector2(this.Position.X+6, this.Position.Y+36));
            }

        }
    }
}
