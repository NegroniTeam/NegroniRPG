namespace NegroniGame.Items
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class ElixirHP : Interfaces.IItem
    {
        public ElixirHP(int count)
        {
            this.Count = count;
            this.Name = "HP Elixir";
            this.RecoveryAmount = 10;
        }
        public ElixirHP(int count, Texture2D texture)
            : this(count)
        {
            this.Texture = texture;
        }

        public string Name { get; private set; }
        public int Count { get; private set; }
        public int RecoveryAmount { get; private set; }
        public Texture2D Texture { get; set; }
    }
}
