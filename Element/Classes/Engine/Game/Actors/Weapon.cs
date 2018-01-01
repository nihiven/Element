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
        public int BaseRPM { get => 1100;  }
        public double BaseRPS { get => this.BaseRPM / 60; }
        public double BaseFiringDelay { get => 1 / this.BaseRPS;  }
        public double BaseReloadDelay { get => 1; }
        public double BaseDamage { get; set; }
        public double BaseVelocity { get; set; }
        public double BaseRange { get; set; }
        public int BaseMagSize { get; set; } // size is how big it can get, count is how many it currently has
        public int BaseReserveSize { get; set; }
        // internal
        private double _timeSinceLastBullet; // 
        private double _dryFireDelay = 0.25; // minimum amount of time between out of ammo clicks in seconds
        private double _timeReloading;
        private bool _reloading;

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
            this.BaseVelocity = 1600;
            this.BaseRange = 400;
            this.BaseMagSize = 64;
            this.BaseReserveSize = 192;

            this.MagCount = this.BaseMagSize;
            this.ReserveCount = this.BaseReserveSize;

            this._bullets = new List<Bullet>();
            this._timeSinceLastBullet = 0;
            this._timeReloading = 0;
            this._reloading = false;

            this.AnimatedSprite = this._contentManager.GetAnimatedSprite(itemId);
            this.SpriteSheet = this._contentManager.GetSpriteSheet("Guns");
            

            this.PlayerClose = false;
        }

        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            this._timeSinceLastBullet += gameTime.ElapsedGameTime.TotalSeconds;
            AnimatedSprite.Update(gameTime);

            foreach (Bullet bullet in this._bullets)
            {
                bullet.Update(gameTime);
            }

            if (!this._reloading)
            {
                if (_input.GetButtonState(Buttons.RightTrigger) == ButtonState.Pressed || _input.GetButtonState(Buttons.RightTrigger) == ButtonState.Held)
                {
                    // TODO: KEY MAPPING
                    // get the angle to fire at from the right thumbstick
                    Vector2 normalized = _input.GetRightThumbstickVector();
                    normalized.Normalize();

                    double angle = Utilities.GetAngleFromVectors(new Vector2(0, 0), );
                    this.Fire(angle);
                }

                if (_input.GetButtonState(Buttons.RightStick) == ButtonState.Pressed)
                {
                    this.Reload();
                }
            }
            else
            {
                this._timeReloading += gameTime.ElapsedGameTime.TotalSeconds;

                if (this._timeReloading > this.BaseReloadDelay)
                {
                    string insertSound = Utilities.GetRandomListMember<string>(new List<string> { "insert1", "insert2", "insert3" });
                    this._reloading = false;
                    this._timeReloading = 0;
                    _contentManager.GetSoundEffect(insertSound).Play(0.6f, 0, 0);
                }
            }
        }

        public void Draw(SpriteRender spriteRender)
        {
            spriteRender.Draw(this.SpriteSheet.Sprite(TexturePackerMonoGameDefinitions.TexturePacker.JadeRabbit_item), this._owner.WeaponAttachPosition);

            foreach (Bullet bullet in this._bullets)
                bullet.Draw(spriteRender);
        }

        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, AnimatedSprite.Width, AnimatedSprite.Height); }
        }

        public void Fire(double angle)
        {
            if (this.MagCount > 0)
            {
                if (this._timeSinceLastBullet > this.BaseFiringDelay)
                {
                    string sound = Utilities.GetRandomListMember<string>(new List<string> { "shot1", "shot2", "shot3", "shot4", "shot5", "shot6" });

                    _input.SetVibration(leftMotor: 0.1f, rightMotor: 0.25f, duration: 0.25f);
                    _contentManager.GetSoundEffect(sound).Play(0.6f, 0, 0);
                    this._bullets.Add(new Bullet(contentManager: this._contentManager, gun: this, position: this.FirePosition, angle: angle));
                    this.MagCount -= 1;
                    this._timeSinceLastBullet = 0;
                }
            }
            else
            {
                if (this._timeSinceLastBullet > this._dryFireDelay)
                {
                    string sound = Utilities.GetRandomListMember<string>(new List<string> { "dryfire1", "dryfire2" });
                    _contentManager.GetSoundEffect(sound).Play(0.6f, 0, 0);
                    this._timeSinceLastBullet = 0;
                }
            }
        }

        public void Reload()
        {
            if (this.MagCount != this.BaseMagSize)
            {
                this._reloading = true;
                int needForMag = this.BaseMagSize - this.MagCount;

                if (needForMag <= this.ReserveCount)
                {
                    this.ReserveCount -= needForMag;
                    this.MagCount += needForMag;

                    string ejectSound = Utilities.GetRandomListMember<string>(new List<string> { "eject1", "eject2", "eject3" });
                    _contentManager.GetSoundEffect(ejectSound).Play(0.6f, 0, 0);
                }
            }
        }
    }
}
