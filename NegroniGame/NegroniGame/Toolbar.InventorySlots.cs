namespace NegroniGame.Toolbar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;

    public class InventorySlots : Interfaces.IInventorySlot
    {
        public InventorySlots(Texture2D infoBoxTexture, SpriteFont font)
        {
            this.Font = font;
            this.TextPosition = new Vector2 (0, 0);

            InventoryAreaTopPoint = Screens.GameScreen.ScreenHeight - 110;
            InventoryAreaLeftPoint = 475;
            InventorySlotWidth = InventorySlotHeight = 51;

            this.InfoBoxTexture = infoBoxTexture;
            this.InventoryArea = new Rectangle(InventoryAreaLeftPoint, InventoryAreaTopPoint, 206, 100); // the whole inventory

            // first row of slots
            this.Slot1CoinsArea = new Rectangle(InventoryAreaLeftPoint, InventoryAreaTopPoint,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot2ElixirsArea = new Rectangle(InventoryAreaLeftPoint + InventorySlotWidth, InventoryAreaTopPoint,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot3WeaponArea = new Rectangle(InventoryAreaLeftPoint + (InventorySlotWidth * 2), InventoryAreaTopPoint,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot4ShieldArea = new Rectangle(InventoryAreaLeftPoint + (InventorySlotWidth * 3), InventoryAreaTopPoint,
                                                    InventorySlotWidth, InventorySlotHeight);
            // second row of slots
            this.Slot5HelmetArea = new Rectangle(InventoryAreaLeftPoint, InventoryAreaTopPoint + InventorySlotHeight,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot6RobeArea = new Rectangle(InventoryAreaLeftPoint + InventorySlotWidth, InventoryAreaTopPoint + InventorySlotHeight,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot7GlovesArea = new Rectangle(InventoryAreaLeftPoint + (InventorySlotWidth * 2), InventoryAreaTopPoint + InventorySlotHeight,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot8BootsArea = new Rectangle(InventoryAreaLeftPoint + (InventorySlotWidth * 3), InventoryAreaTopPoint + InventorySlotHeight,
                                                    InventorySlotWidth, InventorySlotHeight);
        }

        public void Update(GameTime gameTime, Point mousePosition)
        {
            // Show small pop-up descriptive text box when mouse is over an item in the inventory
            if (this.Slot1CoinsArea.Contains(mousePosition))
            {
                this.InventoryPopUpInfoBox = new Rectangle(mousePosition.X + 20, mousePosition.Y + 20, 100, 40);

                this.InventoryPopUpInfoBoxText = "Coins" + "\n" + "100"; // Player.Coins.Amount
                this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                this.TextPosition = new Vector2(mousePosition.X + 20 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), mousePosition.Y + 25);
            }
            else if (this.Slot2ElixirsArea.Contains(mousePosition))
            {
                this.InventoryPopUpInfoBox = new Rectangle(mousePosition.X + 20, mousePosition.Y + 20, 100, 40);

                this.InventoryPopUpInfoBoxText = "2 HP Elixir" + "\n" + "Restores 50 hp"; // Player.Elixirs.Count ; Items.ElixirHP.Info
                this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                this.TextPosition = new Vector2(mousePosition.X + 25 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), mousePosition.Y + 25);
            }
            else if (this.Slot3WeaponArea.Contains(mousePosition))
            {
                this.InventoryPopUpInfoBox = new Rectangle(mousePosition.X - 80, mousePosition.Y + 20, 100, 40);

                this.InventoryPopUpInfoBoxText = "Mystic Staff" + "\n" + "Atk. 100"; // Player.Weapon.Info
                this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                this.TextPosition = new Vector2(mousePosition.X - 80 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), mousePosition.Y + 25);
            }
            else if (this.Slot4ShieldArea.Contains(mousePosition))
            {
                this.InventoryPopUpInfoBox = new Rectangle(mousePosition.X - 80, mousePosition.Y + 20, 100, 40);

                this.InventoryPopUpInfoBoxText = "Dragon Shield" + "\n" + "Def. 20"; // Player.Shield.Info
                this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                this.TextPosition = new Vector2(mousePosition.X - 80 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), mousePosition.Y + 25);
            }
            else if (this.Slot5HelmetArea.Contains(mousePosition))
            {
                this.InventoryPopUpInfoBox = new Rectangle(mousePosition.X + 20, mousePosition.Y - 20, 100, 40);

                this.InventoryPopUpInfoBoxText = "Dragon Helmet" + "\n" + "Def. 10"; // Player.Helmet.Info
                this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                this.TextPosition = new Vector2(mousePosition.X + 20 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), mousePosition.Y - 15);
            }
            else if (this.Slot6RobeArea.Contains(mousePosition))
            {
                this.InventoryPopUpInfoBox = new Rectangle(mousePosition.X + 20, mousePosition.Y - 20, 100, 40);

                this.InventoryPopUpInfoBoxText = "Dragon Robe" + "\n" + "Def. 30"; // Player.Robe.Info
                this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                this.TextPosition = new Vector2(mousePosition.X + 20 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), mousePosition.Y - 15);
            }
            else if (this.Slot7GlovesArea.Contains(mousePosition))
            {
                this.InventoryPopUpInfoBox = new Rectangle(mousePosition.X - 80, mousePosition.Y - 20, 100, 40);

                this.InventoryPopUpInfoBoxText = "Dragon Gloves" + "\n" + "Def. 10"; // Player.Gloves.Info
                this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                this.TextPosition = new Vector2(mousePosition.X - 80 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), mousePosition.Y - 15);
            }
            else if (this.Slot8BootsArea.Contains(mousePosition))
            {
                this.InventoryPopUpInfoBox = new Rectangle(mousePosition.X - 80, mousePosition.Y - 20, 100, 40);

                this.InventoryPopUpInfoBoxText = "Dragon Boots" + "\n" + "Def. 10"; // Player.Boots.Info
                this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                this.TextPosition = new Vector2(mousePosition.X - 80 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), mousePosition.Y - 15);
            }
            else
            {
                this.InventoryPopUpInfoBox = new Rectangle(0, 0, 0, 0);
                this.TextPosition = new Vector2(0, 0);
                this.InventoryPopUpInfoBoxText = "";
            }

        }

        public void Draw(SpriteBatch sb)
        {
            new SystemFunctions.Sprite(this.InfoBoxTexture, this.InventoryPopUpInfoBox).DrawBox(sb);
            new SystemFunctions.Sprite(this.Font, this.InventoryPopUpInfoBoxText, this.TextPosition).DrawText(sb); 
        }

        private int InventorySlotWidth { get; set; }
        private int InventorySlotHeight { get; set; }
        private int InventoryAreaTopPoint { get; set; }
        private int InventoryAreaLeftPoint { get; set; }
        private int StringLength { get; set; }
        public Rectangle InventoryArea { get; private set; }
        public Rectangle Slot1CoinsArea { get; private set; }
        public Rectangle Slot2ElixirsArea { get; private set; }
        public Rectangle Slot3WeaponArea { get; private set; }
        public Rectangle Slot4ShieldArea { get; private set; }
        public Rectangle Slot5HelmetArea { get; private set; }
        public Rectangle Slot6RobeArea { get; private set; }
        public Rectangle Slot7GlovesArea { get; private set; }
        public Rectangle Slot8BootsArea { get; private set; }
        public Vector2 TextPosition { get; private set; }
        public Rectangle InventoryPopUpInfoBox { get; private set; }
        public string InventoryPopUpInfoBoxText { get; private set; }
        public Texture2D InfoBoxTexture { get; private set; }
        public SpriteFont Font { get; private set; }
        
    }
}
