namespace NegroniGame
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using SystemFunctions;
    using Screens;

    public class Scenery
    {
        public Scenery()
        {
            this.ScreenRectangle = new Rectangle(0, 0, GameScreen.ScreenWidth, GameScreen.ScreenHeight);
            this.ToolbarRectangle = new Rectangle(0, GameScreen.ScreenHeight - 134, 700, 134);
            this.WellRectangle = new Rectangle(300, GameScreen.ScreenHeight / 2 + 30, 48, 48);
            this.PlayerPicRectangle = new Rectangle(13, GameScreen.ScreenHeight - 72, 96, 72);
            this.EquipmentShopRect = new Rectangle(GameScreen.ScreenWidth - 115, 5, 102, 54);
        }

        public void Draw(List<Texture2D> allSceneryTextures, SpriteBatch sb)
        {
            // backgroundTexture, toolbar, wellGraphic, playerPic, equipmentShopGraphic
            new Sprite(allSceneryTextures[0], ScreenRectangle).DrawBox(sb);
            new Sprite(allSceneryTextures[1], ToolbarRectangle).DrawBox(sb);
            new Sprite(allSceneryTextures[2], WellRectangle).DrawBox(sb);
            new Sprite(allSceneryTextures[3], PlayerPicRectangle).DrawBox(sb);
            new Sprite(allSceneryTextures[4], EquipmentShopRect).DrawBox(sb);
        }

        public Rectangle ScreenRectangle { get; private set; }
        public Rectangle ToolbarRectangle { get; private set; }
        public Rectangle WellRectangle { get; private set; }
        public Rectangle PlayerPicRectangle { get; private set; }
        public Rectangle EquipmentShopRect { get; private set; }
    }
}
