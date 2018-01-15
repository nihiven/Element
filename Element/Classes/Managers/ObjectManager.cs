using System.Collections.Generic;

namespace Element.Classes
{
    // generic type manager
    public static class ObjectManager
    {
        private static Dictionary<string, object> _objects = new Dictionary<string, object>(32);

        public static Dictionary<string, object> Objects => _objects;
        public static Dictionary<string, object>.KeyCollection ListIdentifiers() => _objects.Keys;

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

        public static void Clear()
        {
            _objects = new Dictionary<string, object>(32);
        }
    }
}
