using Element.Classes;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Element.Factories
{
    public static class ItemFactory
    {
        public enum ItemTiers
        {
            Common = 0, // 60:100, world drops
            Uncommon = 1, // 29:100, world drops
            Rare = 2, // 10:100, world drops
            Legendary = 4, // 1:100 item drops, world drops
            Mythic = 8, // 1:25 extremely rare set items, boss drops
            Exalted = 16  // 1:100 ?? there are legends about these unique weapons, super boss drops
        }

        public enum WeaponTypes // ??
        {

        }

        public static IWeapon RandomWeapon(Vector2 spawnLocation)
        {
            List<string> weapons = new List<string> { null, "Thorn", "HardLight", "JadeRabbette" };
            string weapon = weapons[Utilities.SeededRand.Next(weapons.Count)];

            switch (weapon)
            {
                case "Thorn":
                    return new Thorn(
                        input: ObjectManager.Get<IInput>("input"),
                        contentManager: ObjectManager.Get<IContentManager>("contentManager"),
                        guid: Guid.NewGuid(),
                        spawnLocation: spawnLocation
                    );
                case "JadeRabbette":
                    return new JadeRabbit(
                        input: ObjectManager.Get<IInput>("input"),
                        contentManager: ObjectManager.Get<IContentManager>("contentManager"),
                        guid: Guid.NewGuid(),
                        spawnLocation: spawnLocation
                    );
                default:
                    return new HardLight(
                        input: ObjectManager.Get<IInput>("input"),
                        contentManager: ObjectManager.Get<IContentManager>("contentManager"),
                        guid: Guid.NewGuid(),
                        spawnLocation: spawnLocation
                    );
            }
        }
    }
}
