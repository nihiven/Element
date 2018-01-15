using Element.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace Element.Classes
{
    public class Thorn : Weapon
    {
        public override string Name { get => "Thorn"; }
        public override string ItemID { get => "Thorn"; }
        public override string PopupIcon { get => TexturePackerMonoGameDefinitions.Element.Destiny_Thorn_popup; }
        public override string ItemIcon { get => TexturePackerMonoGameDefinitions.Element.Destiny_Thorn_item; }

        public override WeaponModifiers Modifiers { get => WeaponModifiers.Auto; }
        public override double BaseDamage { get => 180; }
        public override double BaseRange { get => 200; }
        public override int BaseRPM { get => 100; }
        public override int BaseMagSize { get => 7; }
        public override int BaseReserveSize { get => 84; }

        public Thorn(
            IInput input,
            IContentManager contentManager,
            IEntityManager entityManager,
            Guid guid,
            Vector2 spawnLocation
        ) : base(input, contentManager, entityManager, guid, spawnLocation) { }
    }
}
