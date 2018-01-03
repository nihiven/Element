using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IInventory
    {
        int SelectedIndex { get; }
        IItem SelectedItem { get; }
        int MaxItems { get; }
        double TimeOut { get; }
        bool IsOpen { get; }
        int Count { get; }
        IPlayer Owner { get; set; }

        void Open();
        void Close();
        bool Toggle();
        void Draw(SpriteRender spriteRender);
        void Update(GameTime gameTime);
    }

}
