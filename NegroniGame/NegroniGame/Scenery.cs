namespace NegroniGame
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
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

            this.DropList = new List<Drop>();
            this.IndexForDeletion = new List<int>();

            this.AllSceneryTextures = Screens.GameScreen.Instance.AllSceneryTextures;
            Well.Instance.WellGraphic = AllSceneryTextures[2];
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

        public void Initialize()
        {
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
                bool isTimeUp = false;

                if (this.DropList[index] != null)
                {
                    isTimeUp = this.DropList[index].Update(gameTime);
                }

                // deletes the drop if time is up
                if (isTimeUp)
                {
                    this.IndexForDeletion.Add(index);
                }
            }

            // picks up drop
            for (int index = 0; index < Scenery.Instance.DropList.Count; index++)
            {
                if (this.DropList[index] != null && Player.Instance.DestinationPosition.Intersects(Scenery.Instance.DropList[index].DropPosition))
                {
                    if (Scenery.Instance.DropList[index].Name == "Coins")
                    {
                        Player.Instance.Coins = new Items.Coins(Player.Instance.Coins.Amount + Scenery.Instance.DropList[index].Amount);

                        Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You picked up {0} {1}.", Scenery.Instance.DropList[index].Amount, Scenery.Instance.DropList[index].Name), Color.Beige } });

                        this.IndexForDeletion.Add(index);
                    }
                }
            }

            foreach (int index in IndexForDeletion)
            {
                this.DropList[index] = null;
                // DropList.RemoveAt(index);
            }

            this.IndexForDeletion = new List<int>();
        }


        public void Draw()
        {
            // backgroundTexture, toolbar, wellGraphic, playerPic, equipmentShopGraphic
            new Sprite(AllSceneryTextures[0], ScreenRectangle).DrawBox();
            new Sprite(AllSceneryTextures[1], ToolbarRectangle).DrawBox();
            Well.Instance.Draw();
            new Sprite(AllSceneryTextures[3], PlayerPicRectangle).DrawBox();

            // drop
            foreach (Drop drop in DropList)
            {
                if (drop != null)
                {
                    drop.Draw();
                }
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
