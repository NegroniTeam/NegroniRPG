namespace NegroniGame.Items
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public struct Coins
    {
        public Coins(int amount)
            : this()
        {
            this.Amount = amount;
        }

        public Coins(int amount, Texture2D texture)
            : this(amount)
        {
            this.Texture = texture;
        }

        public int Amount { get; private set; }
        public Texture2D Texture { get; set; }
    }
}
