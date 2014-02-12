namespace NegroniGame
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;
    using SystemFunctions;
    using Screens;

    public sealed class InfoBoxes
    {
        // Singleton !
        private static InfoBoxes instance;

        private InfoBoxes()
        { }

        public static InfoBoxes Instance
        {
            get            {                if (instance == null)                {
                    instance = new InfoBoxes();                }                return instance;
             }
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            MousePosition = new Point(mouseState.X, mouseState.Y);

            // upates info boxes
            if (Scenery.Instance.PlayerPicRectangle.Contains(MousePosition))
            {
                this.PlayerNameRectangle = new Rectangle(MousePosition.X + 10, MousePosition.Y + 20, 80, 30);
                this.BoxNameText = Player.Instance.Name;
            }
            else
            {
                this.PlayerNameRectangle = new Rectangle(0, 0, 0, 0);
                this.BoxNameText = "";
            }

            if (Market.Instance.MarketPosition.Contains(MousePosition))
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

        public void Draw(SpriteBatch sb)
        {
            new Sprite(Screens.GameScreen.Instance.infoBox1Texture, PlayerNameRectangle).DrawBox(sb);
            new Sprite(Screens.GameScreen.Instance.FontMessages, this.BoxNameText, new Vector2(PlayerNameRectangle.X + 20, PlayerNameRectangle.Y + 8)).DrawText(sb);

            new Sprite(Screens.GameScreen.Instance.infoBox1Texture, MarketInfoRectangle).DrawBox(sb);
            new Sprite(Screens.GameScreen.Instance.FontInfoBox, this.BoxMarketText, new Vector2(MarketInfoRectangle.X + 20, MarketInfoRectangle.Y + 2)).DrawText(sb);

            new Sprite(Screens.GameScreen.Instance.infoBox1Texture, WellInfoRectangle).DrawBox(sb);
            new Sprite(Screens.GameScreen.Instance.FontInfoBox, this.BoxWellText, new Vector2(WellInfoRectangle.X + 15, WellInfoRectangle.Y + 2)).DrawText(sb);

            new Sprite(Screens.GameScreen.Instance.infoBox1Texture, PlayerInfoRectangle).DrawBox(sb);
            new Sprite(Screens.GameScreen.Instance.FontMessages, this.PlayerInfoText, new Vector2(PlayerInfoRectangle.X + 25, PlayerInfoRectangle.Y + 1)).DrawText(sb);
            
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
    }
}
