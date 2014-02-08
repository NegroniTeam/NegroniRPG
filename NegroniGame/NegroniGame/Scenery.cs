﻿namespace NegroniGame
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using SystemFunctions;
    using Screens;

    public class Scenery
    {
        public Scenery(List<Texture2D> allSceneryTextures)
        {
            this.AllSceneryTextures = allSceneryTextures;
            Well = new Well(AllSceneryTextures[2]);

            this.ScreenRectangle = new Rectangle(0, 0, GameScreen.ScreenWidth, GameScreen.ScreenHeight);
            this.ToolbarRectangle = new Rectangle(0, GameScreen.ScreenHeight - 134, 700, 134);
            this.PlayerPicRectangle = new Rectangle(13, GameScreen.ScreenHeight - 72, 96, 72);
            this.EquipmentShopRect = new Rectangle(GameScreen.ScreenWidth - 115, 5, 102, 54);
        }

        public void Draw(SpriteBatch sb)
        {
            // backgroundTexture, toolbar, wellGraphic, playerPic, equipmentShopGraphic
            new Sprite(AllSceneryTextures[0], ScreenRectangle).DrawBox(sb);
            new Sprite(AllSceneryTextures[1], ToolbarRectangle).DrawBox(sb);
            Well.Draw(sb);
            new Sprite(AllSceneryTextures[3], PlayerPicRectangle).DrawBox(sb);
            new Sprite(AllSceneryTextures[4], EquipmentShopRect).DrawBox(sb);
        }

        public List<Texture2D> AllSceneryTextures { get; private set; }
        public Rectangle ScreenRectangle { get; private set; }
        public Rectangle ToolbarRectangle { get; private set; }
        // public Rectangle WellRectangle { get; private set; }
        public Rectangle PlayerPicRectangle { get; private set; }
        public Rectangle EquipmentShopRect { get; private set; }
        public Well Well { get; private set; }

    }
}
