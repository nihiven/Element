using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using TexturePackerLoader;

namespace Element.Classes
{
    public interface IInventory
    {
        int InventorySelected { get; }
        int InventoryMaxItems { get; }
        double InventoryTimeout { get; }
        bool IsOpen { get; }
        int Count { get; }
        IOwner Owner { get; set;  }

        void Open();
        void Close();
        bool Toggle();
        void Draw(SpriteRender spriteRender);
        void Update(GameTime gameTime);
    }

    public class Inventory : IInventory
    {
        // TODO: make this a component
        public bool IsOpen { get; private set; }
        public double InventoryTimeout { get; private set; }
        public int InventorySelected { get; private set; }
        public int InventoryMaxItems { get; private set; }
        private List<IItem> _inventory { get; }
        public int Count { get => this._inventory.Count; }

        private readonly IContentManager _contentManager;
        private readonly IInput _input;
        private IItemManager _itemManager;
        private IOwner _owner;
        

        public Inventory(IInput input, IContentManager contentManager, IItemManager itemManager, IOwner owner)
        {
            this._input = input ?? throw new ArgumentNullException("input");
            this._contentManager = contentManager ?? throw new ArgumentNullException("contentManager");
            this._itemManager = itemManager ?? throw new ArgumentNullException("itemManager");
            this.Owner = owner;

            this._inventory = new List<IItem>(32);
            this.IsOpen = false;
            this.InventoryTimeout = 5;
            this.InventorySelected = 0;
            this.InventoryMaxItems = 5;
        }

        public IOwner Owner
        {
            get { return this._owner; }
            set { this._owner = value; }
        }

        public IItem SelectedItem()
        {
            return (this._inventory.Count > 0) ? this._inventory[this.InventorySelected] : null;
        }

        public bool Toggle()
        {
            if (this.IsOpen)
                this.Close();
            else
                this.Open();

            return this.IsOpen;
        }

        public void Open()
        {
            if (!this.IsOpen)
            {
                this.IsOpen = true;
                _contentManager.GetSoundEffect("Inv_Open").Play(1, 0, -0.6f);
            }
            this.InventoryTimeout = 5;
        }

        public void Close()
        {
            if (this.IsOpen)
            {
                this.IsOpen = false;
                _contentManager.GetSoundEffect("Inv_Open").Play(1, -0.2f, -0.6f); ;
            }
            this.InventoryTimeout = 0;
        }

        public void Update(GameTime gameTime)
        {

            if (this.IsOpen)
            {
                this.InventoryTimeout -= gameTime.ElapsedGameTime.TotalSeconds;
                if (this.InventoryTimeout <= 0)
                {
                    this.Close();
                }
            }


            if (_input.GetButtonState(Buttons.LeftShoulder) == ButtonState.Pressed)
            {
                this.Open();

                if (this.Count > 0)
                {
                    this.InventorySelected--;
                    this.InventorySelected = (this.InventorySelected < 0) ? this.Count - 1 : this.InventorySelected;
                    _contentManager.GetSoundEffect("Inv_Vertical").Play(1, 0.1f, -0.6f);
                }
            }

            if (_input.GetButtonState(Buttons.RightShoulder) == ButtonState.Pressed)
            {
                this.Open();

                if (this.Count > 0)
                {
                    this.InventorySelected++;
                    this.InventorySelected = (this.InventorySelected > this.Count - 1) ? 0 : this.InventorySelected;
                    _contentManager.GetSoundEffect("Inv_Vertical").Play(1, -0.2f, -0.6f); ;
                }
            }


            if (_input.GetButtonState(Buttons.X) == ButtonState.Pressed)
            {
                if (this.Count > 0)
                {
                    this.Open();
                    this.SelectedItem().Position = this._owner.DropPosition;
                    this._itemManager.Add(this.SelectedItem());
                    this._inventory.Remove(this.SelectedItem());

                    // TODO: if you drop your equipped weapon, remove it from your player

                    if (InventorySelected != 0)
                    {
                        InventorySelected--;
                    }

                }
            }

            if (_input.GetButtonState(Buttons.Y) == ButtonState.Pressed)
            {
                this.Toggle();
            }

            // look for items near me ?
            if (_input.GetButtonState(Buttons.A) == ButtonState.Pressed)
            {
                List<IItem> nearbyItems = this._itemManager.GetItemsInVicinity(this._owner.PickupPosition, 30);

                if (nearbyItems.Count > 0)
                {
                    if (this.Count < this.InventoryMaxItems)
                    {
                        IItem item = nearbyItems[0];
                        this._inventory.Add(item);
                        this._itemManager.Remove(item);
                        _contentManager.GetSoundEffect("Inv_Pickup").Play();

                        if (this._owner.EquippedWeapon == null && item is IGun)
                            this._owner.EquipWeapon((IGun)item);
                    }
                    else
                    {
                        _contentManager.GetSoundEffect("buzzer1").Play();
                    }
                }
            }
        }

        public void Draw(SpriteRender spriteRender)
        {
            // draw inventory first
            if (this.IsOpen)
            {
                Utilities.DrawRectangle(new Rectangle(5, 95, 125, 40 + (this.Count * 30)), Color.DarkSlateGray, spriteRender.spriteBatch);

                // draw title
                int y = 100;
                spriteRender.spriteBatch.DrawString(_contentManager.GetFont("Arial"), "Inventory", new Vector2(10, y), Color.Orange);

                // draw selected rectangle
                IItem selected = (this.Count > 0) ? _inventory[InventorySelected] : null;

                // draw inventory
                foreach (IItem item in this._inventory)
                {
                    y += 32;

                    if (item.Equals(selected))
                    {
                        Utilities.DrawRectangle(new Rectangle(12 - 1, y - 1, item.AnimatedSprite.Width + 2, item.AnimatedSprite.Height + 2), Color.Red, spriteRender.spriteBatch);
                    }

                    if (item.Equals(this._owner.EquippedWeapon))
                    {
                        Utilities.DrawRectangle(new Rectangle(12, y, item.AnimatedSprite.Width, item.AnimatedSprite.Height), Color.Green, spriteRender.spriteBatch);
                    }

                    item.AnimatedSprite.Draw(spriteRender.spriteBatch, new Vector2(12, y));

                }
            }
        }
    }
}
