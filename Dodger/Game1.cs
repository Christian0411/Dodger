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
    public enum eGameStates 
    {
        MENU,
        PLAY,

    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int GAME_WINDOW_WIDTH = 800; // Game window height and width 800px by 600px
        const int GAME_WINDOW_HEIGHT = 600;

        double timer = 5000; // start the timer at 5 seconds
        Player player;

        SpriteFont scoreFont;

        public eGameStates gamestate;

        int score;
        int highScore;

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
            gamestate = eGameStates.MENU; // Start the game at the menu gamestate
            player = new Player(); // Initialize the player
            balls = new List<DodgeBall>(); // Create a new list of balls
            this.IsMouseVisible = false; // Hide the mouse

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scoreFont = Content.Load<SpriteFont>("Pong");

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



            HandleCollision();

            if (gamestate == eGameStates.PLAY) // Only do this if the gamestate is play, so while the game is running
            {
                score += 1;
                CreateNewBall(gameTime); // Creates new balls constantly            
                DestroyBallAfterUse(); // Destroys balls after they have left the screen
                Input.Update(); // Update the mouse
                player.Update(); // Update the player
                UpdateBalls(gameTime); // Update the balls
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                gamestate = eGameStates.PLAY;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (gamestate == eGameStates.PLAY) // Only if the gamestate is play
            {
                player.Draw(spriteBatch);
                spriteBatch.DrawString(scoreFont, "Score: " + score.ToString(), Vector2.Zero, Color.White);
                foreach (DodgeBall n in balls) // Draw all the balls only if the textures are loaded
                {
                    if (n.isTextureNull() == false)
                    {
                        n.Draw(spriteBatch);
                    }
                }
            }

            if(gamestate == eGameStates.MENU)
            {
                spriteBatch.DrawString(scoreFont, "Press Enter to Start", new Vector2(GAME_WINDOW_WIDTH / 2 - 50, GAME_WINDOW_HEIGHT / 2), Color.White);
                spriteBatch.DrawString(scoreFont, "HighScore: " + highScore.ToString(), Vector2.Zero, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }


        void CreateNewBall(GameTime gameTime) 
        {
            timer -= gameTime.TotalGameTime.TotalMilliseconds; //timer 
            if (timer <= 0)
            {
                timer = 20000; //reset the timer
                if (balls.Count != 100) //only create balls if there are not 50 already on the screen
                {
                    DodgeBall ball = new DodgeBall();
                    ball.LoadDodgeBallContent(Content);
                    balls.Add(ball);
                }
            }
        }
        void DestroyBallAfterUse() 
        {
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].Position.Y > 820)
                {
                    balls.Remove(balls[i]); //remove the balls from the list if they are off the screen
                }
            }
        }

        void HandleCollision() 
        {
            for (int i = 0; i <balls.Count;i++)            {
                if(player.CollidesWith(balls[i].Position))
                {
                    gamestate = eGameStates.MENU;
                    balls.RemoveRange(0, balls.Count);
                    if (score > highScore) 
                    {
                        highScore = score;
                    }
                    
                    score = 0;
                }
            }
        }

        void UpdateBalls(GameTime gameTime) 
        {
            foreach (DodgeBall n in balls) // Update all the balls
            {
                n.Update(gameTime);
            }
        }
    }
}
