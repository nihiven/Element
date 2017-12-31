using Element.Classes;
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
        IGameOptions _theGame;
        IContentManager _contentManager;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteRender spriteRender; // TexturePacker
        List<IComponent> components = new List<IComponent>();

        public ElementGame()
        {
            this._theGame = (IGameOptions)ComponentFactory.New("theGame");
            ObjectManager.Add("theGame", this._theGame); // core

            // we use the object manager with our 'GraphicsManager', which is a GraphicsDeviceManager that implements IComponent, IGraphics
            // IGraphics is the interface that allows classes to get screen properties and is used for Dependency Injection
            ObjectManager.Add("graphics", new GraphicsManager(this));
            graphics = ObjectManager.Get<GraphicsDeviceManager>("graphics");

            // TODO: move this
            // run fast
            //this.TargetElapsedTime = TimeSpan.FromSeconds(FPS.ONEFORTYFOUR);
            //graphics.SynchronizeWithVerticalRetrace = true; // vsync
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // intialize every component
            foreach (IComponent component in components)
                component.Initialize();
            
            // call base class initialize
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // texturePacker spriter render class
            this.spriteRender = new SpriteRender(this.spriteBatch);

            /// the master blaster
            this._contentManager = new ContentManagement(Content);
            Content.RootDirectory = "Content";
            ObjectManager.Add("contentManager", this._contentManager);


            // load all of the textures here for now
            // eventually we will move them into a resource file
            // and load them from there

            // TEXTURE PACKER
            _contentManager.AddSpriteSheet("Guns", "Guns");


            // CONTROLLER DEBUG
            _contentManager.AddSpriteSheetJB(identifier: "controllerDebug", contentLocation: "controllerDebug/Xbox360PixelPadtrans", rows: 4, columns: 9);
            _contentManager.AddFont("Arial", Content.Load<SpriteFont>("Arial"));

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

            // JADE RABBIT
            _contentManager.AddSpriteSheetJB(identifier: "JadeRabbit", contentLocation: "weapons/jadeRabbitTiny", rows: 1, columns: 1);
            _contentManager.AddAnimation(identifier: "JadeRabbit:Fire", spriteSheetIdentifier: "JadeRabbit", startFrame: 1, frameCount: 1, framesPerSecond: 1);
            _contentManager.AddAnimatedSprite("JadeRabbit", new AnimatedSprite());
            _contentManager.GetAnimatedSprite("JadeRabbit").AddAnimation(_contentManager.GetAnimation("JadeRabbit:Fire"));

            // BULLET
            _contentManager.AddSpriteSheetJB("bullet", "weapons/bullet", 1, 1);
            _contentManager.AddAnimation("bullet", "bullet", 1, 1, 1);
            _contentManager.AddAnimatedSprite("bullet", new AnimatedSprite());
            _contentManager.GetAnimatedSprite("bullet").AddAnimation(_contentManager.GetAnimation("bullet"));

            // INVENTORY
            _contentManager.AddSoundEffect("buzzer1", "audio/buzzer1");
            _contentManager.AddSoundEffect("Inv_Close", "audio/inventory/Close");
            _contentManager.AddSoundEffect("Inv_Open", "audio/inventory/Open");
            _contentManager.AddSoundEffect("Equip", "audio/inventory/Inv_Equip");
            _contentManager.AddSoundEffect("Inv_Vertical", "audio/inventory/Inv_Vertical");
            _contentManager.AddSoundEffect("Inv_Pickup", "audio/inventory/Pickup");


            // create the game components
            ObjectManager.Add("itemManager", ComponentFactory.New("itemManager")); // core
            ObjectManager.Add("input", ComponentFactory.New("input")); // core
            ObjectManager.Add("controllerDebug", ComponentFactory.New("controllerDebug")); // core
            ObjectManager.Add("itemDebug", ComponentFactory.New("itemDebug")); // core
            ObjectManager.Add("player", ComponentFactory.New("player")); // game

            // add the components
            components.Add(ObjectManager.Get<IComponent>("itemManager")); // core
            components.Add(ObjectManager.Get<IComponent>("input")); // core
            components.Add(ObjectManager.Get<IComponent>("controllerDebug")); // core
            components.Add(ObjectManager.Get<IComponent>("itemDebug")); // core
            components.Add(ObjectManager.Get<IComponent>("player")); // game

            // llooooaadd some content
            foreach (IComponent component in components)
                component.LoadContent(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // llooooaadd some content
            foreach (IComponent component in components)
                component.UnloadContent();

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
            // it's gametime
            foreach (IComponent component in components)
                component.Update(gameTime);

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            
            // draw everything
            foreach (IComponent component in components)
                component.Draw(this.spriteRender);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
