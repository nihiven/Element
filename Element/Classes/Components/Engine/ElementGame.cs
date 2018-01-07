using Element.Classes;
using Element.Factories;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TexturePackerLoader;

namespace Element
{  
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ElementGame : Game
    {
        IContentManager _contentManager;
        GraphicsDeviceManager _graphics;
        SpriteBatch spriteBatch;
        SpriteRender spriteRender; // TexturePacker

        // Components
        IInput _input;
        IControllerDebug _controllerDebug;
        IDebug _debug;
        IUpdate _itemDebug;
        IGameManager _gameManager;

        public ElementGame()
        {
            // we use the object manager with our 'GraphicsManager', which is a GraphicsDeviceManager that implements IComponent, IGraphics
            // IGraphics is the interface that allows classes to get screen properties and is used for Dependency Injection
            ObjectManager.Add("graphics", new GraphicsManager(this));
            _graphics = ObjectManager.Get<GraphicsDeviceManager>("graphics");

            // TODO: move this
            // run fast
            //this.TargetElapsedTime = TimeSpan.FromSeconds(FPS.ONEFORTYFOUR);
            //graphics.SynchronizeWithVerticalRetrace = true; // vsync
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // call base class initialize
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your managed content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // texturePacker spriter render class
            this.spriteRender = new SpriteRender(this.spriteBatch);

            /// the master blaster
            this._contentManager = new AssetManager(Content);
            Content.RootDirectory = "Content";
            ObjectManager.Add("contentManager", this._contentManager);

            LoadAssets();

            // create the game components
            _input = ObjectFactory.NewInput();
            ObjectManager.Add("input", _input);

            _debug = ObjectFactory.NewDebug();
            ObjectManager.Add("debug", _debug);

            _controllerDebug = ObjectFactory.NewControllerDebug();
            ObjectManager.Add("controllerDebug", _controllerDebug);

            // TODO: convert these to object factory
            ObjectManager.Add("itemManager", ObjectFactory.NewItemManager()); // game
            ObjectManager.Add("gameOptions", ObjectFactory.NewGameOptions()); // game

            ObjectManager.Add("player", ObjectFactory.NewPlayer()); // game

            ObjectManager.Add("hud", ObjectFactory.NewHud()); // game

            _itemDebug = ObjectFactory.NewItemDebug();
            ObjectManager.Add("itemDebug", _itemDebug);

            _gameManager = ObjectFactory.NewGameManager();
            ObjectManager.Add("gameManager", ObjectFactory.NewGameManager());
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _input.Update(gameTime);
            _debug.Update(gameTime);
            _itemDebug.Update(gameTime);
            _gameManager.Update(gameTime);

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);
            spriteBatch.Begin();

            _controllerDebug.Draw(spriteRender);
            _debug.Draw(spriteRender);
            _gameManager.Draw(spriteRender);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void LoadAssets()
        {

            // load all of the textures here for now
            // eventually we will move them into a resource file
            // and load them from there

            // TEXTURE PACKER
            _contentManager.AddSpriteSheet("Guns", "Guns");

            // CONTROLLER DEBUG
            _contentManager.AddSpriteSheetJB(identifier: "controllerDebug", contentLocation: "controllerDebug/Xbox360PixelPadtrans", rows: 4, columns: 9);
            _contentManager.AddFont("Arial", Content.Load<SpriteFont>("fonts/Arial"));
            _contentManager.AddFont("ArialBig", Content.Load<SpriteFont>("fonts/ArialBig"));
            _contentManager.AddFont("Impact", Content.Load<SpriteFont>("fonts/Impact"));
            _contentManager.AddFont("ImpactBig", Content.Load<SpriteFont>("fonts/ImpactBig"));

            // PLAYER
            // TODO: read this data from a file
            _contentManager.AddSpriteSheetJB(identifier: "female_walkcycle", contentLocation: "female_walkcycle", rows: 4, columns: 9);
            _contentManager.AddAnimation(identifier: "female_walk_up", spriteSheetIdentifier: "female_walkcycle", startFrame: 1, frameCount: 9, framesPerSecond: FPS.TEN);
            _contentManager.AddAnimation(identifier: "female_walk_down", spriteSheetIdentifier: "female_walkcycle", startFrame: 19, frameCount: 9, framesPerSecond: FPS.TEN);
            _contentManager.AddAnimation(identifier: "female_walk_left", spriteSheetIdentifier: "female_walkcycle", startFrame: 10, frameCount: 9, framesPerSecond: FPS.TEN);
            _contentManager.AddAnimation(identifier: "female_walk_right", spriteSheetIdentifier: "female_walkcycle", startFrame: 28, frameCount: 9, framesPerSecond: FPS.TEN);
            _contentManager.AddAnimatedSprite("female", new AnimatedSprite());
            _contentManager.GetAnimatedSprite("female").AddAnimation(_contentManager.GetAnimation("female_walk_up"));
            _contentManager.GetAnimatedSprite("female").AddAnimation(_contentManager.GetAnimation("female_walk_down"));
            _contentManager.GetAnimatedSprite("female").AddAnimation(_contentManager.GetAnimation("female_walk_left"));
            _contentManager.GetAnimatedSprite("female").AddAnimation(_contentManager.GetAnimation("female_walk_right"));

            // Weapon audio
            _contentManager.AddSoundEffect("smg/shot1", "soundEffects/weapons/smg/snd_SHOT_01.SoundNodeWave_00000122");
            _contentManager.AddSoundEffect("smg/shot2", "soundEffects/weapons/smg/snd_SHOT_02.SoundNodeWave_00000122");
            _contentManager.AddSoundEffect("smg/shot3", "soundEffects/weapons/smg/snd_SHOT_03.SoundNodeWave_00000122");
            _contentManager.AddSoundEffect("smg/shot4", "soundEffects/weapons/smg/snd_SHOT_04.SoundNodeWave_00000122");
            _contentManager.AddSoundEffect("smg/shot5", "soundEffects/weapons/smg/snd_SHOT_05.SoundNodeWave_00000122");
            _contentManager.AddSoundEffect("smg/shot6", "soundEffects/weapons/smg/snd_SHOT_06.SoundNodeWave_00000122");
            _contentManager.AddSoundEffect("smg/dryfire1", "soundEffects/weapons/smg/snd_DRYFIRE_01.SoundNodeWave_00000125");
            _contentManager.AddSoundEffect("smg/dryfire2", "soundEffects/weapons/smg/snd_DRYFIRE_02.SoundNodeWave_00000125");
            _contentManager.AddSoundEffect("smg/eject1", "soundEffects/weapons/smg/snd_CLIP_EJECT_01.SoundNodeWave_00000128");
            _contentManager.AddSoundEffect("smg/eject2", "soundEffects/weapons/smg/snd_CLIP_EJECT_02.SoundNodeWave_00000128");
            _contentManager.AddSoundEffect("smg/eject3", "soundEffects/weapons/smg/snd_CLIP_EJECT_03.SoundNodeWave_00000128");
            _contentManager.AddSoundEffect("smg/insert1", "soundEffects/weapons/smg/snd_CLIP_insert_01.SoundNodeWave_00000129");
            _contentManager.AddSoundEffect("smg/insert2", "soundEffects/weapons/smg/snd_CLIP_insert_02.SoundNodeWave_00000129");
            _contentManager.AddSoundEffect("smg/insert3", "soundEffects/weapons/smg/snd_CLIP_insert_03.SoundNodeWave_00000129");

            // PLAYER audio
            _contentManager.AddSoundEffect("footstep1", "soundEffects/player/footsteps/Footstep_Concrete_Run_01.SoundNodeWave_00000174");
            _contentManager.AddSoundEffect("footstep2", "soundEffects/player/footsteps/Footstep_Concrete_Run_02.SoundNodeWave_00000174");
            _contentManager.AddSoundEffect("footstep3", "soundEffects/player/footsteps/Footstep_Concrete_Run_03.SoundNodeWave_00000174");
            _contentManager.AddSoundEffect("footstep4", "soundEffects/player/footsteps/Footstep_Concrete_Run_04.SoundNodeWave_00000174");
            _contentManager.AddSoundEffect("footstep5", "soundEffects/player/footsteps/Footstep_Concrete_Run_05.SoundNodeWave_00000174");
            _contentManager.AddSoundEffect("footstep6", "soundEffects/player/footsteps/Footstep_Concrete_Run_06.SoundNodeWave_00000174");
            _contentManager.AddSoundEffect("footstep7", "soundEffects/player/footsteps/Footstep_Concrete_Run_07.SoundNodeWave_00000174");
            _contentManager.AddSoundEffect("footstep8", "soundEffects/player/footsteps/Footstep_Concrete_Run_08.SoundNodeWave_00000174");
            _contentManager.AddSoundEffect("footstep9", "soundEffects/player/footsteps/Footstep_Concrete_Run_09.SoundNodeWave_00000174");
            _contentManager.AddSoundEffect("footstep10", "soundEffects/player/footsteps/Footstep_Concrete_Run_10.SoundNodeWave_00000174");
            _contentManager.AddSoundEffect("footstep11", "soundEffects/player/footsteps/Footstep_Concrete_Run_11.SoundNodeWave_00000174");
            _contentManager.AddSoundEffect("footstep12", "soundEffects/player/footsteps/Footstep_Concrete_Run_12.SoundNodeWave_00000174");


            // BULLET
            _contentManager.AddSpriteSheetJB("bullet", "weapons/bullet", 1, 1);
            _contentManager.AddAnimation("bullet", "bullet", 1, 1, 1);
            _contentManager.AddAnimatedSprite("bullet", new AnimatedSprite());
            _contentManager.GetAnimatedSprite("bullet").AddAnimation(_contentManager.GetAnimation("bullet"));

            // INVENTORY
            _contentManager.AddSoundEffect("buzzer1", "soundEffects/ui/inventory/buzzer1");
            _contentManager.AddSoundEffect("Inv_Close", "soundEffects/ui/inventory/Close");
            _contentManager.AddSoundEffect("Inv_Open", "soundEffects/ui/inventory/Open");
            _contentManager.AddSoundEffect("Equip", "soundEffects/ui/inventory/Inv_Equip");
            _contentManager.AddSoundEffect("Inv_Vertical", "soundEffects/ui/inventory/Inv_Vertical");
            _contentManager.AddSoundEffect("Inv_Pickup", "soundEffects/ui/inventory/Pickup");
        }

    }
}
