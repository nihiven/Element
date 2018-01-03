using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using TexturePackerLoader;

namespace Element.Classes
{
    public class Hud : IComponent
    {
        public bool Enabled => true;
        public Vector2 AmmoPosition { get; set; }
        public Vector2 WeaponPosition { get; set; }
        public Vector2 HealthPosition { get; set; }

        private IPlayer _player;
        private IContentManager _contentManager;
        private SpriteFont _font;

        public Hud(IPlayer player, IContentManager contentManager)
        {
            this._player = player ?? throw new ArgumentNullException("player");
            this._contentManager = contentManager ?? throw new ArgumentNullException("contentManager");

            this._font = this._contentManager.GetFont("Arial");
        }

        public void Draw(SpriteRender spriteRender)
        {
            this.DrawAmmo(spriteRender);
            this.DrawWeapon(spriteRender);
            this.DrawHealth(spriteRender);
        }

        public void DrawAmmo(SpriteRender spriteRender)
        {
            int width = spriteRender.spriteBatch.GraphicsDevice.Viewport.Width;
            int height = spriteRender.spriteBatch.GraphicsDevice.Viewport.Height;

            // draw ammo
            string magCount = (this._player.EquippedWeapon != null) ? this._player.EquippedWeapon.MagCount.ToString().PadLeft(3, '0') : "000";
            string reserveCount = (this._player.EquippedWeapon != null) ? this._player.EquippedWeapon.ReserveCount.ToString().PadLeft(3, '0') : "000";

            int ammoX = (int)(width - 10 - Math.Max(this._font.MeasureString(magCount).X, this._font.MeasureString(reserveCount).X));
            int ammoY = (int)(height - 10 - (this._font.LineSpacing * 2));

            // TODO: Need access to drawable screen area
            spriteRender.spriteBatch.DrawString(
                spriteFont: this._font,
                text: magCount,
                position: new Vector2(ammoX, ammoY),
                color: Color.White
            );

            spriteRender.spriteBatch.DrawString(
                spriteFont: this._font,
                text: reserveCount,
                position: new Vector2(ammoX, ammoY + this._font.LineSpacing),
                color: Color.White
            );

            // draw weapon
            if (this._player.EquippedWeapon != null)
            {
                int weaponX = (int)(ammoX - 10 - _player.EquippedWeapon.PopupFrame.Size.X/4);

                Utilities.DrawRectangle(new Rectangle(ammoX - 5, ammoY - 5, 1, _font.LineSpacing * 2 + 10), Color.OrangeRed, spriteRender.spriteBatch);
                spriteRender.Draw(_player.EquippedWeapon.PopupFrame, new Vector2(weaponX, ammoY), Color.White, 0, 0.25f);
            }

            
        }

        public void DrawWeapon(SpriteRender spriteRender)
        {

        }

        public void DrawHealth(SpriteRender spriteRender) // includes shield
        {

        }

        public void Initialize()
        {
            
        }

        public void Update(GameTime gameTime)
        {
        }

        public void LoadContent(ContentManager content)
        {
        }

        public void UnloadContent()
        {
        }
    }
}
