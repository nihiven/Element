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
        List<IComponent> objects = new List<IComponent>();
        
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
            // create the components
            ObjectManager.Add("input", ComponentFactory.New("input"));
            ObjectManager.Add("soundEffects", ComponentFactory.New("soundEffects"));
            ObjectManager.Add("controllerDebug", ComponentFactory.New("controllerDebug"));

            // add the components
            objects.Add((IComponent)ObjectManager.Get("input"));
            objects.Add((IComponent)ObjectManager.Get("controllerDebug"));
            objects.Add((IComponent)ObjectManager.Get("soundEffects"));


            // intialize every component
            foreach (IComponent component in objects)
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

            // llooooaadd some content
            foreach (IComponent component in objects)
                component.LoadContent(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // llooooaadd some content
            foreach (IComponent component in objects)
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
            foreach (IComponent component in objects)
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            // llooooaadd some content
            foreach (IComponent component in objects)
                component.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
