﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShooterRevamped.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpaceShooterRevamped
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game 
    {

        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Random Random;
        public static int ScreenWidth;
        public static int ScreenHeight;

        private float _delay = 1;
        private float _elapsed;
        private static Texture2D rockTexture;
        private List<Sprite> _sprites;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _elapsed = _delay;
            Random = new Random();

            ScreenWidth = 1280;
            ScreenHeight = 720;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Console.WriteLine("Loading");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var shipTexture = Content.Load<Texture2D>("SpaceShipt");
            rockTexture = Content.Load<Texture2D>("rock");

            _sprites = new List<Sprite>
            {
                new Ship(shipTexture)
                {
                    Position = new Vector2(100,100),
                    Bullet = new Bullet(Content.Load<Texture2D>("Bob"))
                },
                new Rock(rockTexture)
            };

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _elapsed -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_elapsed <= 0f){
                Debug.WriteLine("Spawning");
                _sprites.Add(new Rock(rockTexture));
                _elapsed = _delay;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);

            PostUpdate(); //Handles all the sprites that are supposed to be dead
        }

        private void PostUpdate()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (Sprite sprite in _sprites)
                sprite.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
