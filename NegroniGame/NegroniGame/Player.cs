namespace NegroniGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;

    public sealed class Player : Interfaces.IPlayer
    {
        // Singleton !
        private static Player instance;

        private Player() {}

        public static Player Instance
        {
            get            {                if (instance == null)                {
                    instance = new Player();                }                return instance;
             }
        }

        private const float PLAYER_SPEED = 2f;
        private const float PLAYER_ANIM_SPEED = 200f;
        private const float SHOT_REUSE_TIME = 1200f;

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
            this.HpPointsInitial = 50;
            this.HpPointsCurrent = 32;

            this.Direction = SystemFunctions.DirectionsEnum.South;
            this.Shots = new List<Shots>();

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
        public Vector2 ShotPosition; // <- Read the last property info in comment

        public void Update(GameTime gameTime, KeyboardState ks)
        {
            Move(gameTime, ks);
            UpdateInventory();

            this.ElapsedTimeShot += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Adds new shot to the list
            if (ks.IsKeyDown(Keys.Space))
            {
                if (this.ElapsedTimeShot >= SHOT_REUSE_TIME)
                {
                    Shots.Add(new Shots(this.PlayerPosition, this.Direction));
                    this.ElapsedTimeShot = 0;
                }
            }

            // Updates shots
            foreach (var Shot in Shots)
            {
                Shot.Update(gameTime);
            }

        }

        // playerTextures - right, left, up, down
        public void Move(GameTime gameTime, KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Right))
            {
                if (PlayerPosition.X < Screens.GameScreen.ScreenWidth - 30)
                {
                    this.PlayerPosition.X += PLAYER_SPEED;
                    this.PlayerAnim = this.PlayerTextures[0];
                    this.SourcePosition = AnimatePlayer(gameTime);
                    this.Direction = SystemFunctions.DirectionsEnum.East;
                }
            }
            else if (ks.IsKeyDown(Keys.Left))
            {
                if (PlayerPosition.X > 0)
                {
                    this.PlayerPosition.X -= PLAYER_SPEED;
                    this.PlayerAnim = this.PlayerTextures[1];
                    this.SourcePosition = AnimatePlayer(gameTime);
                    this.Direction = SystemFunctions.DirectionsEnum.West;
                }
            }
            else if (ks.IsKeyDown(Keys.Up))
            {
                if (PlayerPosition.Y > 0)
                {
                    this.PlayerPosition.Y -= PLAYER_SPEED;
                    this.PlayerAnim = this.PlayerTextures[2];
                    this.SourcePosition = AnimatePlayer(gameTime);
                    this.Direction = SystemFunctions.DirectionsEnum.North;
                }
            }
            else if (ks.IsKeyDown(Keys.Down))
            {
                if (PlayerPosition.Y <= Screens.GameScreen.ScreenHeight - 170)
                {
                    this.PlayerPosition.Y += PLAYER_SPEED;
                    this.PlayerAnim = this.PlayerTextures[3];
                    this.SourcePosition = AnimatePlayer(gameTime);
                    this.Direction = SystemFunctions.DirectionsEnum.South;
                }
            }
            else
            {
                this.SourcePosition = new Rectangle(64, 0, 32, 32);
            }

            this.DestinationPosition = new Rectangle((int)this.PlayerPosition.X, (int)this.PlayerPosition.Y, 32, 32);
        }

        private Rectangle AnimatePlayer(GameTime gameTime)
        {
            this.ElapsedTimePlayer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.ElapsedTimePlayer >= PLAYER_ANIM_SPEED)
            {
                if (this.Frames >= 2)
                {
                    this.Frames = 0;
                }
                else
                {
                    this.Frames++;
                }
                this.ElapsedTimePlayer = 0;
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
            // this.Coins = new Items.Coins(100, this.CoinsTex);
            // this.Elixirs = new Items.ElixirHP(2, this.ElixirsTex);
            // this.Weapon = new Items.Weapon.NewbieStaff(this.NewbieStaffTex);
            // this.Shield = new Items.Armor.MajesticShield(this.MajesticSetTextures[4]);
            // this.Helmet = new Items.Armor.MajesticHelmet(this.MajesticSetTextures[2]);
            // this.Robe = new Items.Armor.MajesticRobe(this.MajesticSetTextures[3]);
            // this.Gloves = new Items.Armor.MajesticGloves(this.MajesticSetTextures[1]);
            // this.Boots = new Items.Armor.MajesticBoots(this.MajesticSetTextures[0]);
        }

        
        public void Draw(SpriteBatch sb)
        {
            new SystemFunctions.Sprite(this.PlayerAnim, this.DestinationPosition, this.SourcePosition).DrawBoxAnim(sb);

            foreach (var Shot in Shots)
            {
                Shot.Draw(sb);
            }
        }

        public string Name { get; private set; }
        public List<Texture2D> PlayerTextures { get; private set; }
        public Texture2D PlayerAnim { get; private set; }
        public List<Texture2D> MajesticSetTextures { get; private set; } // Boots, Gloves, Helmet, Robe, Shield
        public Texture2D NewbieStaffTex { get; private set; }
        public Texture2D MysticStaffTex { get; private set; }
        public float ElapsedTimePlayer { get; private set; }
        public float ElapsedTimeShot { get; private set; }
        public int Frames { get; private set; }
        public Rectangle SourcePosition { get; private set; }
        public Rectangle DestinationPosition { get; private set; }
        public int HpPointsCurrent { get; private set; }
        public int HpPointsInitial { get; private set; }
        public SystemFunctions.DirectionsEnum Direction { get; private set; }
        public List<Shots> Shots { get; private set; }

        // public Vector2 ShotStartingPosition { get; private set; }
        // public Texture2D ShotAnim { get; private set; }
        // public Vector2 ShotPosition { get; private set; } <- cannot set X and Y if property
        // public Vector2  PlayerPosition { get; set; }  <- cannot set X and Y if property

        public Items.Coins Coins { get; set; }
        public Items.ElixirHP Elixirs { get; set; }
        public Items.Weapon.Weapon Weapon { get; set; }
        public Items.Armor.Armor Shield { get; set; }
        public Items.Armor.Armor Helmet { get; set; }
        public Items.Armor.Armor Robe { get; set; }
        public Items.Armor.Armor Gloves { get; set; }
        public Items.Armor.Armor Boots { get; set; }
    }
}
