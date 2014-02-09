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
            internal InnerPlayer() { } // empty constructor

            private const float PLAYER_SPEED = 200f;

            public Items.Coins Coins;
            public Items.ElixirHP Elixirs;
            public Items.Weapon.Weapon Weapon;
            public Items.Armor.Armor Shield;
            public Items.Armor.Armor Helmet;
            public Items.Armor.Armor Robe;
            public Items.Armor.Armor Gloves;
            public Items.Armor.Armor Boots;

            public void Initialize(List<Texture2D> playerTextures, List<Texture2D> majesticSetTextures,
                Texture2D coinsTex, Texture2D elixirsTex, Texture2D newbieStaffTex, Texture2D mysticStaffTex)
            {
                this.Name = "Elvina";
                this.Frames = 0;
                this.PlayerPosition = new Vector2((float)Screens.GameScreen.ScreenWidth / 2, (float)Screens.GameScreen.ScreenHeight / 2 - 50);

                this.PlayerTextures = playerTextures;
                this.PlayerAnim = playerTextures[3];
                this.MajesticSetTextures = majesticSetTextures;
                this.NewbieStaffTex = NewbieStaffTex;
                this.MysticStaffTex = mysticStaffTex;


                // Boots, Gloves, Helmet, Robe, Shield
                this.Coins = new Items.Coins(100, coinsTex);
                this.Elixirs = new Items.ElixirHP(2, elixirsTex);
                this.Weapon = new Items.Weapon.NewbieStaff(newbieStaffTex);
                this.Shield = new Items.Armor.Armor();
                this.Helmet = new Items.Armor.Armor();
                this.Robe = new Items.Armor.Armor();
                this.Gloves = new Items.Armor.Armor();
                this.Boots = new Items.Armor.Armor();
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

                if (this.Elapsed >= PLAYER_SPEED)
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

            public void UpdateInventory()
            {
                // When picked up

                // !!!!!!! first check if there is any item of the kind
                // Toolbar.InventorySlots.CheckItems(); <- here is the code for checking

                // !!!!!!! then add
                // this.Coins = new Items.Coins(100, coinsTex);
                // this.Elixirs = new Items.ElixirHP(2, elixirsTex);
                // this.Weapon = new Items.Weapon.NewbieStaff(newbieStaffTex);
                // this.Shield = new Items.Armor.MajesticShield(majesticSetTextures[4]);
                // this.Helmet = new Items.Armor.MajesticHelmet(majesticSetTextures[2]);
                // this.Robe = new Items.Armor.MajesticRobe(majesticSetTextures[3]);
                // this.Gloves = new Items.Armor.MajesticGloves(majesticSetTextures[1]);
                // this.Boots = new Items.Armor.MajesticBoots(majesticSetTextures[0]);
            }

            public void Draw(SpriteBatch sb)
            {
                new SystemFunctions.Sprite(this.PlayerAnim, this.DestinationPosition, this.SourcePosition).DrawBoxAnim(sb);
            }

            public string Name { get; private set; }
            public List<Texture2D> PlayerTextures { get; private set; }
            public Texture2D PlayerAnim { get; private set; }
            public List<Texture2D> MajesticSetTextures { get; private set; } // Boots, Gloves, Helmet, Robe, Shield
            public Texture2D NewbieStaffTex { get; private set; }
            public Texture2D MysticStaffTex { get; private set; }
            public float Elapsed { get; private set; }
            public int Frames { get; private set; }
            public Rectangle SourcePosition { get; private set; }
            public Rectangle DestinationPosition { get; private set; }

            // public Vector2  PlayerPosition { get; private set; } 
        }
    }
}
