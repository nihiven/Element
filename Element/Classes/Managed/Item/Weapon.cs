using Element.Classes;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IWeapon : IItem
    {
        WeaponModifiers Modifiers { get; }

        // base numbers
        double BaseDamage { get; }
        double BaseVelocity { get; }
        double BaseRange { get; }
        int BaseRPM { get; }
        double BaseRPS { get; }
        double BaseFiringDelay { get; }
        double BaseReloadDelay { get; }
        int BaseMagSize { get; }
        int BaseReserveSize { get; }

        // current status
        int MagCount { get; set; }
        int ReserveCount { get; set; }

        // sprite related
        Vector2 FirePosition { get; }

        // methods
        void Fire(double angle);
        void Update(GameTime gameTime);
        void Draw(SpriteRender spriteRender);
    }
}

namespace Element.Classes
{
    [Flags]
    public enum WeaponModifiers // last line of code written in 2017
    {
        None = 0,
        Auto = 1,
        RangeBoost = 2,
        DamageBoost = 4,
        ReloadBoost = 8
    }

    public enum WeaponState
    {
        Idle = 0,
        Firing = 1,
        Empty = 2,
        Reloading = 4
    }

    public abstract class Weapon : IWeapon
    {
        // IItem
        private SpriteSheet SpriteSheet { get; set; }
        public virtual string Name { get => "Weapon"; }
        public virtual string ItemID { get => "WeaponID"; }
        public Guid Guid { get; set; }
        public Vector2 Position { get => (this._owner == null) ? this._position : this._owner.WeaponAttachPosition; set => this._position = value; }
        public Rectangle BoundingBox { get => new Rectangle((int)Position.X, (int)Position.Y, (int)this.Width, (int)this.Height); }
        public IPlayer Owner { get => _owner; set => this._owner = value; }
        public virtual string PopupIcon { get => "ERROR_WeaponPopupIcon"; }
        public virtual string ItemIcon { get => "ERROR_WeaponItemIcon"; }
        public float Width => this.SpriteSheet.Sprite(this.ItemIcon).Size.X;
        public float Height => this.SpriteSheet.Sprite(this.ItemIcon).Size.Y;

        // IGun
        public virtual WeaponModifiers Modifiers { get => WeaponModifiers.None; }
        public virtual double BaseDamage { get => 10; }
        public virtual double BaseVelocity { get => 1600; }
        public virtual double BaseRange { get => 400; }
        public virtual int BaseRPM { get => 1100;  }
        public virtual double BaseRPS { get => this.BaseRPM / 60; }
        public virtual double BaseFiringDelay { get => 1 / this.BaseRPS;  }
        public virtual double BaseReloadDelay { get => 1; }
        public virtual int BaseMagSize { get => 64; } // size is how big it can get, count is how many it currently has
        public virtual int BaseReserveSize { get => 960; }
        public virtual int MagCount { get; set; }
        public virtual int ReserveCount { get; set; }
        public virtual Vector2 FirePosition { get => new Vector2(this.Width, 7) + this.Position; } // position at which the bullets are created

        public SpriteFrame PopupFrame { get => this.SpriteSheet.Sprite(this.PopupIcon); }
        public SpriteFrame ItemFrame { get => this.SpriteSheet.Sprite(this.ItemIcon); }

        // Weapon
        private Vector2 _position;
        private double _timeSinceLastBullet; // 
        private double _dryFireDelay = 0.15; // minimum amount of time between out of ammo clicks in seconds
        private double _timeReloading;
        private bool _reloading;
        private readonly IInput _input;
        private readonly IContentManager _contentManager;
        private IPlayer _owner;
        public bool PlayerClose;
        public List<Bullet> _bullets;
        
        public Weapon(IInput input, IContentManager contentManager, Guid guid, Vector2 spawnLocation)
        {
            this._input = input ?? throw new ArgumentNullException("input");
            this._contentManager = contentManager ?? throw new ArgumentNullException("contentManager");

            this.Guid = guid;
            this._position = spawnLocation;

            this.MagCount = this.BaseMagSize;
            this.ReserveCount = this.BaseReserveSize;

            this._bullets = new List<Bullet>();
            this._timeSinceLastBullet = 0;
            this._timeReloading = 0;
            this._reloading = false;

            this.SpriteSheet = this._contentManager.GetSpriteSheet("Guns");
            

            this.PlayerClose = false;
        }

        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            this._timeSinceLastBullet += gameTime.ElapsedGameTime.TotalSeconds;
            //AnimatedSprite.Update(gameTime);

            foreach (Bullet bullet in this._bullets)
            {
                bullet.Update(gameTime);
            }

            // TODO: remember the tooth
            // put this in the particle manager?
            // something like that, bullet manager mabye??
            for (int i = 0; i < this._bullets.Count; i++)
            {
                if (this._bullets[i].Expired)
                {
                    this._bullets[i] = null;
                    this._bullets.RemoveAt(i);
                    i--;
                }
            }

            if (!this._reloading)
            {
                if (_input.GetButtonState(Buttons.RightTrigger) == ButtonState.Pressed || (_input.GetButtonState(Buttons.RightTrigger) == ButtonState.Held && this.Modifiers.HasFlag(WeaponModifiers.Auto)))
                {
                    // get the angle to fire at from the right thumbstick
                    this.Fire(Math.Atan2(_input.GetRightThumbstickVector().Y, _input.GetRightThumbstickVector().X));
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
                    string insertSound = Utilities.GetRandomListMember<string>(new List<string> { "smg/insert1", "smg/insert2", "smg/insert3" });
                    this._reloading = false;
                    this._timeReloading = 0;
                    _contentManager.GetSoundEffect(insertSound).Play(0.6f, 0, 0);
                }
            }
        }

        public void Draw(SpriteRender spriteRender)
        {
            // tilt gun down to give a little reload animation
            if (!_reloading)
                spriteRender.Draw(this.SpriteSheet.Sprite(this.ItemIcon), this.Position);
            else
                spriteRender.Draw(this.SpriteSheet.Sprite(this.ItemIcon), this.Position, null, 0.25f);

            foreach (Bullet bullet in this._bullets)
                bullet.Draw(spriteRender);
        }

        public void Fire(double angle)
        {
            if (this.MagCount > 0)
            {
                if (this._timeSinceLastBullet > this.BaseFiringDelay)
                {
                    string sound = Utilities.GetRandomListMember<string>(new List<string> { "smg/shot1", "smg/shot2", "smg/shot3", "smg/shot4", "smg/shot5", "smg/shot6" });
                    ObjectManager.Get<Debug>("debug").Add(sound, 3);

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
                    string sound = Utilities.GetRandomListMember<string>(new List<string> { "smg/dryfire1", "smg/dryfire2" });
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

                    string ejectSound = Utilities.GetRandomListMember<string>(new List<string> { "smg/eject1", "smg/eject2", "smg/eject3" });
                    _contentManager.GetSoundEffect(ejectSound).Play(0.6f, 0, 0);
                }
            }
        }

        public void Pickup(IPlayer owner)
        {
            this._owner = owner;
        }

        public void Drop(Vector2 position)
        {
            this._owner = null;
            this._position = position;
        }
    }
}
