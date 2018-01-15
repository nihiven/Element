using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Element.Interfaces
{
    public interface IItemDebug : IUpdate
    {
    }
}

namespace Element.Classes
{
    class ItemDebug : IItemDebug
    {
        public bool Enabled => true;

        private IInput _input;
        private IItemManager _itemManager;

        public ItemDebug(IInput input, IItemManager itemManager)
        {
            _input = input ?? throw new ArgumentNullException(ComponentStrings.Input);
            _itemManager = itemManager ?? throw new ArgumentNullException(ComponentStrings.ItemManager);
        }

        public void Update(GameTime gameTime)
        {
            if (_input.GetButtonState(Buttons.B) == ButtonState.Pressed)
            {
                Player player = ObjectManager.Get<Player>(ComponentStrings.Player);
                _itemManager.NewWeapon();
            }
        }
    }
}
