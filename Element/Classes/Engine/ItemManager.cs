using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Element.Classes
{
    public interface IItemManager
    {
        void NewWeapon(Vector2 position);
    }

    public class ItemManager : IComponent, IItemManager
    {
        Dictionary<string, IItem> Items = new Dictionary<string, IItem>();

        public ItemManager()
        {
        }

        public void NewWeapon(Vector2 position)
        {
            IItem item = ItemFactory.RandomWeapon(position);
            string guid = item.Guid.ToString();

            Items.Add(guid, item);
        }

        // IComponent
        public void Initialize()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            foreach (IItem item in this.Items.Values)
            {
                item.AnimatedSprite.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IItem item in this.Items.Values)
            {
                item.AnimatedSprite.Draw(spriteBatch, item.Position);
            }
        }

        public void LoadContent(ContentManager content)
        {
            
        }

        public void UnloadContent()
        {

        }
    }
}
