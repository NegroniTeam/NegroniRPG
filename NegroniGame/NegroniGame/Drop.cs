namespace NegroniGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public class Drop
    {
        public const float SECONDS_TO_DISAPPEARING = 20;

        public Drop(Rectangle mobPosition)
        {
            RandomGenerator = new Random();
            int numberOfTexture = RandomGenerator.Next(0, 0);

            this.DropTextures = GameScreen.Instance.DropTextures;
            this.CurrentTexture = DropTextures[numberOfTexture];

            this.DropPosition = mobPosition;

            // drop is coins, than generate random its amount
            if (this.CurrentTexture == this.DropTextures[0])
            {
                this.Name = "Coins";
                this.Amount = RandomGenerator.Next(10, 20);
            }
            else
            {
                this.Name = "Coins";
                this.Amount = 1;
            }

        }
        public bool Update(GameTime gameTime)
        {
            this.ElapsedTimeDrop += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (ElapsedTimeDrop >= SECONDS_TO_DISAPPEARING)
            {
                return true;
            }
            return false;
        }

        public void Draw()
        {
            new SystemFunctions.Sprite(CurrentTexture, DropPosition).DrawBox();
        }

        public string Name { get; private set; }
        public int Amount { get; private set; }
        public Texture2D CurrentTexture { get; private set; }
        public List<Texture2D> DropTextures { get; private set; }
        public Rectangle DropPosition { get; private set; }
        public float ElapsedTimeDrop { get; private set; }
        // public float InitialTimeDrop { get; private set; }
        public Random RandomGenerator { get; private set; }
    }
}
