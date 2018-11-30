using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Moving_Vectors
{
    public class Block
    {
        public enum Size
        {
            Tiny = 1,
            Small = 2,
            Medium = 3,
            Large = 4,
            Huge = 5
        }
        public Size size;
        public Texture2D texture;
        public Rectangle sprite;
        public Vector2 position;
        public Vector2 target;
        public float angle;
        public float speed;
        public float turnSpeed;

        public Vector2 MovementVector;
        public Rectangle BoundsBox;

        public Block()
        {
            sprite = new Rectangle();
            position = new Vector2();
            angle = 0.0f;
            target = new Vector2();
        }
        public Block(Size _size, Texture2D tex, Vector2 pos, float a, float s, float turn)
        {

            sprite = new Rectangle((int)pos.X, (int)pos.Y, (int)_size * 16, (int)_size * 16);
            BoundsBox = sprite;
            texture = tex;
            size = _size;
            position = pos;
            angle = a;
            speed = s;
            turnSpeed = turn;
            target = new Vector2();
            MovementVector = new Vector2();
            
        }
        public void  SetTarget(Vector2 tar)
        {
            target = tar;
            MovementVector = target - position;
            MovementVector.Normalize();
        }
        
        public void SetTarget(Random rand)
        {
           int wall;
            wall = rand.Next(4);
            switch (wall)
            {
                case 0:
                    target.X = rand.Next(800);
                    target.Y = -16;
                    break;
                case 1:
                    target.X = rand.Next(800);
                    target.Y = 496;
                    break;
                case 2:
                    target.Y = rand.Next(480);
                    target.X = -16;
                    break;
                case 3:
                    target.Y = rand.Next(480);
                    target.X = 816;
                    break;
            }

            MovementVector = target - position;
            MovementVector.Normalize();
        }
        
        public void SetPosition(Random rand)
        {
            int wall;
            wall = rand.Next(4);
            switch (wall)
            {
                case 0:
                    position.X = rand.Next(800);
                    position.Y = 0;
                    break;
                case 1:
                    position.X = rand.Next(800);
                    position.Y = 480;
                    break;
                case 2:
                    position.Y = rand.Next(480);
                    position.X = 0;
                    break;
                case 3:
                    position.Y = rand.Next(480);
                    position.X = 800;
                    break;

            }
        }
        public void SetSpeed(Random rand)
        {
            float s = rand.Next(15) + 5;
            speed = s / 10;
        }
    }
}
