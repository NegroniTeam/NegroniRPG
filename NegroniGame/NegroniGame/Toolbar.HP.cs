namespace NegroniGame.Toolbar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class HP
    {
        public HP(List<Texture2D> hpTexture)
        {
            this.HpTexture = hpTexture; // negroniHPfull, negroniHP2of3, negroniHP1of3, negroniHPempty
            this.CurrentHpTexture = hpTexture[0];
            this.HpPosition = new Rectangle(385, Screens.GameScreen.ScreenHeight - 50, 50, 55);
        }

        public void Update(GameTime gameTime)
        {
            if (Player.Instance.HpPointsCurrent <= 0)
            {
                this.CurrentHpTexture = this.HpTexture[3];
            }
            else if (Player.Instance.HpPointsCurrent <= Player.Instance.HpPointsInitial / 3)
            {
                this.CurrentHpTexture = this.HpTexture[2];
            }
            else if (Player.Instance.HpPointsCurrent <= (Player.Instance.HpPointsInitial / 3) * 2)
            {
                this.CurrentHpTexture = this.HpTexture[1];
            }
            else if (Player.Instance.HpPointsCurrent == Player.Instance.HpPointsInitial)
            {
                this.CurrentHpTexture = this.HpTexture[0];
            }
        }

        public void Draw(SpriteBatch sb)
        {
            new SystemFunctions.Sprite(CurrentHpTexture, HpPosition).DrawBox(sb);
        }

        public List<Texture2D> HpTexture { get; private set; }
        public Texture2D CurrentHpTexture { get; private set; }
        public Rectangle HpPosition { get; private set; }
    }
}
