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
    class GameManager : IGameManager
    {
        public bool Enabled => true;

        IGameOptions _gameOptions;
        IDebug _debug;
        IPlayer _player;
        IItemManager _itemManager;
        IHud _hud;

        List<IProjectile> _projectiles;
        

        public GameManager(IGameOptions gameOptions, IDebug debug, IItemManager itemManager, IHud hud, IPlayer player)
        {
            _gameOptions = gameOptions ?? throw new ArgumentNullException("gameOptions");
            _debug = debug ?? throw new ArgumentNullException("debug");
            _player = player ?? throw new ArgumentNullException("player");
            _itemManager = itemManager ?? throw new ArgumentNullException("itemManager");
            _hud = hud ?? throw new ArgumentNullException("hud");
        }

        public void Draw(SpriteRender spriteRender)
        {
            _player.Draw(spriteRender);
        }

        public void Initialize()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            _debug.Add(text: "This is me!", duration: 0);
        }
    }
}
