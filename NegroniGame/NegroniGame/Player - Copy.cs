//namespace NegroniGame
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using Microsoft.Xna.Framework;
//    using Microsoft.Xna.Framework.Input;
//    using Microsoft.Xna.Framework.Graphics;

//    public sealed class Player : Interfaces.IPlayer
//    {
//        // Singleton !
//        private static Player instance;

//        public const int HP_POINTS_INITIAL = 200;
//        private const float PLAYER_SPEED = 2f;
//        private const float PLAYER_ANIM_SPEED = 200f;

//        private Vector2 playerPosition = new Vector2((float)Screens.GameScreen.ScreenWidth / 2, (float)Screens.GameScreen.ScreenHeight / 2 - 50);
        
//        private readonly List<Texture2D> playerTextures = Screens.GameScreen.Instance.PlayerTextures;
//        private Texture2D playerAnim = Screens.GameScreen.Instance.PlayerTextures[3];

//        private Rectangle animSourcePosition;

//        private float elapsedTimePlayerAnim;

//        private int frames = 0;

//        private Player() { }

//        public static Player Instance
//        {
//            get
//            {
//                if (instance == null)
//                {
//                    instance = new Player();
//                }
//                return instance;
//            }
//        }

//        public string Name { get; private set; }
//        public Rectangle DestinationPosition { get; private set; }
//        public Vector2 CenterOfPlayer { get; private set; }
//        public SystemFunctions.DirectionsEnum Direction { get; private set; }
//        public int HpPointsCurrent { get; private set; }
//        public int WeaponDmg { get; private set; }
//        public int ArmorDef { get; private set; }
//        public Items.Coins Coins { get; set; } //  TO DO PRIVATE
//        public Items.ElixirsHP Elixirs { get; private set; }
//        public Interfaces.IWeapon Weapon { get; private set; }
//        public Interfaces.IShield Shield { get; private set; }
//        public Interfaces.IHelmet Helmet { get; private set; }
//        public Interfaces.IRobe Robe { get; private set; }
//        public Interfaces.IGloves Gloves { get; private set; }
//        public Interfaces.IBoots Boots { get; private set; }

//        public void Initialize()
//        {
//            this.Name = "Elvina";
//            this.HpPointsCurrent = HP_POINTS_INITIAL;
//            this.Direction = SystemFunctions.DirectionsEnum.South;

//            // 16 = half of texture width
//            this.CenterOfPlayer = new Vector2(this.playerPosition.X + 16, this.playerPosition.Y + 16);

//            this.Coins = new Items.Coins(100);
//            this.Elixirs = new Items.ElixirsHP(2);
//            this.Weapon = new Items.Weapon.NewbieStaff();

//            this.WeaponDmg = this.Weapon.Attack;
//            this.ArmorDef = 0;
//        }

//        public void Update(GameTime gameTime, KeyboardState ks)
//        {
//            if (this.HpPointsCurrent <= 0)
//            {
//                Screens.GameScreen.Instance.IsPaused = true;
//            }

//            Move(gameTime, ks);
//            this.CenterOfPlayer = new Vector2(this.playerPosition.X, this.playerPosition.Y);

//            UpdateItems(gameTime);

//            if (this.Weapon == null)
//            {
//                this.WeaponDmg = 5;
//            }
//            else
//            {
//                this.WeaponDmg = this.Weapon.Attack;
//            }

//            this.ArmorDef = 0;

//            if (this.Shield != null)
//            {
//                this.ArmorDef += this.Shield.Defence;
//            }
//            if (this.Helmet != null)
//            {
//                this.ArmorDef += this.Helmet.Defence;
//            }
//            if (this.Robe != null)
//            {
//                this.ArmorDef += this.Robe.Defence;
//            }
//            if (this.Gloves != null)
//            {
//                this.ArmorDef += this.Gloves.Defence;
//            }
//            if (this.Boots != null)
//            {
//                this.ArmorDef += this.Boots.Defence;
//            }

//        }

