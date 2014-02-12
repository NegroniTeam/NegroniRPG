namespace NegroniGame.Toolbar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class HP
    {
        public HP(List<Texture2D> hpTexture)
        {
            this.HpTexture = hpTexture; // negroniHPfull, negroniHP2of3, negroniHP1of3, negroniHPempty
            this.CurrentHpTexture = hpTexture[0];
            this.HpPosition = new Rectangle(385, Screens.GameScreen.ScreenHeight - 50, 50, 55);
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            
            MousePosition = new Point(mouseState.X, mouseState.Y);

            if (Player.Instance.HpPointsCurrent <= 0)
            {
                this.CurrentHpTexture = this.HpTexture[3];
            }
            else if (Player.Instance.HpPointsCurrent <= Player.HP_POINTS_INITIAL / 3)
            {
                this.CurrentHpTexture = this.HpTexture[2];
            }
            else if (Player.Instance.HpPointsCurrent <= (Player.HP_POINTS_INITIAL / 3) * 2)
            {
                this.CurrentHpTexture = this.HpTexture[1];
            }
            else if (Player.Instance.HpPointsCurrent == Player.HP_POINTS_INITIAL)
            {
                this.CurrentHpTexture = this.HpTexture[0];
            }

            // pop ups info box 
            if (this.HpPosition.Contains(MousePosition))
            {
                this.HpInfoRectangle = new Rectangle(MousePosition.X + 20, MousePosition.Y, 80, 30);
                this.HpInfoText = String.Format("HP\n{0} / {1}", Player.Instance.HpPointsCurrent, Player.HP_POINTS_INITIAL);
            }
            else
            {
                this.HpInfoRectangle = new Rectangle(0, 0, 0, 0);
                this.HpInfoText = "";
            }

        }

        public void Draw(SpriteBatch sb)
        {
            new SystemFunctions.Sprite(CurrentHpTexture, HpPosition).DrawBox(sb);

            new SystemFunctions.Sprite(Screens.GameScreen.Instance.infoBox1Texture, HpInfoRectangle).DrawBox(sb);
            new SystemFunctions.Sprite(Screens.GameScreen.Instance.FontMessages, this.HpInfoText, new Vector2(HpInfoRectangle.X + 20, HpInfoRectangle.Y)).DrawText(sb);
        }

        public List<Texture2D> HpTexture { get; private set; }
        public Texture2D CurrentHpTexture { get; private set; }
        public Rectangle HpPosition { get; private set; }
        public Point MousePosition { get; private set; }
        public Rectangle HpInfoRectangle { get; private set; }
        public string HpInfoText { get; private set; }
    }
}
