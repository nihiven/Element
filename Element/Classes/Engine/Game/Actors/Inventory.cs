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
        int SelectedIndex { get; }
        int MaxItemCount { get; }
        double TimeOut { get; }
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
        public int SelectedIndex { get; private set; }
        private List<IItem> _inventory { get; }
        public int Count { get => this._inventory.Count; }

        // options
        public double TimeOut { get; private set; }
        public int MaxItemCount { get; private set; }
        public bool AutoEquip { get; private set; }


        // components
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
            this.MaxItemCount = 5;
            this.AutoEquip = _theGame.GetBoolOption(option: "Inv_AutoEquip", defaultValue: false);
        }

        public IOwner Owner
        {
            get { return this._owner; }
            set { this._owner = value; }
        }

        public IItem SelectedItem()
        {
            return (this._inventory.Count > 0) ? this._inventory[this.SelectedIndex] : null;
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
                _contentManager.GetSoundEffect("Inv_Vertical").Play(1, -0.2f, -0.6f); ;
            }
        }

        private void PickupItem()
        {
            List<IItem> nearbyItems = this._itemManager.GetItemsInVicinity(this._owner.PickupPosition, 30);

            if (nearbyItems.Count > 0)
            {
                if (this.Count < this.MaxItemCount)
                {
                    IItem item = nearbyItems[0];
                    this._inventory.Add(item);
                    item.Owner = this._owner;
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

        private void DropItem()
        {
            if (this.Count > 0)
            {
                this.Open();
                this.SelectedItem().Position = this._owner.DropPosition;
                this._itemManager.Add(this.SelectedItem());
                this._inventory.Remove(this.SelectedItem());

                // TODO: if you drop your equipped weapon, remove it from your player

                if (SelectedIndex != 0)
                {
                    SelectedIndex--;
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
                IItem selected = (this.Count > 0) ? _inventory[SelectedIndex] : null;

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