//        public void DestroyItem(string itemForDestruction)
//        {
//            switch (itemForDestruction)
//            {
//                case "weapon":
//                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} destroyed.", this.Weapon.Name), Color.Red } });
//                    this.Weapon = null;
//                    break;
//                case "shield":
//                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You destroyed {0}.", this.Shield.Name), Color.Red } });
//                    this.Shield = null;
//                    break;
//                case "gloves":
//                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You destroyed {0}.", this.Gloves.Name), Color.Red } });
//                    this.Gloves = null;
//                    break;
//                case "robe":
//                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You destroyed {0}.", this.Robe.Name), Color.Red } });
//                    this.Robe = null;
//                    break;
//                case "boots":
//                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You destroyed {0}.", this.Boots.Name), Color.Red } });
//                    this.Boots = null;
//                    break;
//                case "helmet":
//                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You destroyed {0}.", this.Helmet.Name), Color.Red } });
//                    this.Helmet = null;
//                    break;
//            }
//        }

//        public int RestoreFullHp()
//        {
//            int restoredPoints = HP_POINTS_INITIAL - this.HpPointsCurrent;

//            this.HpPointsCurrent = HP_POINTS_INITIAL;

//            return restoredPoints;
//        }

//        public void DrinkElixir()
//        {
//            this.HpPointsCurrent += ElixirsHandler.Instance.RestoredPoints;
//            this.Elixirs = new Items.ElixirsHP(this.Elixirs.Count - 1);
//        }

//        public void TakeDamage(int damage)
//        {
//            if (this.HpPointsCurrent > 0)
//            {
//                this.HpPointsCurrent -= damage;
//            }
//            if (this.HpPointsCurrent < 0)
//            {
//                this.HpPointsCurrent = 0;
//            }
//        }

//        public void Draw()
//        {
//            new SystemFunctions.Sprite(this.playerAnim, this.DestinationPosition, this.animSourcePosition).DrawBoxAnim();
//        }

//        // playerTextures - right, left, up, down
//        private void Move(GameTime gameTime, KeyboardState ks)
//        {
//            if (ks.IsKeyDown(Keys.Right))
//            {
//                if (playerPosition.X < Screens.GameScreen.ScreenWidth - 30)
//                {
//                    // checks if the player is going over the well. If so, doesn't move
//                    if (!Well.Instance.WellPosition.Intersects(new Rectangle((int)(this.playerPosition.X + this.DestinationPosition.Width + PLAYER_SPEED), (int)(this.playerPosition.Y + 28), 4, 4))
//                        && !Scenery.Instance.MarketPosition.Intersects(new Rectangle((int)(this.playerPosition.X + this.DestinationPosition.Width + PLAYER_SPEED), (int)(this.playerPosition.Y + 28), 4, 4))
//                        && !IntersectsWithMobs(new Rectangle((int)(this.playerPosition.X + PLAYER_SPEED), (int)(this.playerPosition.Y), 32, 32)))
//                    {
//                        this.playerPosition.X += PLAYER_SPEED;
//                    }
//                    this.playerAnim = this.playerTextures[0];
//                    this.animSourcePosition = AnimatePlayer(gameTime);
//                    this.Direction = SystemFunctions.DirectionsEnum.East;
//                }
//            }
//            else if (ks.IsKeyDown(Keys.Left))
//            {
//                if (playerPosition.X > 0)
//                {
//                    if (!Well.Instance.WellPosition.Intersects(new Rectangle((int)(this.playerPosition.X - PLAYER_SPEED), (int)(this.playerPosition.Y + 28), 4, 4))
//                       && !Scenery.Instance.MarketPosition.Intersects(new Rectangle((int)(this.playerPosition.X - PLAYER_SPEED), (int)(this.playerPosition.Y + 28), 4, 4))
//                       && !IntersectsWithMobs(new Rectangle((int)(this.playerPosition.X - PLAYER_SPEED), (int)(this.playerPosition.Y), 32, 32)))
//                    {
//                        this.playerPosition.X -= PLAYER_SPEED;
//                    }
//                    this.playerAnim = this.playerTextures[1];
//                    this.animSourcePosition = AnimatePlayer(gameTime);
//                    this.Direction = SystemFunctions.DirectionsEnum.West;
//                }
//            }
//            else if (ks.IsKeyDown(Keys.Up))
//            {
//                if (playerPosition.Y > 0)
//                {
//                    if (!Well.Instance.WellPosition.Intersects(new Rectangle((int)(this.playerPosition.X), (int)(this.playerPosition.Y - PLAYER_SPEED), 32, 32))
//                        && !Scenery.Instance.MarketPosition.Intersects(this.DestinationPosition)
//                       && !IntersectsWithMobs(new Rectangle((int)(this.playerPosition.X), (int)(this.playerPosition.Y - PLAYER_SPEED), 32, 32)))
//                    {
//                        this.playerPosition.Y -= PLAYER_SPEED;
//                    }
//                    this.playerAnim = this.playerTextures[2];
//                    this.animSourcePosition = AnimatePlayer(gameTime);
//                    this.Direction = SystemFunctions.DirectionsEnum.North;
//                }
//            }
//            else if (ks.IsKeyDown(Keys.Down))
//            {
//                if (playerPosition.Y <= Screens.GameScreen.ScreenHeight - 170)
//                {
//                    if (!Well.Instance.WellPosition.Intersects(new Rectangle((int)(this.playerPosition.X), (int)(this.playerPosition.Y + PLAYER_SPEED), 32, 32))
//                       && !IntersectsWithMobs(new Rectangle((int)(this.playerPosition.X), (int)(this.playerPosition.Y + PLAYER_SPEED), 32, 32)))
//                    {
//                        this.playerPosition.Y += PLAYER_SPEED;
//                    }
//                    this.playerAnim = this.playerTextures[3];
//                    this.animSourcePosition = AnimatePlayer(gameTime);
//                    this.Direction = SystemFunctions.DirectionsEnum.South;
//                }
//            }
//            else
//            {
//                this.animSourcePosition = new Rectangle(64, 0, 32, 32);
//            }

