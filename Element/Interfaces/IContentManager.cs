using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;


namespace Element.Interfaces
{
    public interface IContentManager
    {
        void AddSpriteSheet(string identifier, string contentLocation, int rows, int columns);
        SpriteSheet GetSpriteSheet(string identifier);
        void AddAnimatedSprite(string identifier, AnimatedSprite animatedSprite);
        AnimatedSprite GetAnimatedSprite(string identifier);
        void AddAnimation(string identifier, string spriteSheetIdentifier, int startFrame, int frameCount, double framesPerSecond);
        Animation GetAnimation(string identifier);
        void AddFont(string identifier, SpriteFont font);
        SpriteFont GetFont(string identifier);

        void AddSoundEffect(string identifier, string contentPath);
        SoundEffect GetSoundEffect(string identifier);
    }
}
