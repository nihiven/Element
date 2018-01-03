﻿using System.Collections.Generic;
using Element.Classes;
using Element.Interfaces;
using System;
using Microsoft.Xna.Framework;

namespace Element
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

    // the object factory returns 'singleton' objects for our game components
    public static class ComponentFactory
    {
        public static IComponent New(string identifier)
        {
            switch (identifier)
            {
                case ("input"):
                    return new XB1Pad();
                case ("controllerDebug"):
                    return new ControllerDebug(
                        input: ObjectManager.Get<IInput>("input"), 
                        contentManager: ObjectManager.Get<IContentManager>("contentManager"), 
                        graphics: ObjectManager.Get<IGraphics>("graphics")
                    );
                case ("itemDebug"):
                    return new ItemDebug(
                        input: ObjectManager.Get<IInput>("input"), 
                        itemManager: ObjectManager.Get<IItemManager>("itemManager")
                    );
                case ("debug"):
                    return new Debug(contentManager: ObjectManager.Get<IContentManager>("contentManager"));
                case ("itemManager"):
                    return new ItemManager();
                case ("hud"):
                    return new Hud(
                        ObjectManager.Get<IPlayer>("player"),
                        contentManager: ObjectManager.Get<IContentManager>("contentManager")
                        );
                case ("player"):
                    return new Player(
                        input: ObjectManager.Get<IInput>("input"), 
                        contentManager: ObjectManager.Get<IContentManager>("contentManager")
                    );
                case ("theGame"):
                    return new GameOptions();
                default:
                    return null;
            }
        }
    }

    public static class ObjectFactory
    {
        public static IInventory NewInventory(IPlayer owner)
        {
            return new Inventory(
                theGame: ObjectManager.Get<IGameOptions>("theGame"),
                input: ObjectManager.Get<IInput>("input"),
                contentManager: ObjectManager.Get<IContentManager>("contentManager"),
                itemManager: ObjectManager.Get<IItemManager>("itemManager"),
                owner: owner
            );
        }
    }

    // generic type manager
    public static class ObjectManager
    {
        private static Dictionary<string, object> _objects = new Dictionary<string, object>(32);

        public static void Add(string identifier, object obj)
        {
            // remove an old key,value pair if it exists
            if (_objects.ContainsKey(key: identifier))
                _objects.Remove(key: identifier);

            // add the new key,value pair to our list
            _objects.Add(identifier, obj);
        }

        public static T Get<T>(string identifier)
        {
            // make sure an object exists for identifier
            if (_objects.ContainsKey(identifier))
                return (T)_objects[identifier];
            else
                return default(T);
        }

        public static Dictionary<string, object> Objects
        {
            get { return _objects;  }
        }

        public static void Clear()
        {
            _objects = new Dictionary<string, object>(32);
        }

        public static Dictionary<string, object>.KeyCollection ListIdentifiers()
        {
            // returns a list that can be iterated over
            return _objects.Keys;
        }
    }
}
