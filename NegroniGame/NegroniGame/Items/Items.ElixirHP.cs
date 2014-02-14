namespace NegroniGame.Items
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class ElixirHP : Interfaces.IItem
    {
        public const int RECOVERY_AMOUNT = 10;
        public const int REUSE_TIME = 10;

        public ElixirHP(int count)
        {
            this.Count = count;
            this.Name = "HP Elixir";
            this.ElapsedTimeElixir = REUSE_TIME;
        }
        public ElixirHP(int count, Texture2D texture)
            : this(count)
        {
            this.Texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            this.ElapsedTimeElixir += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void DestroyElixir()
        {
            this.Count--;
            this.ElapsedTimeElixir = 0;
        }

        public string Name { get; private set; }
        public int Count { get; private set; }
        public Texture2D Texture { get; set; }
        public float ElapsedTimeElixir { get; private set; }
    }
}
