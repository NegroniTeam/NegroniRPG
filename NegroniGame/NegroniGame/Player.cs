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

        private Player() { }

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }

        private const float PLAYER_SPEED = 2f;
        private const float PLAYER_ANIM_SPEED = 200f;
        private const float SHOT_REUSE_TIME = 1200f;
        public const int HP_POINTS_INITIAL = 50;

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
            this.HpPointsCurrent = 32;

            this.Direction = SystemFunctions.DirectionsEnum.South;
            this.Shots = new List<Shots>();
            this.IndexesForDeletionShots = new List<int>();

            // Boots, Gloves, Helmet, Robe, Shield
            this.Coins = new Items.Coins(100);
            this.Elixirs = new Items.ElixirHP(2, elixirsTex);
            this.Weapon = new Items.Weapon.NewbieStaff(newbieStaffTex);
            this.Shield = new Items.Armor.Armor();
            this.Helmet = new Items.Armor.Armor();
            this.Robe = new Items.Armor.Armor();
            this.Gloves = new Items.Armor.Armor();
            this.Boots = new Items.Armor.Armor();

            this.WeaponDmg = this.Weapon.Attack;
        }

        public Vector2 PlayerPosition; // <- Read the last property info in comment
        public Vector2 ShotPosition; // <- Read the last property info in comment

        public void Update(GameTime gameTime, KeyboardState ks)
        {
            Move(gameTime, ks);
            UpdateInventory(gameTime);
            UpdateShots(gameTime, ks);

            if (this.Weapon.ToString() == "NegroniGame.Items.Weapon.Weapon")
            {
                this.WeaponDmg = 5;
            }
            else
            {
                this.WeaponDmg = this.Weapon.Attack;
            }
        }

        // playerTextures - right, left, up, down
        public void Move(GameTime gameTime, KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Right))
            {
                if (PlayerPosition.X < Screens.GameScreen.ScreenWidth - 30)
                {
                    // checks if the player is going over the well. If so, doesn't move
                    if (!Well.Instance.WellPosition.Intersects(new Rectangle((int)(this.PlayerPosition.X + this.DestinationPosition.Width + PLAYER_SPEED), (int)(this.PlayerPosition.Y + 28), 4, 4))
                        && !Market.Instance.MarketPosition.Intersects(new Rectangle((int)(this.PlayerPosition.X + this.DestinationPosition.Width + PLAYER_SPEED), (int)(this.PlayerPosition.Y + 28), 4, 4))
                        && !IntersectsWithMobs(new Rectangle((int)(this.PlayerPosition.X + PLAYER_SPEED), (int)(this.PlayerPosition.Y), 32, 32)))
                    {
                        this.PlayerPosition.X += PLAYER_SPEED;
                    }
                    this.PlayerAnim = this.PlayerTextures[0];
                    this.SourcePosition = AnimatePlayer(gameTime);
                    this.Direction = SystemFunctions.DirectionsEnum.East;
                }
            }
            else if (ks.IsKeyDown(Keys.Left))
            {
                if (PlayerPosition.X > 0)
                {
                    if (!Well.Instance.WellPosition.Intersects(new Rectangle((int)(this.PlayerPosition.X - PLAYER_SPEED), (int)(this.PlayerPosition.Y + 28), 4, 4))
                       && !Market.Instance.MarketPosition.Intersects(new Rectangle((int)(this.PlayerPosition.X - PLAYER_SPEED), (int)(this.PlayerPosition.Y + 28), 4, 4))
                       && !IntersectsWithMobs(new Rectangle((int)(this.PlayerPosition.X - PLAYER_SPEED), (int)(this.PlayerPosition.Y), 32, 32)))
                    {
                        this.PlayerPosition.X -= PLAYER_SPEED;
                    }
                    this.PlayerAnim = this.PlayerTextures[1];
                    this.SourcePosition = AnimatePlayer(gameTime);
                    this.Direction = SystemFunctions.DirectionsEnum.West;
                }
            }
            else if (ks.IsKeyDown(Keys.Up))
            {
                if (PlayerPosition.Y > 0)
                {
                    if (!Well.Instance.WellPosition.Intersects(new Rectangle((int)(this.PlayerPosition.X), (int)(this.PlayerPosition.Y - PLAYER_SPEED), 32, 32))
                       && !((Market.Instance.MarketPosition.Y + 12 >= this.PlayerPosition.Y) && Market.Instance.MarketPosition.Intersects(this.DestinationPosition))
                       && !((this.PlayerPosition.X + this.DestinationPosition.Width >= Market.Instance.MarketPosition.X && this.PlayerPosition.X <= Market.Instance.MarketPosition.X + 14)
                                && Market.Instance.MarketPosition.Intersects(this.DestinationPosition))
                       && !((this.PlayerPosition.X + this.DestinationPosition.Width >= Market.Instance.MarketPosition.X + Market.Instance.MarketPosition.Width - 10
                                && Market.Instance.MarketPosition.Intersects(this.DestinationPosition)))
                       && !IntersectsWithMobs(new Rectangle((int)(this.PlayerPosition.X), (int)(this.PlayerPosition.Y - PLAYER_SPEED), 32, 32)))
                    {
                        this.PlayerPosition.Y -= PLAYER_SPEED;
                    }
                    this.PlayerAnim = this.PlayerTextures[2];
                    this.SourcePosition = AnimatePlayer(gameTime);
                    this.Direction = SystemFunctions.DirectionsEnum.North;
                }
            }
            else if (ks.IsKeyDown(Keys.Down))
            {
                if (PlayerPosition.Y <= Screens.GameScreen.ScreenHeight - 170)
                {
                    if (!Well.Instance.WellPosition.Intersects(new Rectangle((int)(this.PlayerPosition.X), (int)(this.PlayerPosition.Y + PLAYER_SPEED), 32, 32))
                       && !IntersectsWithMobs(new Rectangle((int)(this.PlayerPosition.X), (int)(this.PlayerPosition.Y + PLAYER_SPEED), 32, 32)))
                    {
                        this.PlayerPosition.Y += PLAYER_SPEED;
                    }
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

        private bool IntersectsWithMobs(Rectangle newPosition)
        {
            foreach (Monsters.Monster monster in Monsters.MonsterGroup.Instance.SpawnedMobs)
            {
                if (monster.DestinationPosition.Intersects(newPosition))
                {
                    return true;
                }
            }

            return false;
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

        public void UpdateInventory(GameTime gameTime)
        {
            this.ElapsedTimeLastMsgElixir += (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.IndexesForDeletionDrop = new List<int>();

            // picks up drop
            for (int index = 0; index < Scenery.Instance.DropList.Count; index++)
            {
                if (this.DestinationPosition.Intersects(Scenery.Instance.DropList[index].DropPosition))
                {
                    if (Scenery.Instance.DropList[index].Name == "Coins")
                    {
                        this.Coins = new Items.Coins(this.Coins.Amount + Scenery.Instance.DropList[index].Amount);

                        Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You picked up {0} {1}.", Scenery.Instance.DropList[index].Amount, Scenery.Instance.DropList[index].Name), Color.Beige } });

                        this.IndexesForDeletionDrop.Add(index);
                    }
                }
            }

            // deletes picked up drop
            foreach (int index in IndexesForDeletionDrop)
            {
                Scenery.Instance.DropList.RemoveAt(index);
            }

            // updates elixir reuse time
            this.Elixirs.Update(gameTime);

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

        public void UpdateShots(GameTime gameTime, KeyboardState ks)
        {
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
            for (int index = 0; index < Shots.Count; index++)
            {
                bool isOutOfRange = Shots[index].Update(gameTime);

                if (isOutOfRange == true)
                {
                    this.IndexesForDeletionShots.Add(index);
                }
            }

            foreach (int index in IndexesForDeletionShots)
            {
                Shots.RemoveAt(index);
            }

            IndexesForDeletionShots = new List<int>();
        }

        public void UseElixir(GameTime gameTime)
        {

            if (this.Elixirs.ElapsedTimeElixir >= Items.ElixirHP.REUSE_TIME)
            {
                // TO DO: pop up box ask if sure before drinking the elixir
                int restoredPoints = ((Player.HP_POINTS_INITIAL - Player.Instance.HpPointsCurrent) >= Items.ElixirHP.RECOVERY_AMOUNT) ? Items.ElixirHP.RECOVERY_AMOUNT : Player.HP_POINTS_INITIAL - Player.Instance.HpPointsCurrent;

                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You restored {0} HP.", restoredPoints), Color.Aquamarine } });
                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> 1 HP Elixir destroyed."), Color.Red } });

                Player.Instance.HpPointsCurrent += restoredPoints;
                this.Elixirs.DestroyElixir();
                this.ElapsedTimeLastMsgElixir = 0;
            }
            else
            {
                if (this.ElapsedTimeLastMsgElixir >= 1)
                {
                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() {
                        { String.Format(">> {0} seconds to reuse Elixir.", (int)Items.ElixirHP.REUSE_TIME - (int)this.Elixirs.ElapsedTimeElixir), Color.Aquamarine } });
                    this.ElapsedTimeLastMsgElixir = 0;
                }
            }
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
        public float ElapsedTimeLastMsgElixir { get; private set; }
        public int Frames { get; private set; }
        public Rectangle SourcePosition { get; private set; }
        public Rectangle DestinationPosition { get; private set; }
        public int HpPointsCurrent { get; private set; }
        public SystemFunctions.DirectionsEnum Direction { get; private set; }
        public List<Shots> Shots { get; private set; }
        public List<int> IndexesForDeletionShots { get; private set; }
        public List<int> IndexesForDeletionDrop { get; private set; }

        // public Vector2 ShotStartingPosition { get; private set; }
        // public Texture2D ShotAnim { get; private set; }
        // public Vector2 ShotPosition { get; private set; } <- cannot set X and Y if property
        // public Vector2  PlayerPosition { get; set; }  <- cannot set X and Y if property

        public Items.Coins Coins { get; set; }
        public Items.ElixirHP Elixirs { get; set; }
        public Items.Weapon.Weapon Weapon { get; set; }
        public int WeaponDmg { get; set; }
        public Items.Armor.Armor Shield { get; set; }
        public Items.Armor.Armor Helmet { get; set; }
        public Items.Armor.Armor Robe { get; set; }
        public Items.Armor.Armor Gloves { get; set; }
        public Items.Armor.Armor Boots { get; set; }
    }
}
