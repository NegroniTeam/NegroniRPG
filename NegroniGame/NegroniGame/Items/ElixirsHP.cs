namespace NegroniGame.Items
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public struct ElixirsHP : Interfaces.IItem
    {
        public const int RECOVERY_AMOUNT = 30;

        public ElixirsHP(int count) : this()
        {
            this.Count = count;
            this.Name = "HP Elixirs";
            this.Texture = Screens.GameScreen.Instance.ElixirsTexture;
        }

        public string Name { get; private set; }
        public int Count { get; private set; }
        public Texture2D Texture { get; private set; }
    }
}
