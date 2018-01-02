using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TexturePackerLoader;

namespace Element.Classes
{
    public class Inventory : IInventory
    {
        // IInventory
        public int SelectedIndex { get; private set; }
        public IItem SelectedItem { get => (this._inventory.Count > 0) ? this._inventory[this.SelectedIndex] : null; }
        public int MaxItems { get; private set; }
        public double TimeOut { get; private set; }
        public bool IsOpen { get; private set; }
        public int Count { get => this._inventory.Count; }
        public IOwner Owner { get => this._owner; set => this._owner = value; }

        // options
        public bool AutoEquip { get; private set; }

        // implementation
        private List<IItem> _inventory { get; }
        private IGameOptions _theGame;
        private readonly IContentManager _contentManager;
        private readonly IInput _input;
        private IItemManager _itemManager;
        private IOwner _owner;

        public Inventory(IGameOptions theGame, IInput input, IContentManager contentManager, IItemManager itemManager, IOwner owner)
        {
            this._theGame = theGame ?? throw new ArgumentNullException("theGame");
            this._input = input ?? throw new ArgumentNullException("input");
            this._contentManager = contentManager ?? throw new ArgumentNullException("contentManager");
            this._itemManager = itemManager ?? throw new ArgumentNullException("itemManager");
            this.Owner = owner;

            this._inventory = new List<IItem>(32);
            this.IsOpen = false;
            this.TimeOut = 5;
            this.SelectedIndex = 0;
            this.MaxItems = 5;
            this.AutoEquip = _theGame.GetBoolOption(option: "Inv_AutoEquip", defaultValue: false);
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
            this.TimeOut = 5;
        }

        public void Close()
        {
            if (this.IsOpen)
            {
                this.IsOpen = false;
                _contentManager.GetSoundEffect("Inv_Open").Play(1, -0.2f, -0.6f); ;
            }
            this.TimeOut = 0;
        }

        public void Update(GameTime gameTime)
        {
            this.CheckTimeout(gameTime);

            if (_input.GetButtonState(Buttons.LeftShoulder) == ButtonState.Pressed)
            {
                this.MoveSelectionUp();
            }

            if (_input.GetButtonState(Buttons.RightShoulder) == ButtonState.Pressed)
            {
                this.Open();
                this.MoveSelectionDown();
            }


            if (_input.GetButtonState(Buttons.X) == ButtonState.Pressed)
            {
                this.DropItem();
            }

            if (_input.GetButtonState(Buttons.Y) == ButtonState.Pressed)
            {
                this.Toggle();
            }

            // look for items near me ?
            if (_input.GetButtonState(Buttons.A) == ButtonState.Pressed)
            {
                PickupItem();
            }
        }

        private void CheckTimeout(GameTime gameTime)
        {
            if (this.IsOpen)
            {
                this.TimeOut -= gameTime.ElapsedGameTime.TotalSeconds;
                if (this.TimeOut <= 0)
                {
                    this.Close();
                }
            }
        }

        private void MoveSelectionUp()
        {
            this.Open();

            if (this.Count > 0)
            {
                this.SelectedIndex--;
                this.SelectedIndex = (this.SelectedIndex < 0) ? this.Count - 1 : this.SelectedIndex;
                this.Owner.EquipWeapon((IGun)this.SelectedItem);
                _contentManager.GetSoundEffect("Inv_Vertical").Play(1, 0.1f, -0.6f);
            }
        }

        private void MoveSelectionDown()
        {
            this.Open();

            if (this.Count > 0)
            {
                this.SelectedIndex++;
                this.SelectedIndex = (this.SelectedIndex > this.Count - 1) ? 0 : this.SelectedIndex;
                this.Owner.EquipWeapon((IGun)this.SelectedItem);
                _contentManager.GetSoundEffect("Inv_Vertical").Play(1, -0.2f, -0.6f); ;
            }
        }

        private void PickupItem()
        {
            List<IItem> nearbyItems = this._itemManager.GetItemsInVicinity(this._owner.PickupPosition, 60);

            if (nearbyItems.Count > 0)
            {
                if (this.Count < this.MaxItems)
                {
                    // add the item to the inventory
                    IItem item = nearbyItems[0];  // TODO: make sure it's the closest item
                    this._inventory.Add(item);
                    this._itemManager.Remove(item);

                    // tell item and owner about the pickup
                    item.Pickup(this.Owner);
                    this.Owner.Pickup(item);

                    // play sound
                    _contentManager.GetSoundEffect("Inv_Pickup").Play();
                }
                else
                {
                    // inventory is full, play wah wah sound
                    _contentManager.GetSoundEffect("buzzer1").Play();
                }
            }
        }


        // TODO: player needs to drop weapon, not the inventory
        private void DropItem()
        {
            if (this.Count > 0)
            {
                this.Open(); // show the inventory, is that right?

                IItem item = this.SelectedItem;
                this._inventory.Remove(item);
                this._itemManager.Add(item);

                // adjust selected value for removed item
                if (this.SelectedIndex > 0)
                    this.SelectedIndex--;

                // tell owner and item about the drop
                this._owner.Drop(item); // handle drop on player
                item.Drop(this._owner.DropPosition); // handle drop on weapon
            }
        }

        public void Draw(SpriteRender spriteRender)
        {
            // draw inventory first
            if (this.IsOpen)
            {
                Utilities.DrawRectangle(new Rectangle(5, 95, 125, 40 + (this.Count * 40)), Color.DarkSlateGray, spriteRender.spriteBatch);

                // draw title
                int y = 100;
                spriteRender.spriteBatch.DrawString(_contentManager.GetFont("Arial"), "Inventory", new Vector2(10, y), Color.Orange);

                // draw selected rectangle
                IItem selected = (this.Count > 0) ? _inventory[SelectedIndex] : null;

                // draw inventory
                foreach (IItem item in this._inventory)
                {
                    y += 5 + (int)item.SpriteSheet.Sprite(item.ItemIcon).Size.Y;

                    if (item.Equals(selected))
                    {
                        Utilities.DrawRectangle(new Rectangle(12 - 1, y - 1, (int)item.Width + 2, (int)item.Height + 2), Color.Red, spriteRender.spriteBatch);
                    }

                    if (item.Equals(this._owner.EquippedWeapon))
                    {
                        Utilities.DrawRectangle(new Rectangle(12, y, (int)item.Width, (int)item.Height), Color.Green, spriteRender.spriteBatch);
                    }

                    spriteRender.Draw(sprite: item.SpriteSheet.Sprite(item.ItemIcon), position: new Vector2(12, y));
                }
            }
        }
    }
}
