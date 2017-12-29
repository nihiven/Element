using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TexturePackerLoader;

namespace Element.Classes
{
    class ItemDebug : IComponent
    {
        private IInput input;
        private IItemManager itemManager;

        public ItemDebug(IInput input, IItemManager itemManager)
        {
            this.input = input ?? throw new ArgumentNullException("input");
            this.itemManager = itemManager ?? throw new ArgumentNullException("itemManager");
        }

        public void Draw(SpriteRender spriteRender)
        {
            
        }

        public void Initialize()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            if (input.GetButtonState(Buttons.B) == ButtonState.Pressed)
            {
                Player player = ObjectManager.Get<Player>("player");
                itemManager.NewWeapon(player.Position);
            }
        }
    }
}
