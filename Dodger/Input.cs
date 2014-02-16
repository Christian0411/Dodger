using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;



namespace Dodger
{
    static class Input // static Input class which grabs the mouse position
    {
        static Rectangle rectMousePosition; // Mouses position in a rectangle
        static MouseState mouse; // the mouse object to get the state

        static int mouseX; // Mouse's X Position
        static int mouseY; // Mouse's Y position

        #region Properties
        public static int MouseX { get { return mouseX; } }
        public static int MouseY { get { return mouseY; } }
        public static Rectangle RectMousePosition { get { return rectMousePosition; } }
        #endregion

        public static void Update() 
        {
            mouse = Mouse.GetState(); // Set the mouse to a new mouse state every update
            mouseX = mouse.X;
            mouseY = mouse.Y;
            rectMousePosition = new Rectangle(mouseX, mouseY, 40, 40);
        }


    }
}
