namespace NegroniGame.Toolbar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.GamerServices;

    public class InventorySlots : Interfaces.IInventorySlot
    {
        public InventorySlots(Texture2D infoBoxTexture, SpriteFont font, List<Texture2D> slotsTextures)
        {
            this.Font = font;
            this.TextPosition = new Vector2 (0, 0);

            this.DefaultSlotTextures = slotsTextures;

            InventoryAreaTopPoint = Screens.GameScreen.ScreenHeight - 105;
            InventoryAreaLeftPoint = 485;
            InventorySlotWidth = InventorySlotHeight = 50;

            this.InfoBoxTexture = infoBoxTexture;
            this.InventoryArea = new Rectangle(InventoryAreaLeftPoint, InventoryAreaTopPoint, 206, 100); // the whole inventory

            // 1 is added for borders
            // first row of slots
            this.Slot1CoinsArea = new Rectangle(InventoryAreaLeftPoint, InventoryAreaTopPoint,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot2ElixirsArea = new Rectangle(InventoryAreaLeftPoint + InventorySlotWidth + 1, InventoryAreaTopPoint,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot3WeaponArea = new Rectangle(InventoryAreaLeftPoint + ((InventorySlotWidth + 1) * 2), InventoryAreaTopPoint,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot4ShieldArea = new Rectangle(InventoryAreaLeftPoint + ((InventorySlotWidth + 1) * 3), InventoryAreaTopPoint,
                                                    InventorySlotWidth, InventorySlotHeight);
            // second row of slots
            this.Slot5HelmetArea = new Rectangle(InventoryAreaLeftPoint, InventoryAreaTopPoint + InventorySlotHeight + 1,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot6RobeArea = new Rectangle(InventoryAreaLeftPoint + InventorySlotWidth + 1, InventoryAreaTopPoint + InventorySlotHeight + 1,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot7GlovesArea = new Rectangle(InventoryAreaLeftPoint + ((InventorySlotWidth + 1) * 2), InventoryAreaTopPoint + InventorySlotHeight + 1,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot8BootsArea = new Rectangle(InventoryAreaLeftPoint + ((InventorySlotWidth + 1) * 3), InventoryAreaTopPoint + InventorySlotHeight + 1,
                                                    InventorySlotWidth, InventorySlotHeight);

        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            MousePosition = new Point(mouseState.X, mouseState.Y);
            CheckItems();

            // Console.WriteLine(Player.Instance.Weapon.ToString());

            // Show small pop-up descriptive text box when mouse is over an item in the inventory
            if (this.Slot1CoinsArea.Contains(MousePosition))
            {
                this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X + 20, MousePosition.Y + 20, 100, 40);

                this.InventoryPopUpInfoBoxText = String.Format("Coins" + "\n" + "{0}", Player.Instance.Coins.Amount);
                this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                this.TextPosition = new Vector2(MousePosition.X + 20 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y + 25);
            }

            else if (this.Slot2ElixirsArea.Contains(MousePosition) && Player.Instance.Elixirs.Count > 0)
            {
                this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X + 20, MousePosition.Y + 20, 100, 40);

                this.InventoryPopUpInfoBoxText = String.Format("{0} HP Elixir(s)" + "\n" + "Restores {1} HP", Player.Instance.Elixirs.Count, Player.Instance.Elixirs.RecoveryAmount);
                this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                this.TextPosition = new Vector2(MousePosition.X + 25 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y + 25);
            }

            else if (this.Slot3WeaponArea.Contains(MousePosition) && IsAnyWeapon)
            {
                Console.WriteLine("4");
                // Checks if right mouse button is clicked and destroys the item
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before destroys the item
                    Player.Instance.Weapon = new Items.Weapon.Weapon();
                }
                else
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X - 80, MousePosition.Y + 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0}" + "\n" + "Atk. {1}", Player.Instance.Weapon.Name, Player.Instance.Weapon.Attack);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X - 80 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y + 25);
                }
            }

            else if (this.Slot4ShieldArea.Contains(MousePosition) && IsAnyShield)
            {
                // Checks if right mouse button is clicked and destroys the item
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before destroys the item
                    Player.Instance.Shield = new Items.Armor.Armor();
                }
                else
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X - 80, MousePosition.Y + 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0}" + "\n" + "Def. {1}", Player.Instance.Shield.Name, Player.Instance.Shield.Defence);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X - 80 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y + 25);
                }
            }

            else if (this.Slot5HelmetArea.Contains(MousePosition) && IsAnyHelmet)
            {
                // Checks if right mouse button is clicked and destroys the item
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before destroys the item
                    Player.Instance.Helmet = new Items.Armor.Armor();
                }
                else
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X + 20, MousePosition.Y - 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0}" + "\n" + "Def. {1}", Player.Instance.Helmet.Name, Player.Instance.Helmet.Defence);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X + 20 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y - 15);
                }
            }

            else if (this.Slot6RobeArea.Contains(MousePosition) && IsAnyRobe)
            {
                // Checks if right mouse button is clicked and destroys the item
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before destroys the item
                    Player.Instance.Robe = new Items.Armor.Armor();
                }
                else
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X + 20, MousePosition.Y - 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0}" + "\n" + "Def. {1}", Player.Instance.Robe.Name, Player.Instance.Robe.Defence);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X + 20 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y - 15);
                }
            }

            else if (this.Slot7GlovesArea.Contains(MousePosition) && IsAnyGloves)
            {
                // Checks if right mouse button is clicked and destroys the item
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before destroys the item
                    Player.Instance.Gloves = new Items.Armor.Armor();
                }
                else
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X - 80, MousePosition.Y - 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0}" + "\n" + "Def. {1}", Player.Instance.Gloves.Name, Player.Instance.Gloves.Defence);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X - 80 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y - 15);
                }
            }

            else if (this.Slot8BootsArea.Contains(MousePosition) && IsAnyBoots)
            {
                // Checks if right mouse button is clicked and destroys the item
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before destroys the item
                    Player.Instance.Boots = new Items.Armor.Armor();
                }
                else
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X - 80, MousePosition.Y - 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0}" + "\n" + "Def. {1}", Player.Instance.Boots.Name, Player.Instance.Boots.Defence);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X - 80 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y - 15);
                }
            }

            else
            {
                this.InventoryPopUpInfoBox = new Rectangle(0, 0, 0, 0);
                this.TextPosition = new Vector2(0, 0);
                this.InventoryPopUpInfoBoxText = "";
            }

            CheckItems();

            // Show different picture on the slot depending on the object there
            if (Player.Instance.Coins.Amount > 0) { Slot1Image = Player.Instance.Coins.Texture; }
            else { Slot1Image = DefaultSlotTextures[0]; }

            if (Player.Instance.Elixirs.Count > 0) { Slot2Image = Player.Instance.Elixirs.Texture; }
            else { Slot2Image = DefaultSlotTextures[1]; }

            if (IsAnyWeapon) { Slot3Image = Player.Instance.Weapon.Texture; }
            else { Slot3Image = DefaultSlotTextures[2]; }

            if (IsAnyShield) { Slot4Image = Player.Instance.Shield.Texture; }
            else { Slot4Image = DefaultSlotTextures[3]; }

            if (IsAnyHelmet) { Slot5Image = Player.Instance.Helmet.Texture; }
            else { Slot5Image = DefaultSlotTextures[4]; }

            if (IsAnyRobe) { Slot6Image = Player.Instance.Robe.Texture; }
            else { Slot6Image = DefaultSlotTextures[5]; }

            if (IsAnyGloves) { Slot7Image = Player.Instance.Gloves.Texture; }
            else { Slot7Image = DefaultSlotTextures[6]; }

            if (IsAnyBoots) { Slot8Image = Player.Instance.Boots.Texture; }
            else { Slot8Image = DefaultSlotTextures[7]; }
        }

        public void Draw(SpriteBatch sb)
        {
            new SystemFunctions.Sprite(this.Slot1Image, this.Slot1CoinsArea).DrawBox(sb);
            new SystemFunctions.Sprite(this.Slot2Image, this.Slot2ElixirsArea).DrawBox(sb);
            new SystemFunctions.Sprite(this.Slot3Image, this.Slot3WeaponArea).DrawBox(sb);
            new SystemFunctions.Sprite(this.Slot4Image, this.Slot4ShieldArea).DrawBox(sb);
            new SystemFunctions.Sprite(this.Slot5Image, this.Slot5HelmetArea).DrawBox(sb);
            new SystemFunctions.Sprite(this.Slot6Image, this.Slot6RobeArea).DrawBox(sb);
            new SystemFunctions.Sprite(this.Slot7Image, this.Slot7GlovesArea).DrawBox(sb);
            new SystemFunctions.Sprite(this.Slot8Image, this.Slot8BootsArea).DrawBox(sb);
            new SystemFunctions.Sprite(this.InfoBoxTexture, this.InventoryPopUpInfoBox).DrawBox(sb);
            new SystemFunctions.Sprite(this.Font, this.InventoryPopUpInfoBoxText, this.TextPosition).DrawText(sb); 
        }

        public void CheckItems()
        {
            IsAnyWeapon = (Player.Instance.Weapon.ToString() != "NegroniGame.Items.Weapon.Weapon") ? true : false;
            IsAnyShield = (Player.Instance.Shield.ToString() != "NegroniGame.Items.Armor.Armor") ? true : false;
            IsAnyHelmet = (Player.Instance.Helmet.ToString() != "NegroniGame.Items.Armor.Armor") ? true : false;
            IsAnyRobe = (Player.Instance.Robe.ToString() != "NegroniGame.Items.Armor.Armor") ? true : false;
            IsAnyGloves = (Player.Instance.Gloves.ToString() != "NegroniGame.Items.Armor.Armor") ? true : false;
            IsAnyBoots = (Player.Instance.Boots.ToString() != "NegroniGame.Items.Armor.Armor") ? true : false;
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
        public List<Texture2D> DefaultSlotTextures { get; private set; }
        public Texture2D Slot1Image { get; private set; }
        public Texture2D Slot2Image { get; private set; }
        public Texture2D Slot3Image { get; private set; }
        public Texture2D Slot4Image { get; private set; }
        public Texture2D Slot5Image { get; private set; }
        public Texture2D Slot6Image { get; private set; }
        public Texture2D Slot7Image { get; private set; }
        public Texture2D Slot8Image { get; private set; }
        public Vector2 TextPosition { get; private set; }
        public Rectangle InventoryPopUpInfoBox { get; private set; }
        public string InventoryPopUpInfoBoxText { get; private set; }
        public Texture2D InfoBoxTexture { get; private set; }
        public SpriteFont Font { get; private set; }
        public Point MousePosition { get; private set; }
        public bool IsAnyWeapon { get; private set; }
        public bool IsAnyShield { get; private set; }
        public bool IsAnyHelmet { get; private set; }
        public bool IsAnyRobe { get; private set; }
        public bool IsAnyGloves { get; private set; }
        public bool IsAnyBoots { get; private set; }
       
    }
}
