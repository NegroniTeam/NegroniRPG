namespace NegroniGame
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;
    using SystemFunctions;
    using Screens;

    public sealed class Scenery
    {
        // Singleton !
        private static Scenery instance;

        private Scenery()
        {
            this.ScreenRectangle = new Rectangle(0, 0, GameScreen.ScreenWidth, GameScreen.ScreenHeight);
            this.ToolbarRectangle = new Rectangle(0, GameScreen.ScreenHeight - 134, 700, 134);

            this.PlayerPicRectangle = new Rectangle(13, GameScreen.ScreenHeight - 72, 96, 72);
            //this.PlayerNameRectangle = new Rectangle(this.PlayerPicRectangle.X, this.PlayerPicRectangle.Y, 80, 30);

            this.DropList = new List<Drop>();
            this.IndexForDeletion = new List<int>();
        }

        public static Scenery Instance
        {
            get            {                if (instance == null)                {
                    instance = new Scenery();                }                return instance;
             }
        }

        public void Initialize(List<Texture2D> allSceneryTextures)
        {
            this.AllSceneryTextures = allSceneryTextures;
            Well.Instance.WellGraphic = AllSceneryTextures[2];
            Market.Instance.MarketGraphic = AllSceneryTextures[4];
        }

        public void AddDrop(Drop newDrop)
        {
            this.DropList.Add(newDrop);
        }

        public void UpdateScenery(GameTime gameTime, MouseState mouseState)
        {
            UpdateDrop(gameTime);
            
            MousePosition = new Point(mouseState.X, mouseState.Y);

            // upates info boxes
            if (this.PlayerPicRectangle.Contains(MousePosition))
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

        public void UpdateDrop(GameTime gameTime)
        {
            for (int index = 0; index < this.DropList.Count; index++)
            {
                bool isTimeUp = this.DropList[index].Update(gameTime);

                // deletes the drop if time is up
                if (isTimeUp)
                {
                    this.IndexForDeletion.Add(index);
                }
            }

            foreach (int index in IndexForDeletion)
            {
                DropList.RemoveAt(index);
            }

            this.IndexForDeletion = new List<int>();
        }


        public void Draw(SpriteBatch sb)
        {
            // backgroundTexture, toolbar, wellGraphic, playerPic, equipmentShopGraphic
            new Sprite(AllSceneryTextures[0], ScreenRectangle).DrawBox(sb);
            new Sprite(AllSceneryTextures[1], ToolbarRectangle).DrawBox(sb);
            Well.Instance.Draw(sb);
            Market.Instance.Draw(sb);
            new Sprite(AllSceneryTextures[3], PlayerPicRectangle).DrawBox(sb);

            // drop
            foreach (Drop drop in DropList)
            {
                drop.Draw(sb);
            }

            // info boxes
            new Sprite(Screens.GameScreen.Instance.infoBox1Texture, PlayerNameRectangle).DrawBox(sb);
            new Sprite(Screens.GameScreen.Instance.FontMessages, this.BoxNameText, new Vector2(PlayerNameRectangle.X + 20, PlayerNameRectangle.Y + 8)).DrawText(sb);

            new Sprite(Screens.GameScreen.Instance.infoBox1Texture, MarketInfoRectangle).DrawBox(sb);
            new Sprite(Screens.GameScreen.Instance.FontInfoBox, this.BoxMarketText, new Vector2(MarketInfoRectangle.X + 20, MarketInfoRectangle.Y + 2)).DrawText(sb);

            new Sprite(Screens.GameScreen.Instance.infoBox1Texture, WellInfoRectangle).DrawBox(sb);
            new Sprite(Screens.GameScreen.Instance.FontInfoBox, this.BoxWellText, new Vector2(WellInfoRectangle.X + 15, WellInfoRectangle.Y + 2)).DrawText(sb);

            new Sprite(Screens.GameScreen.Instance.infoBox1Texture, PlayerInfoRectangle).DrawBox(sb);
            new Sprite(Screens.GameScreen.Instance.FontMessages, this.PlayerInfoText, new Vector2(PlayerInfoRectangle.X + 25, PlayerInfoRectangle.Y + 1)).DrawText(sb);
            
        }


        public List<Texture2D> AllSceneryTextures { get; private set; }
        public Rectangle ScreenRectangle { get; private set; }
        public Rectangle ToolbarRectangle { get; private set; }
        public Rectangle PlayerPicRectangle { get; private set; }
        public Rectangle PlayerNameRectangle { get; private set; }
        public Rectangle MarketInfoRectangle { get; private set; }
        public Rectangle WellInfoRectangle { get; private set; }
        public Rectangle PlayerInfoRectangle { get; private set; }
        
        public Well Well { get; private set; }
        public Market Market { get; private set; }
        public List<Drop> DropList { get; private set; }
        public List<int> IndexForDeletion { get; private set; }
        public Point MousePosition { get; private set; }
        public string BoxNameText { get; private set; }
        public string BoxMarketText { get; private set; }
        public string BoxWellText { get; private set; }
        public string PlayerInfoText { get; private set; }
    }
}
