namespace NegroniGame
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Market
    {
        // Singleton !
        private static Market instance;

        private Market()
        {
            this.MarketPosition = new Rectangle(Screens.GameScreen.ScreenWidth - 115, 5, 102, 54);
        }

        public static Market Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Market();
                }
                return instance;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            new SystemFunctions.Sprite(this.MarketGraphic, this.MarketPosition).DrawBox(sb);
        }

        public Texture2D MarketGraphic { get; set; }
        public Rectangle MarketPosition { get; private set; }
    }
}
