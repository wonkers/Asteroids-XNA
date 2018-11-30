using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moving_Vectors
{
    public class BulletController
    {
        public List<Bullet> Bullets = new List<Bullet>();
        public bool CanShoot = true;

        public BulletController()
        {
        }
        public void Move(Bullet bullet)
        {
            bullet.position.X += bullet.trajectory.X * 3;
            bullet.position.Y -= bullet.trajectory.Y * 3;

            bullet.sprite.X = (int)bullet.position.X;
            bullet.sprite.Y = (int)bullet.position.Y;

            if (bullet.position.X < -16 || bullet.position.Y < -16 ||
                bullet.position.X > 784 || bullet.position.Y > 464)
            {
                RemoveBullet(bullet);
            }

        }
        public void RemoveBullet(Bullet bullet)
        {
            Bullets.Remove(bullet);
        }
    }
}
