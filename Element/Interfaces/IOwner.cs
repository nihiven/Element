using Microsoft.Xna.Framework;

namespace Element.Interfaces
{
    public interface IOwner
    {
        Vector2 DropPosition { get; }
        Vector2 PickupPosition { get; }
        Vector2 WeaponAttachPosition { get; }
        void EquipWeapon(IGun gun);
        void RemoveItem(IItem item);
        IGun EquippedWeapon { get; }

        void Pickup(IItem item);
        void Drop(IItem item);
    }
}
