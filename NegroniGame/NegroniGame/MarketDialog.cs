namespace NegroniGame
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public sealed class MarketDialog
    {
        // Singleton !
        private static MarketDialog instance;

        private static int buyButtonSize = 50;

        private static int DialogWindowWidth = 250;
        private static int DialogWindowHeight = 370;

        private static Texture2D DialogWindowTexture = Screens.GameScreen.Instance.MarketDialog;

        private static Rectangle DialogWindowPosition = new Rectangle((Screens.GameScreen.ScreenWidth - DialogWindowWidth) / 2, 10, DialogWindowWidth, DialogWindowHeight);
        private static Rectangle item1PicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 60, 50, 50);
        private static Rectangle item2PicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 100, 50, 50);
        private static Rectangle item3PicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 140, 50, 50);
        private static Rectangle item4PicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 180, 50, 50);
        private static Rectangle item5PicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 220, 50, 50);
        private static Rectangle item6PicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 260, 50, 50);
        private static Rectangle item7PicPosition = new Rectangle(DialogWindowPosition.Left + 10, DialogWindowPosition.Top + 300, 50, 50);

        private static readonly Texture2D buyButtontexture = Screens.GameScreen.Instance.BuyButton;
        private Rectangle BuyButton1Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 10, item1PicPosition.Top, buyButtonSize, buyButtonSize);
        private Rectangle BuyButton2Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 10, item2PicPosition.Top, buyButtonSize, buyButtonSize);
        private Rectangle BuyButton3Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 10, item3PicPosition.Top, buyButtonSize, buyButtonSize);
        private Rectangle BuyButton4Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 10, item4PicPosition.Top, buyButtonSize, buyButtonSize);
        private Rectangle BuyButton5Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 10, item5PicPosition.Top, buyButtonSize, buyButtonSize);
        private Rectangle BuyButton6Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 10, item6PicPosition.Top, buyButtonSize, buyButtonSize);
        private Rectangle BuyButton7Position = new Rectangle(DialogWindowPosition.Right - buyButtonSize - 10, item7PicPosition.Top, buyButtonSize, buyButtonSize);

        private readonly Items.ElixirsHP item1 = new Items.ElixirsHP(1);
        private readonly Items.Armor.MajesticBoots item2 = new Items.Armor.MajesticBoots();
        private readonly Items.Armor.MajesticGloves item3 = new Items.Armor.MajesticGloves();
        private readonly Items.Armor.MajesticHelmet item4 = new Items.Armor.MajesticHelmet();
        private readonly Items.Armor.MajesticRobe item5 = new Items.Armor.MajesticRobe();
        private readonly Items.Armor.MajesticShield item6 = new Items.Armor.MajesticShield();
        private readonly Items.Weapon.MysticStaff item7 = new Items.Weapon.MysticStaff();

        private string ElixirText, BootsText, GlovesText, HelmetText, RobeText, ShieldText, StaffText;
        private static readonly SpriteFont font = Screens.GameScreen.Instance.FontMessages;

        private MarketDialog()
        {
            this.ElixirText = String.Format("{0}" + "\n" + "HP {1} / {2} Coins", this.item1.Name, Items.ElixirsHP.RECOVERY_AMOUNT, this.item1.BuyingPrice);
            this.BootsText = String.Format("{0}" + "\n" + "Def.{1} / {2} Coins", this.item2.Name, this.item2.Defence, this.item2.BuyingPrice);
            this.GlovesText = String.Format("{0}" + "\n" + "Def.{1} / {2} Coins", this.item3.Name, this.item3.Defence, this.item3.BuyingPrice);
            this.HelmetText = String.Format("{0}" + "\n" + "Def.{1} / {2} Coins", this.item4.Name, this.item4.Defence, this.item4.BuyingPrice);
            this.RobeText = String.Format("{0}" + "\n" + "Def.{1} / {2} Coins", this.item5.Name, this.item5.Defence, this.item5.BuyingPrice);
            this.ShieldText = String.Format("{0}" + "\n" + "Atk.{1} / {2} Coins", this.item6.Name, this.item6.Defence, this.item6.BuyingPrice);
            this.StaffText = String.Format("{0}" + "\n" + "Atk.{1} / {2} Coins", this.item7.Name, this.item7.Attack, this.item7.BuyingPrice);
        }

        public static MarketDialog Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MarketDialog();
                }
                return instance;
            }
        }

        public void CheckIfSomethingBought(MouseState mouseState, MouseState mouseStatePrevious)
        {
            Point mousePosition = new Point(mouseState.X, mouseState.Y);

            if (this.BuyButton1Position.Contains(mousePosition)
                && mouseState.LeftButton == ButtonState.Pressed
                && mouseStatePrevious.LeftButton == ButtonState.Released)
            {
                int newNumberElixirs = Player.Instance.Elixirs.Count + 1;

                Player.Instance.BuyItem(new Items.ElixirsHP(newNumberElixirs), this.item1.BuyingPrice);
                MarketDialogHandler.Instance.IsInMarket = false;
                Screens.GameScreen.Instance.IsPaused = false;
            }
            else if (this.BuyButton2Position.Contains(mousePosition)
                && mouseState.LeftButton == ButtonState.Pressed
                && mouseStatePrevious.LeftButton == ButtonState.Released)
            {
                Player.Instance.BuyItem(this.item2, this.item2.BuyingPrice);
                MarketDialogHandler.Instance.IsInMarket = false;
                Screens.GameScreen.Instance.IsPaused = false;
            }
            else if (this.BuyButton3Position.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Player.Instance.BuyItem(this.item3, this.item3.BuyingPrice);
                MarketDialogHandler.Instance.IsInMarket = false;
                Screens.GameScreen.Instance.IsPaused = false;
            }
            else if (this.BuyButton4Position.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Player.Instance.BuyItem(this.item4, this.item4.BuyingPrice);
                MarketDialogHandler.Instance.IsInMarket = false;
                Screens.GameScreen.Instance.IsPaused = false;
            }
            else if (this.BuyButton5Position.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Player.Instance.BuyItem(this.item5, this.item5.BuyingPrice);
                MarketDialogHandler.Instance.IsInMarket = false;
                Screens.GameScreen.Instance.IsPaused = false;
            }
            else if (this.BuyButton6Position.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Player.Instance.BuyItem(this.item6, this.item6.BuyingPrice);
                MarketDialogHandler.Instance.IsInMarket = false;
                Screens.GameScreen.Instance.IsPaused = false;
            }
            else if (this.BuyButton7Position.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Player.Instance.BuyItem(this.item7, this.item7.BuyingPrice);
                MarketDialogHandler.Instance.IsInMarket = false;
                Screens.GameScreen.Instance.IsPaused = false;
            }

        }

        public void Draw()
        {
            new SystemFunctions.Sprite(DialogWindowTexture, DialogWindowPosition).DrawBox();
            new SystemFunctions.Sprite(this.item1.Texture, item1PicPosition).DrawBox();
            new SystemFunctions.Sprite(this.item2.Texture, item2PicPosition).DrawBox();
            new SystemFunctions.Sprite(this.item3.Texture, item3PicPosition).DrawBox();
            new SystemFunctions.Sprite(this.item4.Texture, item4PicPosition).DrawBox();
            new SystemFunctions.Sprite(this.item5.Texture, item5PicPosition).DrawBox();
            new SystemFunctions.Sprite(this.item6.Texture, item6PicPosition).DrawBox();
            new SystemFunctions.Sprite(this.item7.Texture, item7PicPosition).DrawBox();

            new SystemFunctions.Sprite(font, this.ElixirText, new Vector2(DialogWindowPosition.Left + 60, item1PicPosition.Y + 10)).DrawText();
            new SystemFunctions.Sprite(font, this.BootsText, new Vector2(DialogWindowPosition.Left + 60, item2PicPosition.Y + 10)).DrawText();
            new SystemFunctions.Sprite(font, this.GlovesText, new Vector2(DialogWindowPosition.Left + 60, item3PicPosition.Y + 10)).DrawText();
            new SystemFunctions.Sprite(font, this.HelmetText, new Vector2(DialogWindowPosition.Left + 60, item4PicPosition.Y + 10)).DrawText();
            new SystemFunctions.Sprite(font, this.RobeText, new Vector2(DialogWindowPosition.Left + 60, item5PicPosition.Y + 10)).DrawText();
            new SystemFunctions.Sprite(font, this.ShieldText, new Vector2(DialogWindowPosition.Left + 60, item6PicPosition.Y + 10)).DrawText();
            new SystemFunctions.Sprite(font, this.StaffText, new Vector2(DialogWindowPosition.Left + 60, item7PicPosition.Y + 10)).DrawText();


            new SystemFunctions.Sprite(buyButtontexture, this.BuyButton1Position).DrawBox();
            new SystemFunctions.Sprite(buyButtontexture, this.BuyButton2Position).DrawBox();
            new SystemFunctions.Sprite(buyButtontexture, this.BuyButton3Position).DrawBox();
            new SystemFunctions.Sprite(buyButtontexture, this.BuyButton4Position).DrawBox();
            new SystemFunctions.Sprite(buyButtontexture, this.BuyButton5Position).DrawBox();
            new SystemFunctions.Sprite(buyButtontexture, this.BuyButton6Position).DrawBox();
            new SystemFunctions.Sprite(buyButtontexture, this.BuyButton7Position).DrawBox();
        }
    }
}
