using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TexturePackerLoader;

namespace Element.Classes
{
    public class Weapon : IGun
    {
        // replace with Projectile Manager
        public List<Bullet> _bullets;


        // sprite and positioning related
        public Vector2 Position { get; set; }
        public Vector2 FirePosition { get => new Vector2(this.Width, 2) + this._owner.WeaponAttachPosition; } // position at which the bullets are created
        public AnimatedSprite AnimatedSprite { get; set; }
        public SpriteSheet SpriteSheet { get; set; }
        public int Width => this.AnimatedSprite.Width;
        public int Height => this.AnimatedSprite.Height;

        // IGun
        public IOwner Owner { get => _owner; set => this._owner = value; }
        public int BaseRPM { get => 180;  }
        public int BaseRPS { get => this.BaseRPM / 60; }
        public float BaseDamage { get; set; }
        public float BaseVelocity { get; set; }
        public float BaseRange { get; set; }
        public int BaseMagSize { get; set; } // size is how big it can get, count is how many it currently has
        public int BaseReserveSize { get; set; }

        // current stats
        public int MagCount { get; set; }
        public int ReserveCount { get; set; }

        public string Name { get; set; }
        public Guid Guid { get; set; }


        // components
        private readonly IInput _input;
        private readonly IContentManager _contentManager;
        private IOwner _owner;

        //Testing 
        public bool PlayerClose;

        public Weapon(IInput input, IContentManager contentManager, Guid guid, string itemId, string name, Vector2 spawnLocation)
        {
            this._input = input ?? throw new ArgumentNullException("input");
            this._contentManager = contentManager ?? throw new ArgumentNullException("contentManager");

            this.Name = name;
            this.Guid = guid;
            this.Position = spawnLocation;
            this.BaseDamage = 10;
            this.BaseVelocity = 450;
            this.BaseRange = 400;
            this.BaseMagSize = 190;
            this.BaseReserveSize = 190;
            this._bullets = new List<Bullet>();

            this._contentManager = contentManager;
            this.AnimatedSprite = this._contentManager.GetAnimatedSprite(itemId);
            this.SpriteSheet = this._contentManager.GetSpriteSheet("Guns");
            

            this.PlayerClose = false;
        }

        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            AnimatedSprite.Update(gameTime);

            foreach (Bullet bullet in this._bullets)
            {
                bullet.Update(gameTime);
            }

            if (_input.GetButtonState(Buttons.RightTrigger) == ButtonState.Pressed)
            {
                // TODO: KEY MAPPING
                // get the angle to fire at from the right thumbstick
                double angle = Utilities.GetAngleFromVectors(new Vector2(0, 0), _input.GetRightThumbstickVector());
                this.Fire(angle);
            }
        }

        public void Draw(SpriteRender spriteRender)
        {
            spriteRender.Draw(this.SpriteSheet.Sprite(TexturePackerMonoGameDefinitions.TexturePacker.JadeRabbit_item), this._owner.WeaponAttachPosition);

            foreach (Bullet bullet in this._bullets)
            {
                bullet.Draw(spriteRender);
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, AnimatedSprite.Width, AnimatedSprite.Height);
            }
        }

        public void Fire(double angle)
        {
            if (this.BaseMagSize > 0)
            {
                _input.SetVibration(leftMotor: 0.1f, rightMotor: 0.25f, duration: 0.25f);
                this._bullets.Add(new Bullet(contentManager: this._contentManager, gun: this, position: this.FirePosition, angle: angle));
                this.MagCount -= 1;
            }
        }
    }
}
