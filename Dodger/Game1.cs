#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Dodger
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        const int GAME_WINDOW_WIDTH = 800;
        const int GAME_WINDOW_HEIGHT = 600;

        double timer = 5000;
        Player player;

        List<DodgeBall> balls;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = GAME_WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = GAME_WINDOW_WIDTH;
            this.IsMouseVisible = false; // Hide the mouse

            player = new Player();
            balls = new List<DodgeBall>();
            DodgeBall ball = new DodgeBall();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadPlayerContent(Content);

            foreach (DodgeBall n in balls) 
            {
                n.LoadDodgeBallContent(Content);
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            timer -= gameTime.TotalGameTime.TotalMilliseconds; //timer 

            if (timer <= 0)
            {
                timer = 20000; //reset the timer
                if (balls.Count != 50) //only create balls if there are not 50 already on the screen
                {
                    DodgeBall ball = new DodgeBall();
                    ball.LoadDodgeBallContent(Content);
                    balls.Add(ball);
                }
            }


            for (int i = 0; i < balls.Count; i++)          
            {
                if (balls[i].Position.Y > 820) 
                {
                    balls.Remove(balls[i]); //remove the balls from the list if they are off the screen
                }
            }
            
            Input.Update(); // Update the mouse
            player.Update(); // Update the player
            foreach (DodgeBall n in balls) // Update all the balls
            {
                n.Update(gameTime);
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            foreach (DodgeBall n in balls)
            {
                if (n.isTextureNull() == false)
                {
                    n.Draw(spriteBatch);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
