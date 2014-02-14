namespace NegroniGame
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Well
    {
        // Singleton !
        private static Well instance;

        private Well()
        {
            this.WellPosition = new Rectangle(300, Screens.GameScreen.ScreenHeight / 2 + 30, 48, 48);
        }

        public static Well Instance
        {
            get            {                if (instance == null)                {
                    instance = new Well();                }                return instance;
             }
        }

        public void Draw(SpriteBatch sb)
        {
            new SystemFunctions.Sprite(this.WellGraphic, this.WellPosition).DrawBox(sb);
        }

        public Texture2D WellGraphic { get; set; }
        public Rectangle WellPosition { get; private set; }
    }
}
