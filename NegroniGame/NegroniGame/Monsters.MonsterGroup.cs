namespace NegroniGame.Monsters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;

    public sealed class MonsterGroup
    {
        // Singleton !
        private static MonsterGroup instance;

        private MonsterGroup()
        {
            this.RandomGenerator = new Random();
            this.SpawnedMobsNumber = 0;
            this.SpawnedMobs = new List<Monster>();
            this.TimeToNextSpawn = 0;
            this.IsCountingDownToSpawn = false;
        }

        public static MonsterGroup Instance
        {
            get            {                if (instance == null)                {                    instance = new MonsterGroup();                }                return instance;
             }
        }

        public int number;

        // Player.Instance.PlayerPosition
        public void SpawnGenerator(GameTime gameTime)
        {
            this.Elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.SpawnedMobsNumber == 0 && this.IsCountingDownToSpawn == false)
            {
                // generates time to next spawn
                this.TimeToNextSpawn = 1;
                this.IsCountingDownToSpawn = true;
            }
            else if (this.SpawnedMobsNumber < 4 && this.IsCountingDownToSpawn == false)
            {
                // generates time to next spawn
                this.TimeToNextSpawn = RandomGenerator.Next(5, 10);
                this.IsCountingDownToSpawn = true;
            }
            if (this.Elapsed >= this.TimeToNextSpawn && this.IsCountingDownToSpawn == true)
            {
                // generates picture of the mob
                sbyte numberOfTexture = (sbyte)RandomGenerator.Next(0, 4);

                // excludes positions

                // generates position
                int positionX = RandomGenerator.Next(1, Screens.GameScreen.ScreenWidth - 30);
                int positionY = RandomGenerator.Next(1, Screens.GameScreen.ScreenHeight - 170);
                this.SpawnPosition = new Rectangle(positionX, positionY, 32, 32);

                // checks if the generated position is OK
                while (true)
                {
                    bool doesIntersectWithMobs = false;

                    for (int index = 0; index < SpawnedMobs.Count; index++)
                    {
                        if (this.SpawnPosition.Intersects(SpawnedMobs[index].MonsterPosition))
                        {
                            doesIntersectWithMobs = true;
                            break;
                        }
                    }

                    if (!this.SpawnPosition.Intersects(Player.Instance.DestinationPosition)
                    && !this.SpawnPosition.Intersects(Well.Instance.WellPosition)
                    && !this.SpawnPosition.Intersects(Market.Instance.MarketPosition)
                    && !doesIntersectWithMobs)
                    {
                        break;
                    }

                    positionX = RandomGenerator.Next(1, Screens.GameScreen.ScreenWidth - 30);
                    positionY = RandomGenerator.Next(1, Screens.GameScreen.ScreenHeight - 170);
                    this.SpawnPosition = new Rectangle(positionX, positionY, 32, 32);
                }

                // adds the mob
                this.SpawnedMobs.Add(new Monster(this.MonsterTextures[numberOfTexture], this.SpawnPosition));

                // returns values of variables
                this.SpawnedMobsNumber++;
                this.IsCountingDownToSpawn = false;
                this.Elapsed = 0;
            }
        }

        //public bool CheckForCollision(int positionX, int positionY)
        //{
        //    if (positionX)
        //    {
        //        return true;
        //    }

        //    return false;
        //}


        public void Move(GameTime gameTime)
        {
            foreach (var Monster in SpawnedMobs)
            {
                Monster.Move(gameTime);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (var Monster in SpawnedMobs)
            {
                Monster.Draw(spritebatch);
            }
        }

        public Random RandomGenerator { get; private set; }
        public float Elapsed { get; private set; }
        public int TimeToNextSpawn { get; private set; }
        public bool IsCountingDownToSpawn { get; private set; }
        public List<Monster> SpawnedMobs { get; private set; }
        public sbyte SpawnedMobsNumber { get; private set; }
        public Rectangle SpawnPosition { get; private set; }
        public List<List<Texture2D>> MonsterTextures { get; set; }
    }
}
