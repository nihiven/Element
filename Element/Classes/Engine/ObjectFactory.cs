using System.Collections.Generic;
using Element.Classes;
using Element.Interfaces;
using Element.DestinyGuns;
using System;
using Microsoft.Xna.Framework;

namespace Element
{  
    public static class ItemFactory
    {
        public static IGun RandomWeapon(Vector2 spawnLocation)
        {
            return new Weapon((IInput)ObjectManager.Get("input"), Guid.NewGuid(), "JadeRabbit", "Jade Rabbit", spawnLocation);
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
                    return new ControllerDebug(input: (IInput)ObjectManager.Get("input"), graphics: (IGraphics)ObjectManager.Get("graphics"));
                case ("itemDebug"):
                    return new ItemDebug((IInput)ObjectManager.Get("input"), (IItemManager)ObjectManager.Get("itemManager"));
                case ("itemManager"):
                    return new ItemManager();
                case ("soundEffects"):
                    return new SoundEffects((IInput)ObjectManager.Get("input"));
                case ("player"):
                    return new Player((IInput)ObjectManager.Get("input"));
                default:
                    return null;
            }
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

        public static object Get(string identifier)
        {
            // make sure an object exists for identifier
            if (_objects.ContainsKey(identifier))
                return _objects[identifier];
            else
                return null;
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
