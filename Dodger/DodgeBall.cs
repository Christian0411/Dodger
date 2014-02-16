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
    class DodgeBall
    {
        Texture2D texture; 
        Rectangle position;
        Rectangle startingPosition;
        Random rand;
        public Rectangle Position { get { return position; } }
        int velocity;

        public DodgeBall() 
        {
            rand = new Random();
            velocity = rand.Next(5,10);
            SetStartingPosition();
        }

        public void SetStartingPosition() 
        {
            startingPosition = new Rectangle(rand.Next(0, 800), -60, 20, 20);
            position = startingPosition;
        }

        public void LoadDodgeBallContent(ContentManager cm) 
        {
            texture = cm.Load<Texture2D>("Player");
        }

        public void Draw(SpriteBatch sb) 
        {
            sb.Draw(texture, position, Color.Blue);
        }

        public void Update(GameTime gameTime) 
        {
            position.Y += velocity;
            rand = new Random(gameTime.ElapsedGameTime.Milliseconds); //Update the random so that the seed changes everytime.
        }
        public bool isTextureNull() 
        {
            if (texture == null) 
            {
                return true;
            }
            return false;
        }
    }
}
