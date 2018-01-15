using Element.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace Element.Classes
{
    public class JadeRabbit : Weapon
    {
        public override string Name { get => "Jade Rabbit"; }
        public override string ItemID { get => "JadeRabbit"; }
        public override string PopupIcon { get => TexturePackerMonoGameDefinitions.Element.Destiny_JadeRabbette_popup; }
        public override string ItemIcon { get => TexturePackerMonoGameDefinitions.Element.Destiny_JadeRabbette_item; }

        public override WeaponModifiers Modifiers { get => WeaponModifiers.RangeBoost; }
        public override double BaseDamage { get => 60; }
        public override double BaseRange { get => 800; }
        public override int BaseRPM { get => 300; }
        public override int BaseMagSize { get => 21; }
        public override int BaseReserveSize { get => 252; }

        public JadeRabbit(
            IInput input,
            IContentManager contentManager,
            IEntityManager entityManager,
            Guid guid,
            Vector2 spawnLocation
        ) : base(input, contentManager, entityManager, guid, spawnLocation) { }
    }
}
