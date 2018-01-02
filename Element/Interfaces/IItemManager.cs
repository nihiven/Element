using Element.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Element.Interfaces
{
    public interface IItemManager
    {
        void NewWeapon(Vector2 position);
        void Add(IItem item);
        void Remove(IItem item);
        List<IItem> GetItemsInVicinity(Vector2 searchVector, double searchDistance);
    }
}
