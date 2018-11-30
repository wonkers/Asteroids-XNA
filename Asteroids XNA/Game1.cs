using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Moving_Vectors
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        enum State
        {
            Menu,
            Playing,
            Lost,
            Destroyed
        }
        State GameState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        SpriteFont largeFont;

        Texture2D texture;
        Texture2D shipTexture;
        Texture2D bulletTexture;
        Texture2D background;
        Random rand = new Random();
        
        Vector2 position = new Vector2(384, 226);

        Block ship;
        Block Asteroid;
        
        AsteroidController asteroidController = new AsteroidController();
        BulletController bulletController = new BulletController();

        float time = 0;
        float timer = 0;
        float lostInSpaceTimer = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GameState = State.Playing;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            texture = Content.Load<Texture2D>("Asteroid");
            shipTexture = Content.Load<Texture2D>("Ship");
            bulletTexture = Content.Load<Texture2D>("Bullet");
            background = Content.Load<Texture2D>("wormhole");

            font = Content.Load<SpriteFont>("Ariel");
            largeFont = Content.Load<SpriteFont>("Ariel");

            ship = new Block(Block.Size.Small, shipTexture, position, 0.0f, 2.8f, 0.03f);

            Asteroid = new Block(Block.Size.Huge, texture, new Vector2(41, 41), 0.0f, 1.8f, 0.03f);
            Asteroid.SetTarget(ship.position);

            asteroidController.Asteroids.Add(Asteroid);
           
            
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private float Vector2ToRadian(Vector2 direction)
        {
            return (float)Math.Atan2(direction.X, -direction.Y);
        }
        private Vector2 RadianToVector(float radian)
        {
            return new Vector2((float)Math.Cos(radian), -(float)Math.Sin(radian));
        }

       
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            time += gameTime.ElapsedGameTime.Milliseconds;
            timer += gameTime.ElapsedGameTime.Milliseconds;

            if (timer > 10000)
            {
                timer = 0;
                asteroidController.Asteroids.Add(new Block(Block.Size.Huge, texture, new Vector2(rand.Next(800), 41), 0.0f, 1.8f, 0.03f));
                asteroidController.Asteroids[asteroidController.Asteroids.Count - 1].SetTarget(rand);
            }
            if (time > 300)
            {
                bulletController.CanShoot = true;
                time = 0;
            }
         
            // TODO: Add your update logic here
            
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                ship.angle -= ship.turnSpeed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                ship.angle += ship.turnSpeed;
            }

            if (asteroidController.Asteroids.Count > 0)
            {
                try
                {
                    foreach (Block asteroid in asteroidController.Asteroids)
                    {
                        asteroidController.UpdateBlockPosition(asteroid);
                        asteroid.angle += 0.03f;
                        
                        //CHECK COLLISION
                        if(asteroid.BoundsBox.Intersects(ship.BoundsBox))
                        {
                            asteroidController.ExplodeAsteroid(asteroid);
                        }
                        foreach (Bullet bullet in bulletController.Bullets)
                        {
                            if(asteroid.BoundsBox.Intersects(bullet.sprite))
                             {
                                asteroidController.ExplodeAsteroid(asteroid);
                                bulletController.RemoveBullet(bullet);
                            }
                        }
                        if (asteroidController.Asteroids.Count == 0)
                        {
                            break;
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                    //catch the exception when the Asteroids list is modified within the 
                    //foreach loop
                }
            }


            ship.MovementVector = RadianToVector(ship.angle);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                ship.speed += 0.1f;
                if(ship.speed > 2.8f)
                {
                    ship.speed = 2.8f;
                }
                ship.MovementVector *= ship.speed;
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                ship.speed -= 0.1f;
                if (ship.speed < 0)
                {
                    ship.speed = 0;
                }
                ship.MovementVector *= ship.speed;
            }
            else
            {
                
                ship.speed -= 0.025f;
                //ship.MovementVector *= 0;
                if (ship.speed < 0)
                {
                    ship.speed = 0;
                }
                ship.MovementVector *= ship.speed;
            }
            

            ship.position.X += ship.MovementVector.X;
            ship.position.Y -= ship.MovementVector.Y;
            ship.sprite.X = (int)ship.position.X;
            ship.sprite.Y = (int)ship.position.Y;
            ship.BoundsBox.X = ship.sprite.X - (ship.sprite.Width / 2);
            ship.BoundsBox.Y = ship.sprite.Y - (ship.sprite.Width / 2);

            //space bar to shoot
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (bulletController.CanShoot)
                {
                    bulletController.CanShoot = false;
                    bulletController.Bullets.Add(
                        new Bullet(
                            RadianToVector(ship.angle) * 2,
                            ship.position,
                            bulletTexture)
                            );
                }
            }

            //Move bullets
            if (bulletController.Bullets.Count > 0)
            {
                try
                {

                    foreach (Bullet bullet in bulletController.Bullets)
                    {
                        bulletController.Move(bullet);
                    }
                }
                catch (InvalidOperationException)
                {
                    //catch exception because we've changed list
                }
            }

            if (ship.position.X < 0 || ship.position.Y < 0
                || ship.position.X > 800 || ship.position.Y > 480)
            {
                lostInSpaceTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (lostInSpaceTimer > 3000)
                {
                    //gameover
                    GameState = State.Lost;
                }
            }
            else
            {
                lostInSpaceTimer = 0;
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            //START DRAWING//
            spriteBatch.Begin();
            
            //Background
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            
            //bullets
            if (bulletController.Bullets.Count > 0)
            {
                foreach (Bullet bullet in bulletController.Bullets)
                {
                    spriteBatch.Draw(bullet.tex, bullet.sprite, Color.White);
                }
            }

            //ship
            spriteBatch.Draw(ship.texture, ship.sprite, null, Color.White, ship.angle, new Vector2(16, 16), SpriteEffects.None, 1);
            
            //asteroids
            if (asteroidController.Asteroids.Count > 0)
            {
                foreach (Block asteroid in asteroidController.Asteroids)
                {
                    spriteBatch.Draw(asteroid.texture, asteroid.sprite, null, Color.White, asteroid.angle, new Vector2(32, 32), SpriteEffects.None, 1);
                }
            }

            if (GameState == State.Lost)
            {
                spriteBatch.DrawString(largeFont, "You were Lost in Space", new Vector2(100, 200), Color.Blue);
            }
            //display game info
            //spriteBatch.DrawString(font, ship.position.ToString(), new Vector2(0, 0), Color.White);
            //spriteBatch.DrawString(font, time.ToString(), new Vector2(0, 40), Color.White);
            //spriteBatch.DrawString(font, asteroidController.Asteroids[0].position.ToString(), new Vector2(0, 60), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
