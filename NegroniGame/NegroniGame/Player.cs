/*   public sealed class Log    {       private static Log instance;       private Log() {}       public static Log Instance       {           get          {             if (instance == null)             {                lock (instance)                {                   if (instance == null)                      instance = new Log();                }             }             return instance;          }       }    }
 */

namespace NegroniGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;

    public static class Player
    {
        // Singleton !
        public static InnerPlayer Instance { get; private set; }

        static Player() { Instance = new InnerPlayer(); }

        public class InnerPlayer : Interfaces.IPlayer
        {
            internal InnerPlayer() { Initialize(); } // empty constructor

            private void Initialize()
            {
                this.Name = "Elvina";
                this.Delay = 200f;
                this.Frames = 0;
                this.PlayerPosition = new Vector2((float)Screens.GameScreen.ScreenWidth / 2, (float)Screens.GameScreen.ScreenHeight / 2 - 50);
            }

            public Vector2 PlayerPosition; // <- Read the last property info in comment

            // playerTextures - right, left, up, down
            public void Move(GameTime gameTime, KeyboardState ks)
            {
                if (ks.IsKeyDown(Keys.Right))
                {
                    if (PlayerPosition.X < Screens.GameScreen.ScreenWidth - 30)
                    {
                        this.PlayerPosition.X += 2f;
                        this.PlayerAnim = this.PlayerTextures[0];
                        this.SourcePosition = Animate(gameTime);
                    }
                }
                else if (ks.IsKeyDown(Keys.Left))
                {
                    if (PlayerPosition.X > 0)
                    {
                        this.PlayerPosition.X -= 2f;
                        this.PlayerAnim = this.PlayerTextures[1];
                        this.SourcePosition = Animate(gameTime);
                    }
                }
                else if (ks.IsKeyDown(Keys.Up))
                {
                    if (PlayerPosition.Y > 0)
                    {
                        this.PlayerPosition.Y -= 2f;
                        this.PlayerAnim = this.PlayerTextures[2];
                        this.SourcePosition = Animate(gameTime);
                    }
                }
                else if (ks.IsKeyDown(Keys.Down))
                {
                    if (PlayerPosition.Y <= Screens.GameScreen.ScreenHeight - 170)
                    {
                        this.PlayerPosition.Y += 2f;
                        this.PlayerAnim = this.PlayerTextures[3];
                        this.SourcePosition = Animate(gameTime);
                    }
                }
                else
                {
                    this.SourcePosition = new Rectangle(64, 0, 32, 32);
                }

                this.DestinationPosition = new Rectangle((int)this.PlayerPosition.X, (int)this.PlayerPosition.Y, 32, 32);
            }

            private Rectangle Animate(GameTime gameTime)
            {
                this.Elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (this.Elapsed >= this.Delay)
                {
                    if (this.Frames >= 2)
                    {
                        this.Frames = 0;
                    }
                    else
                    {
                        this.Frames++;
                    }
                    this.Elapsed = 0;
                }

                // if on frame 0 - top up position 0
                return new Rectangle(32 * this.Frames, 0, 32, 32);
            } 

            public void Draw(SpriteBatch sb)
            {
                new SystemFunctions.Sprite(this.PlayerAnim, this.DestinationPosition, this.SourcePosition).DrawBoxAnim(sb);
            }


            public string Name { get; private set; }
            public List<Texture2D> PlayerTextures { get; set; }
            public Texture2D PlayerAnim { get; set; }
            public float Elapsed { get; private set; }
            public float Delay { get; private set; }
            public int Frames { get; private set; }
            public Rectangle SourcePosition { get; private set; }
            public Rectangle DestinationPosition { get; private set; }
            // public Vector2  PlayerPosition { get; private set; } 
        }
    }
}
