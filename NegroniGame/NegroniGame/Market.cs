//namespace NegroniGame
//{
//    using System;
//    using Microsoft.Xna.Framework;
//    using Microsoft.Xna.Framework.Graphics;
//    using Microsoft.Xna.Framework.Input;

//    public class Market
//    {
//        // Singleton !
//        private static Market instance;

//        private Market()
//        {
//            this.MarketGraphic = Screens.GameScreen.Instance.AllSceneryTextures[4];

//            this.MarketPosition = new Rectangle(Screens.GameScreen.ScreenWidth - 115, 5, 102, 54);

//            this.DialogWindowWidth = 250;
//            this.DialogWindowHeight = 370;

//            this.DialogWindowTexture = Screens.GameScreen.Instance.MarketDialog;

//            this.DialogWindowPosition = new Rectangle((Screens.GameScreen.ScreenWidth - DialogWindowWidth) / 2, 10, DialogWindowWidth, DialogWindowHeight);
//            this.BootsPicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 50, 50, 50);
//            this.GlovesPicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 100, 50, 50);
//            this.HelmetPicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 150, 50, 50);
//            this.RobePicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 200, 50, 50);
//            this.ShieldPicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 250, 50, 50);
//            this.StaffPicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 300, 50, 50);

//            int buyButtonSize = 50;
//            this.BuyButton1Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 15, BootsPicPosition.Top, buyButtonSize, buyButtonSize);
//            this.BuyButton2Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 15, GlovesPicPosition.Top, buyButtonSize, buyButtonSize);
//            this.BuyButton3Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 15, HelmetPicPosition.Top, buyButtonSize, buyButtonSize);
//            this.BuyButton4Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 15, RobePicPosition.Top, buyButtonSize, buyButtonSize);
//            this.BuyButton5Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 15, ShieldPicPosition.Top, buyButtonSize, buyButtonSize);
//            this.BuyButton6Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 15, StaffPicPosition.Top, buyButtonSize, buyButtonSize);

//            this.BootsText = "Majestic Boots" + "\n" + "Def.10 / 50 Coins";
//            this.GlovesText = "Majestic Gloves" + "\n" + "Def.10 / 50 Coins";
//            this.HelmetText = "Majestic Helmet" + "\n" + "Def.10 / 50 Coins";
//            this.RobeText = "Majestic Robe" + "\n" + "Def.25 / 50 Coins";
//            this.ShieldText = "Majestic Shield" + "\n" + "Def.15 / 50 Coins";
//            this.StaffText = "Mystic Staff" + "\n" + "Atk.50 / 50 Coins";
//        }

//        public static Market Instance
//        {
//            get
//            {
//                if (instance == null)
//                {
//                    instance = new Market();
//                }
//                return instance;
//            }
//        }

//        public void Update()
//        {
//            if (Player.Instance.DestinationPosition.Intersects(new Rectangle(this.MarketPosition.X - 5, this.MarketPosition.Y - 5, this.MarketPosition.Width + 10, this.MarketPosition.Height + 10))
//                && Screens.GameScreen.Instance.KeyboardState.IsKeyDown(Keys.Enter)
//                && Screens.GameScreen.Instance.KeyboardStatePrevious.IsKeyUp(Keys.Enter))
//            {
//                Market.Instance.MarketDialog();
//                Screens.GameScreen.Instance.IsPaused = true;
//            }
//        }


//        public void Draw()
//        {
//            new SystemFunctions.Sprite(this.MarketGraphic, this.MarketPosition).DrawBox();

//            if (IsInMarket)
//            {
//                new SystemFunctions.Sprite(this.DialogWindowTexture, this.DialogWindowPosition).DrawBox();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.MajesticSetTextures[0], this.BootsPicPosition).DrawBox();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.MajesticSetTextures[1], this.GlovesPicPosition).DrawBox();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.MajesticSetTextures[2], this.HelmetPicPosition).DrawBox();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.MajesticSetTextures[3], this.RobePicPosition).DrawBox();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.MajesticSetTextures[4], this.ShieldPicPosition).DrawBox();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.MysticStaffTexture, this.StaffPicPosition).DrawBox();

//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.FontMessages, this.BootsText, new Vector2(DialogWindowPosition.Left + 65, BootsPicPosition.Y + 10)).DrawText();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.FontMessages, this.GlovesText, new Vector2(DialogWindowPosition.Left + 65, GlovesPicPosition.Y + 10)).DrawText();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.FontMessages, this.HelmetText, new Vector2(DialogWindowPosition.Left + 65, HelmetPicPosition.Y + 10)).DrawText();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.FontMessages, this.RobeText, new Vector2(DialogWindowPosition.Left + 65, RobePicPosition.Y + 10)).DrawText();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.FontMessages, this.ShieldText, new Vector2(DialogWindowPosition.Left + 65, ShieldPicPosition.Y + 10)).DrawText();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.FontMessages, this.StaffText, new Vector2(DialogWindowPosition.Left + 65, StaffPicPosition.Y + 10)).DrawText();


//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.BuyButton, this.BuyButton1Position).DrawBox();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.BuyButton, this.BuyButton2Position).DrawBox();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.BuyButton, this.BuyButton3Position).DrawBox();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.BuyButton, this.BuyButton4Position).DrawBox();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.BuyButton, this.BuyButton5Position).DrawBox();
//                new SystemFunctions.Sprite(Screens.GameScreen.Instance.BuyButton, this.BuyButton6Position).DrawBox();
//            }
//        }

//        public bool IsInMarket { get; private set; }
//        public Texture2D MarketGraphic { get; private set; }
//        public Texture2D DialogWindowTexture { get; private set; }
//        public Rectangle MarketPosition { get; private set; }
//        public int DialogWindowWidth { get; private set; }
//        public int DialogWindowHeight { get; private set; }
//        public Rectangle DialogWindowPosition { get; private set; }
//        public Rectangle RobePicPosition { get; private set; }
//        public Rectangle GlovesPicPosition { get; private set; }
//        public Rectangle HelmetPicPosition { get; private set; }
//        public Rectangle BootsPicPosition { get; private set; }
//        public Rectangle ShieldPicPosition { get; private set; }
//        public Rectangle StaffPicPosition { get; private set; }
//        public Rectangle BuyButton1Position { get; private set; }
//        public Rectangle BuyButton2Position { get; private set; }
//        public Rectangle BuyButton3Position { get; private set; }
//        public Rectangle BuyButton4Position { get; private set; }
//        public Rectangle BuyButton5Position { get; private set; }
//        public Rectangle BuyButton6Position { get; private set; }
//        public string BootsText { get; private set; }
//        public string GlovesText { get; private set; }
//        public string HelmetText { get; private set; }
//        public string RobeText { get; private set; }
//        public string ShieldText { get; private set; }
//        public string StaffText { get; private set; }

//    }
//}
