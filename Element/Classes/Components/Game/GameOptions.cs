using Element.Interfaces;
using System;
using System.Collections.Generic;

namespace Element.Interfaces
{
    public interface IGameOptions
    {
        bool GetBoolOption(string option, bool defaultValue);
        void SetBoolOption(string option, bool value);
    }
}

namespace Element.Classes
{
    [Serializable]
    public class GameOptions : IGameOptions
    {
        public bool Enabled { get => true; } // always enabled

        private Dictionary<string, bool> _boolOptions = new Dictionary<string, bool>();

        public bool GetBoolOption(string option, bool defaultValue)
        {
            try
            {
                return _boolOptions[option];
            }
            catch(KeyNotFoundException)
            {
                return defaultValue;
            }
        }

        public void SetBoolOption(string option, bool value)
        {
            _boolOptions[option] = value;
        }
    }
}
