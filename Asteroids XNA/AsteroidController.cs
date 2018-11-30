using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Moving_Vectors
{
    public class AsteroidController
    {
        Random rand;
        public List<Block> Asteroids = new List<Block>();

        public AsteroidController()
        {
            rand = new Random();
        }
        public void UpdateBlockPosition(Block block)
        {

            if ( isWithin((int)block.target.X, (int)block.position.X) &&
                isWithin((int)block.target.Y, (int)block.position.Y))
            {
                //ExplodeAsteroid(block);
            }
           

            if (block.position.Y > 480 - (block.sprite.Width/2))
            {
                block.MovementVector.Y *= -1;
            }
            else if (block.position.Y < 0 + (block.sprite.Width / 2))
            {
                block.MovementVector.Y *= -1;
            }
            else if (block.position.X > 800 - (block.sprite.Width / 2))
            {
                block.MovementVector.X *= -1;
            }
            else if (block.position.X < 0 + (block.sprite.Width / 2))
            {
                block.MovementVector.X *= -1;
            }
            
            block.position.X += block.MovementVector.X * block.speed;
            block.position.Y += block.MovementVector.Y * block.speed;

            block.sprite.X = (int)block.position.X;
            block.sprite.Y = (int)block.position.Y;

            block.BoundsBox.X = block.sprite.X - (block.sprite.Width / 2);
            block.BoundsBox.Y = block.sprite.Y - (block.sprite.Width / 2);
            
        }
        bool isWithin(int a, int b)
        {
            if (Math.Abs(a - b) < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExplodeAsteroid(Block asteroid)
        {
            switch (asteroid.size)
            {
                case Block.Size.Tiny:
                    Asteroids.Remove(asteroid);
                    break;
                case Block.Size.Small:
                    Asteroids.Add(new Block(Block.Size.Tiny, asteroid.texture, asteroid.position, MathHelper.ToRadians(15), 1.8f, 0.0f));
                    Asteroids[Asteroids.Count - 1].SetTarget(rand);
                    Asteroids.Add(new Block(Block.Size.Tiny, asteroid.texture, asteroid.position, MathHelper.ToRadians(25), 1.8f, 0.0f));
                    Asteroids[Asteroids.Count - 1].SetTarget(rand);
                    Asteroids.Add(new Block(Block.Size.Tiny, asteroid.texture, asteroid.position, MathHelper.ToRadians(35), 1.8f, 0.0f));
                    Asteroids[Asteroids.Count - 1].SetTarget(rand);
                    Asteroids.Add(new Block(Block.Size.Tiny, asteroid.texture, asteroid.position, MathHelper.ToRadians(45), 1.8f, 0.0f));
                    Asteroids[Asteroids.Count - 1].SetTarget(rand);
                    Asteroids.Remove(asteroid);
                     
                    break;
                case Block.Size.Medium:
                    Asteroids.Add(new Block(Block.Size.Small, asteroid.texture, asteroid.position, MathHelper.ToRadians(15), 1.8f, 0.0f));
                    Asteroids[Asteroids.Count - 1].SetTarget(rand);
                    Asteroids.Add(new Block(Block.Size.Small, asteroid.texture, asteroid.position, MathHelper.ToRadians(35), 1.8f, 0.0f));
                    Asteroids[Asteroids.Count - 1].SetTarget(rand);
                    Asteroids.Add(new Block(Block.Size.Small, asteroid.texture, asteroid.position, MathHelper.ToRadians(75), 1.8f, 0.0f));
                    Asteroids[Asteroids.Count - 1].SetTarget(rand);

                    Asteroids.Remove(asteroid);
                    break;
                case Block.Size.Large:
                    Asteroids.Add(new Block(Block.Size.Medium, asteroid.texture, asteroid.position, MathHelper.ToRadians(35), 1.8f, 0.0f));
                    Asteroids[Asteroids.Count - 1].SetTarget(rand);
                    Asteroids.Add(new Block(Block.Size.Medium, asteroid.texture, asteroid.position, MathHelper.ToRadians(75), 1.8f, 0.0f));
                    Asteroids[Asteroids.Count - 1].SetTarget(rand);

                    Asteroids.Remove(asteroid);
                    break;
                case Block.Size.Huge:
                    Asteroids.Add(new Block(Block.Size.Medium, asteroid.texture, asteroid.position, MathHelper.ToRadians(15), 1.8f, MathHelper.ToRadians(15)));
                    Asteroids[Asteroids.Count - 1].SetTarget(rand);
                    Asteroids.Add(new Block(Block.Size.Medium, asteroid.texture, asteroid.position, MathHelper.ToRadians(25), 1.8f, MathHelper.ToRadians(25)));
                    Asteroids[Asteroids.Count - 1].SetTarget(rand);
                    Asteroids.Add(new Block(Block.Size.Medium, asteroid.texture, asteroid.position, MathHelper.ToRadians(45), 1.8f, MathHelper.ToRadians(35)));
                    Asteroids[Asteroids.Count - 1].SetTarget(rand);

                    Asteroids.Remove(asteroid);
                    break;
            }
        }
        
    }
}
