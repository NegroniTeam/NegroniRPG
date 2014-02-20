namespace NegroniGame
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class MarketDialogHandler
    {
        // Singleton !
        private static MarketDialogHandler instance;


        private MarketDialogHandler()
        {
            IsInMarket = false;
        }

        public static MarketDialogHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MarketDialogHandler();
                }
                return instance;
            }
        }

        public bool IsInMarket { get; set; } // TO DO PRIVATE

        public void Update(MouseState mouseState, MouseState mouseStatePrevious)
        {
            if (IsInMarket == false
                && Player.Instance.DestinationPosition.Intersects(
                    new Rectangle(Scenery.Instance.MarketPosition.X - 5, Scenery.Instance.MarketPosition.Y - 5, Scenery.Instance.MarketPosition.Width + 10, Scenery.Instance.MarketPosition.Height + 10))
                && Screens.GameScreen.Instance.KeyboardState.IsKeyDown(Keys.Enter)
                && Screens.GameScreen.Instance.KeyboardStatePrevious.IsKeyUp(Keys.Enter))
            {
                IsInMarket = true;
                Screens.GameScreen.Instance.IsPaused = true;
            }
            else if (IsInMarket == true)
            {
                MarketDialog.Instance.CheckIfSomethingBought(mouseState, mouseStatePrevious);

                if (Screens.GameScreen.Instance.KeyboardState.IsKeyDown(Keys.Enter)
                    && Screens.GameScreen.Instance.KeyboardStatePrevious.IsKeyUp(Keys.Enter))
                {
                    IsInMarket = false;
                    Screens.GameScreen.Instance.IsPaused = false;
                }
            }
        }

        public void Draw()
        {
            if (IsInMarket)
            {
                MarketDialog.Instance.Draw();
            }
        }

    }
}
