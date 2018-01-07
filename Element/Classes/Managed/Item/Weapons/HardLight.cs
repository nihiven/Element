using Element.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace Element.Classes
{
    public class HardLight : Weapon
    {
        public override string Name { get => "Hard Light"; }
        public override string ItemID { get => "HardLight"; }
        public override string PopupIcon { get => TexturePackerMonoGameDefinitions.Element.Destiny_HardLight_popup; }
        public override string ItemIcon { get => TexturePackerMonoGameDefinitions.Element.Destiny_HardLight_item; }

        public override WeaponModifiers Modifiers { get => WeaponModifiers.Auto; }
        public override double BaseDamage { get => 20; }
        public override double BaseRange { get => 400; }
        public override int BaseRPM { get => 900; }
        public override int BaseMagSize { get => 63; }
        public override int BaseReserveSize { get => 756; }

        public HardLight(
            IInput input,
            IContentManager contentManager,
            Guid guid,
            Vector2 spawnLocation
        ) : base(input, contentManager, guid, spawnLocation) { }
    }
}
