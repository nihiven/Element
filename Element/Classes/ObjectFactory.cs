using System.Collections.Generic;
using Element.Classes;
using Element.Interfaces;

namespace Element
{
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
                    return new ControllerDebug((IInput)ObjectManager.Get("input"), (IGraphics)ObjectManager.Get("graphics"));
                case ("soundEffects"):
                    return new SoundEffects((IInput)ObjectManager.Get("input"));
                default:
                    return null;
            }
        }
    }

    // generic type manager
    public static class ObjectManager
    {
        private static Dictionary<string, object> objects = new Dictionary<string, object>(32);

        public static void Add(string identifier, IComponent obj)
        {
            // remove an old key,value pair if it exists
            if (objects.ContainsKey(identifier))
                objects.Remove(identifier);

            // add the new key,value pair to our list
            objects.Add(identifier, obj);
        }

        public static object Get(string identifier)
        {
            // make sure an object exists for identifier
            if (objects.ContainsKey(identifier))
                return objects[identifier];
            else
                return null;
        }

        public static void Clear()
        {
            objects = new Dictionary<string, object>(32);
        }

        public static Dictionary<string, object>.KeyCollection ListIdentifiers()
        {
            // returns a list that can be iterated over
            return objects.Keys;
        }
    }

}
