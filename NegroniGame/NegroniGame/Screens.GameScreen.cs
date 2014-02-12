namespace NegroniGame.Screens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;
    using NegroniGame.SystemFunctions;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public sealed class GameScreen : Microsoft.Xna.Framework.Game
    {
        // Singleton !
        private static GameScreen instance;

        private GameScreen()
        {
            // Windows settings
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 700;
            graphics.ApplyChanges(); //Changes the settings that you just applied

            Content.RootDirectory = "Content";
        }

        public static GameScreen Instance
        {
            get            {                if (instance == null)                {
                    instance = new GameScreen();                }                return instance;
             }
        }

        #region Declarations

        // SINGLETON starts
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GraphicsDevice device;
        // SINGLETON ends

        private Toolbar.SystemMsg AllMessages;
        private Toolbar.InventorySlots InventorySlots;
        private Toolbar.HP HpBar;

        private MouseState mouseState;

        private KeyboardState ks;
        private Vector2 cursorPos = new Vector2();

        #endregion

        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;

            ScreenWidth = (int)graphics.PreferredBackBufferWidth;
            ScreenHeight = (int)graphics.PreferredBackBufferHeight;

            SystemFunctions.Sound.PlayIngameMusic();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            FontMessages = Content.Load<SpriteFont>("Segoe UI Mono");

            FontInfoBox = Content.Load<SpriteFont>("Segoe UI Mono Smaller");

            cursorTex = Content.Load<Texture2D>("media/cursor1"); // cursor

            // background, toolbar, well, playerPic, equipmentShop
            allSceneryTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/background"),
                Content.Load<Texture2D>("media/toolbar"),
                Content.Load<Texture2D>("media/well"),
                Content.Load<Texture2D>("media/Elvina"),
                Content.Load<Texture2D>("media/market")
            };

            Scenery.Instance.Initialize(allSceneryTextures);

            playerTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/Elvina-right"),
                Content.Load<Texture2D>("media/sprites/Elvina-left"),
                Content.Load<Texture2D>("media/sprites/Elvina-up"),
                Content.Load<Texture2D>("media/sprites/Elvina-down"),
            };
            
            majesticSetTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/drop/majesticBoots"),
                Content.Load<Texture2D>("media/drop/majesticGloves"),
                Content.Load<Texture2D>("media/drop/majesticHelmet"),
                Content.Load<Texture2D>("media/drop/majesticRobe"),
                Content.Load<Texture2D>("media/drop/majesticShield")
            };

            coinsTex = Content.Load<Texture2D>("media/drop/coins");
            elixirsTex = Content.Load<Texture2D>("media/drop/elixirs");
            newbieStaffTex = Content.Load<Texture2D>("media/drop/newbieStaff");
            mysticStaffTex = Content.Load<Texture2D>("media/drop/mysticStaff");

            Player.Instance.Initialize(playerTextures, majesticSetTextures, coinsTex, elixirsTex, newbieStaffTex, mysticStaffTex);

            monster1Textures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/monster1-right"),
                Content.Load<Texture2D>("media/sprites/monster1-left"),
                Content.Load<Texture2D>("media/sprites/monster1-up"),
                Content.Load<Texture2D>("media/sprites/monster1-down"),
            };

            monster2Textures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/monster2-right"),
                Content.Load<Texture2D>("media/sprites/monster2-left"),
                Content.Load<Texture2D>("media/sprites/monster2-up"),
                Content.Load<Texture2D>("media/sprites/monster2-down"),
            };

            monster3Textures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/monster3-right"),
                Content.Load<Texture2D>("media/sprites/monster3-left"),
                Content.Load<Texture2D>("media/sprites/monster3-up"),
                Content.Load<Texture2D>("media/sprites/monster3-down"),
            };

            monster4Textures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/monster4-right"),
                Content.Load<Texture2D>("media/sprites/monster4-left"),
                Content.Load<Texture2D>("media/sprites/monster4-up"),
                Content.Load<Texture2D>("media/sprites/monster4-down"),
            };

            monstersTextures = new List<List<Texture2D>>()
            {
                monster1Textures,
                monster2Textures,
                monster3Textures,
                monster4Textures
            };

            //Monster = new Monsters.Monster(monster1Textures);

            Monsters.MonsterGroup.Instance.MonsterTextures = monstersTextures;

            // Monsters.MonsterGroup.Instance.

            infoBoxTexture = Content.Load<Texture2D>("media/infoBox");

            slotsTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/slots/defaultSlot1"),
                Content.Load<Texture2D>("media/slots/defaultSlot2"),
                Content.Load<Texture2D>("media/slots/defaultSlot3"),
                Content.Load<Texture2D>("media/slots/defaultSlot4"),
                Content.Load<Texture2D>("media/slots/defaultSlot5"),
                Content.Load<Texture2D>("media/slots/defaultSlot6"),
                Content.Load<Texture2D>("media/slots/defaultSlot7"),
                Content.Load<Texture2D>("media/slots/defaultSlot8"),
            };

            InventorySlots = new Toolbar.InventorySlots(infoBoxTexture, FontInfoBox, slotsTextures);

            negroniHPList = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/negroniHPfull"),
                Content.Load<Texture2D>("media/negroniHP2of3"),
                Content.Load<Texture2D>("media/negroniHP1of3"),
                Content.Load<Texture2D>("media/negroniHPempty")
            };

            HpBar = new Toolbar.HP(negroniHPList);

            // fireballs = Content.Load<Texture2D>("media/sprites/fireballs");

            shotsTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/fireballs")
            };

            DropTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/drop/coins2")
            };
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            // this.Exit();

            cursorPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y); // cursor update
            mouseState = Mouse.GetState();

            ks = Keyboard.GetState();

            Player.Instance.Update(gameTime, ks);

            // Monster.Move(gameTime);

            Monsters.MonsterGroup.Instance.SpawnGenerator(gameTime);
            Monsters.MonsterGroup.Instance.Update(gameTime);
            Scenery.Instance.UpdateDrop(gameTime);

            InventorySlots.Update(gameTime, mouseState);
            Toolbar.SystemMsg.Instance.GetLastMessages();
            HpBar.Update(gameTime);

            Player.Instance.UpdateInventory(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            Scenery.Instance.Draw(spriteBatch);

            Monsters.MonsterGroup.Instance.Draw(spriteBatch);
            Player.Instance.Draw(spriteBatch);

            InventorySlots.Draw(spriteBatch);
            Toolbar.SystemMsg.Instance.DrawText(spriteBatch);
            HpBar.Draw(spriteBatch);

            spriteBatch.Draw(cursorTex, cursorPos, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public SpriteFont FontMessages { get; private set; }
        public SpriteFont FontInfoBox { get; private set; }
        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }

        public List<List<Texture2D>> monstersTextures { get; private set; }
        public List<Texture2D> monster1Textures { get; private set; }
        public List<Texture2D> monster2Textures { get; private set; }
        public List<Texture2D> monster3Textures { get; private set; }
        public List<Texture2D> monster4Textures { get; private set; }
        public List<Texture2D> allSceneryTextures { get; private set; }
        public List<Texture2D> playerTextures { get; private set; }
        public List<Texture2D> slotsTextures { get; private set; }
        public List<Texture2D> majesticSetTextures { get; private set; }
        public List<Texture2D> negroniHPList { get; private set; }
        public List<Texture2D> shotsTextures { get; private set; }
        public List<Texture2D> DropTextures { get; private set; }
        public Texture2D newbieStaffTex { get; private set; }
        public Texture2D mysticStaffTex { get; private set; }
        public Texture2D coinsTex { get; private set; }
        public Texture2D elixirsTex { get; private set; }
        public Texture2D cursorTex { get; private set; }
        public Texture2D infoBoxTexture { get; private set; }
        public Texture2D fireballs { get; private set; }
    }
}
