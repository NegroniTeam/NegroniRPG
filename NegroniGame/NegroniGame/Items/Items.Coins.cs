namespace NegroniGame.Items
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public struct Coins
    {
        public Coins(int amount) : this()
        {
            this.Amount = amount;
            this.Texture = Screens.GameScreen.Instance.coinsTex;
        }

        public int Amount { get; private set; }
        public Texture2D Texture { get; set; }
    }
}
