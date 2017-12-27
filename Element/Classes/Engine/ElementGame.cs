using Element.Classes;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Element
{  
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ElementGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<IComponent> components = new List<IComponent>();

        // these are temporary
        // we need to make some kind of resource manager
        // maybe subclass the content manager and go from there
        private Dictionary<string, SpriteSheet> spriteSheets = new Dictionary<string, SpriteSheet>();
        private Dictionary<string, AnimatedSprite> animatedSprites = new Dictionary<string, AnimatedSprite>();
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        
        public ElementGame()
        {
            // this is crappy,  i think
            // add the ContentManager to the global object list so we can get to it from anywhere?
            ObjectManager.Add("contentManager", Content);
            Content.RootDirectory = "Content"; // set out root content directory

            // we use the object manager with our 'GraphicsManager', which is a GraphicsDeviceManager that implements IComponent, IGraphics
            // IGraphics is the interface that allows classes to get screen properties and is used for Dependency Injection
            ObjectManager.Add("graphics", new GraphicsManager(this));
            graphics = (GraphicsDeviceManager)ObjectManager.Get("graphics");

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
            // create the game components
            ObjectManager.Add("itemManager", ComponentFactory.New("itemManager")); // core
            ObjectManager.Add("input", ComponentFactory.New("input")); // core
            ObjectManager.Add("soundEffects", ComponentFactory.New("soundEffects")); // core
            ObjectManager.Add("controllerDebug", ComponentFactory.New("controllerDebug")); // core
            ObjectManager.Add("itemDebug", ComponentFactory.New("itemDebug")); // core
            ObjectManager.Add("player", ComponentFactory.New("player")); // game


            // HERE WE GO
            // AN ITEM MANAGER


            // add the components
            components.Add((IComponent)ObjectManager.Get("itemManager")); // core
            components.Add((IComponent)ObjectManager.Get("input")); // core
            components.Add((IComponent)ObjectManager.Get("controllerDebug")); // core
            components.Add((IComponent)ObjectManager.Get("itemDebug")); // core
            components.Add((IComponent)ObjectManager.Get("soundEffects")); // core
            components.Add((IComponent)ObjectManager.Get("player")); // game


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

            // load all of the textures here for now
            // eventually we will move them into a resource file
            // and load them from there

            // PLAYER
            // TODO: read this data from a file
            spriteSheets.Add("female_walkcycle", new SpriteSheet(Content, "female_walkcycle", 4, 9));
            Animation walkUp = new Animation("female_walk_up", spriteSheets["female_walkcycle"], 1, 9, FPS.TEN);
            Animation walkDown = new Animation("female_walk_down", spriteSheets["female_walkcycle"], 19, 9, FPS.TEN);
            Animation walkLeft = new Animation("female_walk_left", spriteSheets["female_walkcycle"], 10, 9, FPS.TEN);
            Animation walkRight = new Animation("female_walk_right", spriteSheets["female_walkcycle"], 28, 9, FPS.TEN);
            animatedSprites.Add("female", new AnimatedSprite());
            animatedSprites["female"].AddAnimation(walkUp);
            animatedSprites["female"].AddAnimation(walkDown);
            animatedSprites["female"].AddAnimation(walkLeft);
            animatedSprites["female"].AddAnimation(walkRight);

            // JADE RABBIT
            spriteSheets.Add("JadeRabbit", new SpriteSheet(Content, "weapons/jadeRabbitTiny", 1, 1));
            animations.Add("JadeRabbit:Fire", new Animation("jadeRabbitFire", spriteSheets["JadeRabbit"], 1, 1, 1));
            animatedSprites.Add("JadeRabbit", new AnimatedSprite());
            animatedSprites["JadeRabbit"].AddAnimation(animations["JadeRabbit:Fire"]);
            
            ObjectManager.Add("animatedSprites", animatedSprites);


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
                component.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
