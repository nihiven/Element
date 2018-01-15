using Element.Factories;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IItemManager : IDraw
    {
        void NewWeapon();
        void Add(IItem item);
        void Remove(IItem item);
        List<IItem> ItemsInVicinity(Vector2 searchVector, double searchDistance);
    }
}

namespace Element.Classes
{
    public class ItemManager : IItemManager
    {
        private bool _enabled = false;
        Dictionary<string, IItem> Items = new Dictionary<string, IItem>();

        public bool Enabled
        {
            get { return this._enabled; }
            private set { this._enabled = value; }
        }

        public void NewWeapon()
        {
            IItem item = ItemFactory.RandomWeapon(ObjectManager.Get<IPlayer>(ComponentStrings.Player).DropPosition);
            string guid = item.Guid.ToString();

            Items.Add(guid, item);
        }

        public void Draw(SpriteRender spriteRender)
        {
            foreach (IItem item in this.Items.Values)
                spriteRender.Draw(item.ItemFrame, item.Position);
        }

        public void Add(IItem item)
        {
            Items.Add(item.Guid.ToString(), item);
        }

        public void Remove(IItem item)
        {
            Items.Remove(item.Guid.ToString());
        }

        public List<IItem> ItemsInVicinity(Vector2 searchVector, double searchDistance)
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