//            this.DestinationPosition = new Rectangle((int)this.playerPosition.X, (int)this.playerPosition.Y, 32, 32);
//        }

//        private bool IntersectsWithObjects(Rectangle newPosition)
//        {
//            bool doesIntersectWithMobs = false;

//            foreach (Monsters.Monster monster in Monsters.MonstersHandler.Instance.SpawnedMobs)
//            {
//                if (monster.DestinationPosition.Intersects(newPosition))
//                {
//                    doesIntersectWithMobs = true;
//                    break;
//                }
//            }
            
//            if (Well.Instance.WellPosition.Intersects(newPosition)
//                || Scenery.Instance.MarketPosition.Intersects(newPosition)
//                || doesIntersectWithMobs)
//            {
//                return true;
//            }

//            return false;
//        }

//        private Rectangle AnimatePlayer(GameTime gameTime)
//        {
//            this.elapsedTimePlayerAnim += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

//            if (this.elapsedTimePlayerAnim >= PLAYER_ANIM_SPEED)
//            {
//                if (this.frames >= 2)
//                {
//                    this.frames = 0;
//                }
//                else
//                {
//                    this.frames++;
//                }
//                this.elapsedTimePlayerAnim = 0;
//            }

//            // if on frame 0 - top up position 0
//            return new Rectangle(32 * this.frames, 0, 32, 32);
//        }

//        private void UpdateItems(GameTime gameTime)
//        {
//            // When picked up

//            // !!!!!!! first check if there is any item of the kind
//            // Toolbar.InventorySlots.CheckItems(); <- here is the code for checking

//            // !!!!!!! then add
//            //this.Coins = new Items.Coins(100);
//            //this.Elixirs = new Items.ElixirHP(2);
//            //this.Weapon = new Items.Weapon.MysticStaff();
//            //this.Shield = new Items.Armor.MajesticShield();
//            //this.Helmet = new Items.Armor.MajesticHelmet();
//            //this.Robe = new Items.Armor.MajesticRobe();
//            //this.Gloves = new Items.Armor.MajesticGloves();
//            //this.Boots = new Items.Armor.MajesticBoots();
//        }

//    }
//}
