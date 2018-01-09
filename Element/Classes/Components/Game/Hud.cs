using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IHud
    {
        Vector2 AmmoPosition { get; }
        Vector2 WeaponPosition { get; }
        Vector2 HealthPosition { get; }
        string MagCount { get; }
        string ReserveCount { get; }
        void Draw(SpriteRender spriteRender);
    }
}

namespace Element.Classes
{
    public class Hud : IHud
    {
        public bool Enabled => true;
        public Vector2 AmmoPosition => new Vector2(_ammoX, _ammoY);
        public Vector2 WeaponPosition => new Vector2(AmmoPosition.X - 10 - _activeGear.Weapon.PopupFrame.Size.X / 4, AmmoPosition.Y);
        public Vector2 HealthPosition { get; set; }
        public string MagCount => (_activeGear.Weapon != null) ? _activeGear.Weapon.MagCount.ToString().PadLeft(2, '0') : "00";
        public string ReserveCount => (_activeGear.Weapon != null) ? _activeGear.Weapon.ReserveCount.ToString().PadLeft(3, '0') : "000";

        private IGraphics _graphics;
        private IPlayer _player;
        private IContentManager _contentManager;
        private IActiveGear _activeGear;
        private SpriteFont _font;
        private SpriteFont _fontBig;
        
        private float _ammoX => (_viewportWidth - 10 - Math.Max(this._fontBig.MeasureString(MagCount).X, this._font.MeasureString(ReserveCount).X));
        private float _ammoY => (_viewportHeight - 10 - (this._fontBig.LineSpacing + this._font.LineSpacing));
        private int _viewportWidth => (int)_graphics.GetViewPortSize.X;
        private int _viewportHeight => (int)_graphics.GetViewPortSize.Y;
        private int _viewportCenterWidth => (int)_graphics.GetViewPortCenter.X;

        public Hud(IGraphics graphics, IPlayer player, IContentManager contentManager, IActiveGear activeGear)
        {
            _graphics = graphics ?? throw new ArgumentNullException(ComponentStrings.Graphics);
            _player = player ?? throw new ArgumentNullException(ComponentStrings.Player);
            _contentManager = contentManager ?? throw new ArgumentNullException(ComponentStrings.ContentManager);
            _activeGear = activeGear ?? throw new ArgumentNullException(ComponentStrings.ActiveGear);

            _font = this._contentManager.GetFont("Arial");
            _fontBig = this._contentManager.GetFont("ArialBig");
        }

        public void Draw(SpriteRender spriteRender)
        {
            this.DrawAmmo(spriteRender);
            this.DrawHealth(spriteRender);
        }

        public void DrawAmmo(SpriteRender spriteRender)
        {
            if (_activeGear.Weapon != null)
            {
                // draw ammo
                spriteRender.spriteBatch.DrawString(
                    spriteFont: this._fontBig,
                    text: MagCount,
                    position: AmmoPosition + new Vector2(3, 3),
                    color: Color.Black
                );

                spriteRender.spriteBatch.DrawString(
                    spriteFont: this._fontBig,
                    text: MagCount,
                    position: AmmoPosition,
                    color: Color.White
                );

                spriteRender.spriteBatch.DrawString(
                   spriteFont: this._font,
                   text: ReserveCount,
                   position: AmmoPosition + new Vector2(3, 3 + this._fontBig.LineSpacing),
                   color: Color.Black
                );
            
                spriteRender.spriteBatch.DrawString(
                    spriteFont: this._font,
                    text: ReserveCount,
                    position: AmmoPosition + new Vector2(0, this._fontBig.LineSpacing),
                    color: Color.White
                );

                // draw weapon
                // TODO: fix references to _ammoX _ammoY
                Utilities.DrawRectangle(new Rectangle((int)_ammoX - 5, (int)_ammoY - 5, 1, _font.LineSpacing + _fontBig.LineSpacing + 10), Color.Black, spriteRender.spriteBatch);
                spriteRender.Draw(_activeGear.Weapon.PopupFrame, WeaponPosition, Color.White, 0, 0.25f);
            }
        }

        public void DrawHealth(SpriteRender spriteRender) // includes shield
        {
            float health = _player.Health;
            float shield = _player.Shield;

            int barWidth = 400;
            int barHeight = 20;
            int barX = _viewportCenterWidth - (barWidth / 2);
            int shieldY = _viewportHeight - 71;
            int healthY = _viewportHeight - 50;
            float barFillHealth = barWidth * (_player.Health / 100);
            float barFillShield = barWidth * (_player.Shield / 100);

            Utilities.DrawRectangle(new Rectangle(barX-1, shieldY-1, barWidth+2, barHeight+2), Color.Black, spriteBatch: spriteRender.spriteBatch); // backing
            Utilities.DrawRectangle(new Rectangle(barX, shieldY, barWidth, barHeight), Color.LightBlue, spriteBatch: spriteRender.spriteBatch); // shield

            Utilities.DrawRectangle(new Rectangle(barX - 1, healthY - 1, barWidth + 2, barHeight + 2), Color.Black, spriteBatch: spriteRender.spriteBatch); // backing
            Utilities.DrawRectangle(new Rectangle(barX, healthY, barWidth, barHeight), Color.Red, spriteBatch: spriteRender.spriteBatch); // health

        }
    }
}
