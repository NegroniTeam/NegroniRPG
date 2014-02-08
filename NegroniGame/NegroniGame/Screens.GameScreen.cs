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
        // SINGLETON starts
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GraphicsDevice device;
        // SINGLETON ends

        private Toolbar.SystemMsg AllMessages;
        private Scenery MainScenery;
        private Player Player;
        private Monsters.Monsters Monster;

        private MouseState mouseState;
        private Point mousePosition = new Point();
        private Rectangle inventoryArea = new Rectangle();

        private List<Texture2D> allSceneryTextures;
        private List<Texture2D> playerTextures;
        private List<Texture2D> monsterTextures;

        private Texture2D cursorTex;
        private Texture2D infoBoxTexture;

        private Rectangle inventoryPopUpInfoBox;

        private KeyboardState ks;
        private Vector2 cursorPos = new Vector2();

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

            Font = Content.Load<SpriteFont>("Segoe UI Mono");
            AllMessages = new Toolbar.SystemMsg(Font);
            MainScenery = new Scenery();

            inventoryArea = new Rectangle(475, ScreenHeight - 110, 206, 100);

            base.Initialize();
        }

        protected override void LoadContent()
        {
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

            playerTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/Elvina-right"),
                Content.Load<Texture2D>("media/sprites/Elvina-left"),
                Content.Load<Texture2D>("media/sprites/Elvina-up"),
                Content.Load<Texture2D>("media/sprites/Elvina-down"),
            };

            Player = new Player(playerTextures);

            monsterTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("media/sprites/monster1-right"),
                Content.Load<Texture2D>("media/sprites/monster1-left"),
                Content.Load<Texture2D>("media/sprites/monster1-up"),
                Content.Load<Texture2D>("media/sprites/monster1-down"),
            };

            Monster = new Monsters.Monsters(monsterTextures);

            infoBoxTexture = Content.Load<Texture2D>("media/infoBox");
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            // this.Exit();

            cursorPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y); // cursor update
            mouseState = Mouse.GetState();
            mousePosition = new Point(mouseState.X, mouseState.Y);

            ks = Keyboard.GetState();

            Player.Move(gameTime, ks);
            Monster.Move(gameTime);

            // Show small pop-up descriptive text box when mouse is over an item in the inventory
            if (inventoryArea.Contains(mousePosition))
            {
                inventoryPopUpInfoBox = new Rectangle(550, ScreenHeight - 80, 100, 40);
            }
            else
            {
                inventoryPopUpInfoBox = new Rectangle(0, 0, 0, 0);
            }

            AllMessages.Update();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            MainScenery.Draw(allSceneryTextures, spriteBatch);

            spriteBatch.Draw(Monster.MonsterAnim, Monster.DestinationPosition, Monster.SourcePosition, Color.White);
            spriteBatch.Draw(Player.PlayerAnim, Player.DestinationPosition, Player.SourcePosition, Color.White);
            spriteBatch.Draw(infoBoxTexture, inventoryPopUpInfoBox, Color.White);

            AllMessages.DrawText(spriteBatch);

            spriteBatch.Draw(cursorTex, cursorPos, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public SpriteFont Font { get; private set; }
        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }
    }
}
