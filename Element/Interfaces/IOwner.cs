using Microsoft.Xna.Framework;

namespace Element.Interfaces
{
    public interface IOwner
    {
        Vector2 DropPosition { get; }
        Vector2 PickupPosition { get; }
        void EquipWeapon(IGun gun);
        IGun EquippedWeapon { get; }
    }
}
