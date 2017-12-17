using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
        List<IActor> actorList = new List<IActor>();
        List<IComponent> componentList = new List<IComponent>();
        XB1Pad input = new XB1Pad();
        
        public ElementGame()
        {
            

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // run fast
            this.TargetElapsedTime = TimeSpan.FromSeconds(FPS.ONEFORTYFOUR);
            graphics.SynchronizeWithVerticalRetrace = false; // vsync

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // components
            input.Initialize();

            // actors
            actorList.Add(new Player());
            actorList.Add(new SoundEffects());

            foreach (IActor actor in actorList)
                actor.Initialize();

            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            foreach (IActor actor in actorList)
                actor.LoadContent(this.Content);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            foreach (IActor actor in actorList)
                actor.UnloadContent();
            
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
            // components
            input.Update(gameTime);

            // actors
            foreach (IActor actor in actorList)
                actor.Update(gameTime, ref input);

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach (IActor actor in actorList)
                actor.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
