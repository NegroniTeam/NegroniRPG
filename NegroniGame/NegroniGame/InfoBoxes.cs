namespace NegroniGame
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using SystemFunctions;

    public sealed class InfoBoxes
    {
        // Singleton !
        private static InfoBoxes instance;

        private InfoBoxes()
        { }

        public static InfoBoxes Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InfoBoxes();
                }
                return instance;
            }
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            MousePosition = new Point(mouseState.X, mouseState.Y);

            // upates info boxes
            if (Scenery.Instance.PlayerPicRectangle.Contains(MousePosition))
            {
                this.PlayerNameRectangle = new Rectangle(MousePosition.X + 25, MousePosition.Y - 10, 100, 50);
                this.BoxNameText = Player.Instance.Name;
                this.BoxAtkDefText = String.Format("Atk. {0}\nDef. {1}", Player.Instance.WeaponDmg, Player.Instance.ArmorDef);
            }
            else
            {
                this.PlayerNameRectangle = new Rectangle(0, 0, 0, 0);
                this.BoxNameText = "";
                this.BoxAtkDefText = "";
            }

            if (Scenery.Instance.MarketPosition.Contains(MousePosition))
            {
                this.MarketInfoRectangle = new Rectangle(MousePosition.X - 40, MousePosition.Y + 20, 80, 30);
                this.BoxMarketText = "Market\nBuy Items";
            }
            else
            {
                this.MarketInfoRectangle = new Rectangle(0, 0, 0, 0);
                this.BoxMarketText = "";
            }

            if (Well.Instance.WellPosition.Contains(MousePosition))
            {
                this.WellInfoRectangle = new Rectangle(MousePosition.X + 20, MousePosition.Y + 10, 80, 30);
                this.BoxWellText = "Well\nRestores HP";
            }
            else
            {
                this.WellInfoRectangle = new Rectangle(0, 0, 0, 0);
                this.BoxWellText = "";
            }

            if (Player.Instance.DestinationPosition.Contains(MousePosition))
            {
                this.PlayerInfoRectangle = new Rectangle(MousePosition.X + 20, MousePosition.Y + 10, 80, 30);
                this.PlayerInfoText = "It's\nYou!";
            }
            else
            {
                this.PlayerInfoRectangle = new Rectangle(0, 0, 0, 0);
                this.PlayerInfoText = "";
            }
        }

        public void Draw()
        {
            new Sprite(Screens.GameScreen.Instance.InfoBox1Texture, PlayerNameRectangle).DrawBox();
            new Sprite(Screens.GameScreen.Instance.FontMessages, this.BoxNameText, new Vector2(PlayerNameRectangle.X + 20, PlayerNameRectangle.Y + 6)).DrawText();
            new Sprite(Screens.GameScreen.Instance.FontInfoBox, this.BoxAtkDefText, new Vector2(PlayerNameRectangle.X + 20, PlayerNameRectangle.Y + 21)).DrawText();

            new Sprite(Screens.GameScreen.Instance.InfoBox1Texture, MarketInfoRectangle).DrawBox();
            new Sprite(Screens.GameScreen.Instance.FontInfoBox, this.BoxMarketText, new Vector2(MarketInfoRectangle.X + 20, MarketInfoRectangle.Y + 2)).DrawText();

            new Sprite(Screens.GameScreen.Instance.InfoBox1Texture, WellInfoRectangle).DrawBox();
            new Sprite(Screens.GameScreen.Instance.FontInfoBox, this.BoxWellText, new Vector2(WellInfoRectangle.X + 15, WellInfoRectangle.Y + 2)).DrawText();

            new Sprite(Screens.GameScreen.Instance.InfoBox1Texture, PlayerInfoRectangle).DrawBox();
            new Sprite(Screens.GameScreen.Instance.FontMessages, this.PlayerInfoText, new Vector2(PlayerInfoRectangle.X + 25, PlayerInfoRectangle.Y + 1)).DrawText();

        }

        public Rectangle PlayerNameRectangle { get; private set; }
        public Rectangle MarketInfoRectangle { get; private set; }
        public Rectangle WellInfoRectangle { get; private set; }
        public Rectangle PlayerInfoRectangle { get; private set; }

        public Point MousePosition { get; private set; }
        public string BoxNameText { get; private set; }
        public string BoxMarketText { get; private set; }
        public string BoxWellText { get; private set; }
        public string PlayerInfoText { get; private set; }
        public string BoxAtkDefText { get; private set; }
    }
}
