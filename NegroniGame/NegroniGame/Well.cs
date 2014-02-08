namespace NegroniGame
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Well
    {
        public Well(Texture2D wellGraphic)
        {
            this.WellGraphic = wellGraphic;
            this.WellPosition = new Rectangle(300, Screens.GameScreen.ScreenHeight / 2 + 30, 48, 48);
        }

        public void Draw(SpriteBatch sb)
        {
            new SystemFunctions.Sprite(this.WellGraphic, this.WellPosition).DrawBox(sb);
        }

        public Texture2D WellGraphic { get; private set; }
        public Rectangle WellPosition { get; private set; }
    }
}
