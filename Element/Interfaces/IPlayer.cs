using Microsoft.Xna.Framework;

namespace Element.Interfaces
{
    public interface IPlayer
    {
        // stats
        float Health { get; }
        float Shield { get; }
        float Acceleration { get; }
        float Velocity { get; }

        // inventory
        IInventory Inventory { get; }
        IWeapon EquippedWeapon { get; }

        Vector2 Position { get; }
        Vector2 DropPosition { get; }
        Vector2 PickupPosition { get; }
        Vector2 WeaponAttachPosition { get; }

        void EquipWeapon(IWeapon gun);
        void RemoveItem(IItem item);
        void Pickup(IItem item);
        void Drop(IItem item);
    }
}
