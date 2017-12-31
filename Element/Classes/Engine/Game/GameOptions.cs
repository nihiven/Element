using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using TexturePackerLoader;

namespace Element.Classes
{
    public interface IGameOptions
    {
        bool GetBoolOption(string option, bool defaultValue);
        void SetBoolOption(string option, bool value);
    }

    [Serializable]
    public class GameOptions : IGameOptions, IComponent
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

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteRender spriteRender)
        {
            throw new NotImplementedException();
        }

        public void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
