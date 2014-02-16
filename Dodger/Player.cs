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
    class Player
    {
        Texture2D playerTexture;
        Rectangle playerPosition;

        
        public void LoadPlayerContent(ContentManager cm)
        {
            playerTexture = cm.Load<Texture2D>("player");
        }
        public void Update() 
        {
            playerPosition.Width = 20; // Set the width and 
            playerPosition.Height = 20; // height as 20 x 20

            playerPosition.X = Input.MouseX - 10; // set the X position and
            playerPosition.Y = Input.MouseY - 10; // Y position to the mouse position X and Y minus 10 so that it gets the center of the texture 

            #region Keep player position in bounds (If statements)
            if (Input.MouseX == 0) 
            {
                playerPosition.X = Input.MouseX + 1;
            }

            if (Input.MouseX >= 799) 
            {
                playerPosition.X = 780;
            }

            if (Input.MouseY == 0)
            {
                playerPosition.Y = Input.MouseY;
            }

            if (Input.MouseY == 599)
            {
                playerPosition.Y = 580;
            }

            if (Input.MouseX == 799)
            {
                playerPosition.X = 780;
            }
            #endregion
        }

        public bool CollidesWith(Rectangle rect) 
        {
            if (playerPosition.Intersects(rect)) // return true if the player position collides with given rectangle
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch sb) 
        {
            sb.Draw(playerTexture, playerPosition, Color.Red);
        }
    }
}
