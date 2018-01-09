using Element.Classes;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IGameManager : IUpdate, IDraw
    {
    }
}

namespace Element.Managers
{
    internal class GameManager : IGameManager
    {
        public bool Enabled => true;

        private IDebug _debug;
        private IGameOptions _gameOptions;
        private IHud _hud;
        private IInventory _inventory;
        private IItemManager _itemManager;
        private IPlayer _player;
        private IActiveGear _activeGear;

        private List<IProjectile> _projectiles;

        public GameManager(IGameOptions gameOptions, IDebug debug, IItemManager itemManager, IHud hud, IPlayer player, IInventory inventory, IActiveGear activeGear)
        {
            // components
            _debug = debug ?? throw new ArgumentNullException(ComponentStrings.Debug);
            _gameOptions = gameOptions ?? throw new ArgumentNullException(ComponentStrings.GameOptions);
            _hud = hud ?? throw new ArgumentNullException(ComponentStrings.HUD);
            _inventory = inventory ?? throw new ArgumentNullException(ComponentStrings.Inventory);
            _itemManager = itemManager ?? throw new ArgumentNullException(ComponentStrings.ItemManager);
            _player = player ?? throw new ArgumentNullException(ComponentStrings.Player);
            _activeGear = activeGear ?? throw new ArgumentNullException(ComponentStrings.ActiveGear);

            // happy projectiles
            _projectiles = new List<IProjectile>();
        }

        public void Update(GameTime gameTime)
        {
            // components that implement IUpdate
            _debug.Update(gameTime);
            _inventory.Update(gameTime);
            _player.Update(gameTime);

            foreach (IProjectile projectile in _projectiles)
                projectile.Update(gameTime);
        }

        public void Draw(SpriteRender spriteRender)
        {
            // components that implement IDraw
            _debug.Draw(spriteRender);
            _hud.Draw(spriteRender);
            _inventory.Draw(spriteRender);
            _player.Draw(spriteRender);
            _activeGear.Draw(spriteRender);

            foreach (IProjectile projectile in _projectiles)
                projectile.Draw(spriteRender);
        }

        public void LoadContent(ContentManager content)
        {
        }

        public void UnloadContent()
        {
        }

    }
}