using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TexturePackerLoader;

namespace Element.Classes
{
    public interface IItemManager
    {
        void NewWeapon(Vector2 position);
        void Add(IItem item);
        void Remove(IItem item);
        List<IItem> GetItemsInVicinity(Vector2 searchVector, double searchDistance);
    }

    public class ItemManager : IComponent, IItemManager
    {
        private bool _enabled = false;
        Dictionary<string, IItem> Items = new Dictionary<string, IItem>();

        public ItemManager()
        {
        }

        public bool Enabled
        {
            get { return this._enabled; }
            private set { this._enabled = value; }
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
            /*
            foreach (IItem item in this.Items.Values)
            {
                item.AnimatedSprite.Update(gameTime);
            }
            */
        }

        public void Draw(SpriteRender spriteRender)
        {
            foreach (IItem item in this.Items.Values)
            {
                spriteRender.Draw(item.SpriteSheet.Sprite(item.ItemIcon), item.Position);
                //item.AnimatedSprite.Draw(spriteRender.spriteBatch, item.Position);
            }
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void UnloadContent()
        {

        }

        public void Add(IItem item)
        {
            Items.Add(item.Guid.ToString(), item);
        }

        public void Remove(IItem item)
        {
            Items.Remove(item.Guid.ToString());
        }

        public List<IItem> GetItemsInVicinity(Vector2 searchVector, double searchDistance)
        {
            List<IItem> hits = new List<IItem>();
            foreach (IItem item in Items.Values)
            { 
                if (Vector2.Distance(item.Position, searchVector) <= searchDistance)
                {
                    hits.Add(item);
                }
            }

            return hits;
        }

    }
}
