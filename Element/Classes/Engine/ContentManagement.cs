using Element.Interfaces;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Element.Classes
{
    public class ContentManagement : IContentManager
    {
        private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
        private Dictionary<string, SpriteSheet> _spriteSheets = new Dictionary<string, SpriteSheet>();
        private Dictionary<string, AnimatedSprite> _animatedSprites = new Dictionary<string, AnimatedSprite>();
        private Dictionary<string, Animation> _animations = new Dictionary<string, Animation>();
        private Dictionary<string, SpriteFont> _fonts = new Dictionary<string, SpriteFont>();
        public Dictionary<string, SoundEffect> _soundEffects = new Dictionary<string, SoundEffect>();
        private ContentManager _content;

        public ContentManagement(ContentManager content)
        {
            this._content = content;
        }

        public void AddSpriteSheet(string identifier, string contentLocation, int rows, int columns)
        {
            _spriteSheets.Add(identifier, new SpriteSheet(this._content, contentLocation, rows, columns));
        }
        public SpriteSheet GetSpriteSheet(string identifier)
        {
            return this._spriteSheets[identifier];
        }

        public void AddAnimatedSprite(string identifier, AnimatedSprite animatedSprite)
        {
            _animatedSprites.Add(identifier, animatedSprite);
        }
        public AnimatedSprite GetAnimatedSprite(string identifier)
        {
            return this._animatedSprites[identifier];
        }

        public void AddAnimation(string identifier, string spriteSheetIdentifier, int startFrame, int frameCount, double framesPerSecond)
        {
            _animations.Add(identifier, new Animation(identifier, this.GetSpriteSheet(spriteSheetIdentifier), startFrame, frameCount, framesPerSecond));
        }
        public Animation GetAnimation(string identifier)
        {
            return this._animations[identifier];
        }

        public void AddFont(string identifier, SpriteFont font)
        {
            _fonts.Add(identifier, font);
        }
        public SpriteFont GetFont(string identifier)
        {
            return this._fonts[identifier];
        }

        public void AddSoundEffect(string identifier, string contentPath)
        {
            _soundEffects.Add(identifier, _content.Load<SoundEffect>(contentPath));
        }
        public SoundEffect GetSoundEffect(string identifier)
        {
            return this._soundEffects[identifier];
        }
    }
}
