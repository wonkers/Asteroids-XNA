using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Moving_Vectors
{
    public class Bullet
    {
        public Vector2 trajectory;
        public Vector2 position;
        public Rectangle sprite;
        public Texture2D tex;
        public float angle;

        public Bullet(Vector2 Trajecory, Vector2 Position, Texture2D texture)
        {
            trajectory = Trajecory;
            position = Position;
            tex = texture;
            sprite = new Rectangle((int)position.X, (int)position.Y, 4, 4);

        }
        public Bullet()
        {
        }
        
    }
}
