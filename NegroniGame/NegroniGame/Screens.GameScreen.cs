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
    public class GameScreen : Microsoft.Xna.Framework.Game
    {
        #region Declarations

        // SINGLETON starts
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GraphicsDevice device;
        // SINGLETON ends

        private Toolbar.SystemMsg AllMessages;
        private Scenery MainScenery;
        private Monsters.Monsters Monster;
        private Toolbar.InventorySlots InventorySlots;

        private MouseState mouseState;

        private List<Texture2D> allSceneryTextures;
        private List<Texture2D> playerTextures;
        private List<Texture2D> monsterTextures;
        private List<Texture2D> slotsTextures;
        private List<Texture2D> majesticSetTextures;
        private Texture2D newbieStaffTex, mysticStaffTex;
        private Texture2D coinsTex;
        private Texture2D elixirsTex;
        private Texture2D cursorTex;
        private Texture2D infoBoxTexture;

        private KeyboardState ks;
        private Vector2 cursorPos = new Vector2();

        #endregion

        public GameScreen()
        {
            // Windows settings
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 700;
            graphics.ApplyChanges(); //Changes the settings that you just applied

            Content.RootDirectory = "Content";
        }

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

            AllMessages = new Toolbar.SystemMsg(FontMessages);

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

            MainScenery = new Scenery(allSceneryTextures);

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

            monsterTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/monster1-right"),
                Content.Load<Texture2D>("media/sprites/monster1-left"),
                Content.Load<Texture2D>("media/sprites/monster1-up"),
                Content.Load<Texture2D>("media/sprites/monster1-down"),
            };

            Monster = new Monsters.Monsters(monsterTextures);

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

        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            // this.Exit();

            cursorPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y); // cursor update
            mouseState = Mouse.GetState();

            ks = Keyboard.GetState();

            Player.Instance.Move(gameTime, ks);
            Monster.Move(gameTime);
            InventorySlots.Update(gameTime, mouseState);

            Player.Instance.UpdateInventory();

            AllMessages.GetLastMessages();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            MainScenery.Draw(spriteBatch);
            Monster.Draw(spriteBatch);
            Player.Instance.Draw(spriteBatch);
            InventorySlots.Draw(spriteBatch);
            AllMessages.DrawText(spriteBatch);
            spriteBatch.Draw(cursorTex, cursorPos, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public SpriteFont FontMessages { get; private set; }
        public SpriteFont FontInfoBox { get; private set; }
        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }
    }
}
