using Element.Interfaces;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IInventory : IDraw, IUpdate
    {
        IItem SelectedItem { get; }
        int MaxItems { get; }
        bool IsOpen { get; }
        int Count { get; }

        void Add(IItem item);
        void Remove(IItem item);
        void Open();
        void Close();
        bool Toggle();
    }
}

namespace Element.Classes
{
    public class Inventory : IInventory
    {
        // IInventory
        public IItem SelectedItem { get => (this._inventory.Count > 0) ? this._inventory[this._selectedIndex] : null; }
        public int MaxItems => _maxItems;
        public bool IsOpen => _isOpen;
        public int Count => _inventory.Count;

        private int _selectedIndex;
        private int _maxItems;
        private bool _isOpen;
        private double _timeOut;
        private List<IItem> _inventory { get; }

        // components
        private readonly IContentManager _contentManager;
        private readonly IInput _input;
        private IActiveGear _activeGear;

        public Inventory(IInput input, IContentManager contentManager, IActiveGear activeGear)
        {
            _input = input ?? throw new ArgumentNullException(ComponentStrings.Input);
            _contentManager = contentManager ?? throw new ArgumentNullException(ComponentStrings.ContentManager);
            _activeGear = activeGear ?? throw new ArgumentNullException(ComponentStrings.ActiveGear);

            _isOpen = false;
            _inventory = new List<IItem>(32);
            _timeOut = 5;
            _selectedIndex = 0;
            _maxItems = 5;
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
                _isOpen = true;
                _contentManager.GetSoundEffect("Inv_Open").Play(1, 0, -0.6f);
            }
            this._timeOut = 5;
        }

        public void Close()
        {
            if (this.IsOpen)
            {
                _isOpen = false;
                _contentManager.GetSoundEffect("Inv_Open").Play(1, -0.2f, -0.6f); ;
            }
            this._timeOut = 0;
        }

        public void Update(GameTime gameTime)
        {
            _activeGear.Update(gameTime);

            CheckTimeout(gameTime);

            if (_input.GetButtonState(Buttons.LeftShoulder) == ButtonState.Pressed)
                MoveSelectionUp();

            if (_input.GetButtonState(Buttons.RightShoulder) == ButtonState.Pressed)
            {
                Open();
                MoveSelectionDown();
            }

            if (_input.GetButtonState(Buttons.Y) == ButtonState.Pressed)
                Toggle();

            if (_input.GetButtonState(Buttons.A) == ButtonState.Pressed)
            {
                if (IsOpen)
                {
                    if (SelectedItem != null)
                        _activeGear.Equip(SelectedItem, Slots.Weapon);
                }
            }
        }

        private void CheckTimeout(GameTime gameTime)
        {
            if (this.IsOpen)
            {
                this._timeOut -= gameTime.ElapsedGameTime.TotalSeconds;
                if (this._timeOut <= 0)
                {
                    this.Close();
                }
            }
        }

        private void MoveSelectionUp()
        {
            Open();

            if (Count > 0)
            {
                this._selectedIndex--;
                this._selectedIndex = (this._selectedIndex < 0) ? this.Count - 1 : this._selectedIndex;
                _contentManager.GetSoundEffect("Inv_Vertical").Play(1, 0.1f, -0.6f);
            }
        }

        private void MoveSelectionDown()
        {
            this.Open();

            if (this.Count > 0)
            {
                this._selectedIndex++;
                this._selectedIndex = (this._selectedIndex > this.Count - 1) ? 0 : this._selectedIndex;
                _contentManager.GetSoundEffect("Inv_Vertical").Play(1, -0.2f, -0.6f); ;
            }
        }

        public void Add(IItem item)
        {
            if (this.Count < this.MaxItems)
            {
                // add the item to the inventory
                _inventory.Add(item);

                // play sound
                _contentManager.GetSoundEffect("Inv_Pickup").Play();
            }
            else
            {
                // inventory is full, play wah wah sound
                _contentManager.GetSoundEffect("buzzer1").Play();
            }
        }


        // TODO: player needs to drop weapon, not the inventory
        public void Remove(IItem item)
        {
            if (this.Count > 0)
            {
                this.Open(); // show the inventory, is that right?

                this._inventory.Remove(item);

                // adjust selected value for removed item
                if (this._selectedIndex > 0)
                    this._selectedIndex--;
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
                IItem selected = (this.Count > 0) ? _inventory[_selectedIndex] : null;

                // draw inventory
                foreach (IItem item in this._inventory)
                {
                    y += 10 + (int)item.ItemFrame.Size.Y;

                    if (item.Equals(selected))
                    {
                        Utilities.DrawRectangle(new Rectangle(12 - 1, y - 1, (int)item.Width + 2, (int)item.Height + 2), Color.Red, spriteRender.spriteBatch);
                    }

                    if (item.Equals(_activeGear.Weapon))
                    {
                        Utilities.DrawRectangle(new Rectangle(12, y, (int)item.Width, (int)item.Height), Color.Green, spriteRender.spriteBatch);
                    }

                    spriteRender.Draw(sprite: item.ItemFrame, position: new Vector2(12, y));
                }
            }
        }
    }
}
