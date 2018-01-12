using Element.Classes;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IActiveGear : IDraw
    {
        void Equip(IItem item, Slots slots);
        IWeapon Weapon { get; }
        Vector2 WeaponPosition { get; set; }

    }
}

namespace Element.Classes
{
    public enum Slots
    {
        Weapon = 1,
        Helmet = 4,
        Shoulders = 8,
        Chest = 16,
        Gloves = 32,
        Pants = 64,
        Boots = 128,
        RingLeft = 256,
        RingRight = 512,
        Belt = 1024,
        Neck = 2048
    }

    class ActiveGear : IActiveGear
    {
        public static Dictionary<Slots, string> Text = new Dictionary<Slots, string>
        {
            { Slots.Weapon, "Weapon" },
            { Slots.Helmet, "Helmet" },
            { Slots.Shoulders, "Shoulders" },
            { Slots.Chest, "Chest" },
            { Slots.Gloves, "Gloves" },
            { Slots.Pants, "Pants" },
            { Slots.Boots, "Boots" },
            { Slots.RingLeft, "Ring Left" },
            { Slots.RingRight, "Ring Right" },
            { Slots.Belt, "Belt" },
            { Slots.Neck, "Neck" }
        };

        public IWeapon Weapon => _weapon;
        private IWeapon _weapon;

        public Vector2 WeaponPosition
        {
            get
            {
                return (_weapon == null) ? new Vector2(0) : _weapon.Position;
            }
            set
            {
                if (_weapon != null)
                    _weapon.Position = value;
            }
            
        }

        public void Equip(IItem item, Slots slot)
        {
            switch (slot)
            {
                case Slots.Weapon:
                    _weapon = (IWeapon)item;
                    break;
                default:
                    throw new System.NotImplementedException("This weapon type is not implementied: ActiveGear.cs");
            }
        }
    
        public void Draw(SpriteRender spriteRender)
        {
            if (_weapon != null)
            _weapon.Draw(spriteRender);
        }

    }
}
