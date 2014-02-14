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
            get
            {
                if (instance == null)
                {
                    instance = new Scenery();
                }
                return instance;
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

        public void Update(GameTime gameTime)
        {
            UpdateDrop(gameTime);
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
            // Market.Instance.Draw(sb);
            new Sprite(AllSceneryTextures[3], PlayerPicRectangle).DrawBox(sb);

            // drop
            foreach (Drop drop in DropList)
            {
                drop.Draw(sb);
            }

        }


        public List<Texture2D> AllSceneryTextures { get; private set; }
        public Rectangle ScreenRectangle { get; private set; }
        public Rectangle ToolbarRectangle { get; private set; }
        public Rectangle PlayerPicRectangle { get; private set; }

        public Well Well { get; private set; }
        public Market Market { get; private set; }
        public List<Drop> DropList { get; private set; }
        public List<int> IndexForDeletion { get; private set; }
        public Point MousePosition { get; private set; }
    }
}
