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
            this.ShotTargets = new List<int>();
        }

        public static MonsterGroup Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MonsterGroup();
                }
                return instance;
            }
        }

        public const int MAX_SPAWNED_MOBS = 35;

        // Player.Instance.PlayerPosition
        public void SpawnGenerator(GameTime gameTime)
        {
            this.Elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // generates time to next spawn
            if (this.SpawnedMobsNumber == 0 && this.IsCountingDownToSpawn == false)
            {
                this.Elapsed = 0;
                this.TimeToNextSpawn = 1;
                this.IsCountingDownToSpawn = true;
            }
            else if (this.SpawnedMobsNumber < MAX_SPAWNED_MOBS && this.IsCountingDownToSpawn == false)
            {
                this.Elapsed = 0;
                this.TimeToNextSpawn = RandomGenerator.Next(1, 2); ///// 5, 15
                this.IsCountingDownToSpawn = true;
            }

            // generates spawn position
            if (this.Elapsed >= this.TimeToNextSpawn && this.IsCountingDownToSpawn == true)
            {
                // generates picture of the mob
                sbyte numberOfTexture = (sbyte)RandomGenerator.Next(0, 6);


                // generates position
                int positionX = RandomGenerator.Next(1, Screens.GameScreen.ScreenWidth - 30);
                int positionY = RandomGenerator.Next(1, Screens.GameScreen.ScreenHeight - 170);
                this.SpawnPosition = new Rectangle(positionX, positionY, 32, 32);

                // checks if the generated position is OK and excludes positions
                while (true)
                {
                    bool doesIntersectWithMobs = false;
                    bool doesIntersectWithDrop = false;

                    for (int index = 0; index < SpawnedMobs.Count; index++)
                    {
                        if (this.SpawnPosition.Intersects(SpawnedMobs[index].MonsterPosition))
                        {
                            doesIntersectWithMobs = true;
                            break;
                        }
                    }

                    for (int index = 0; index < Scenery.Instance.DropList.Count; index++)
                    {
                        if (this.SpawnPosition.Intersects(Scenery.Instance.DropList[index].DropPosition))
                        {
                            doesIntersectWithDrop = true;
                            break;
                        }
                    }

                    if (!this.SpawnPosition.Intersects(Player.Instance.DestinationPosition)
                    && !this.SpawnPosition.Intersects(Well.Instance.WellPosition)
                    && !this.SpawnPosition.Intersects(Market.Instance.MarketPosition)
                    && !doesIntersectWithMobs
                    && !doesIntersectWithDrop)
                    {
                        break;
                    }

                    positionX = RandomGenerator.Next(1, Screens.GameScreen.ScreenWidth - 30);
                    positionY = RandomGenerator.Next(1, Screens.GameScreen.ScreenHeight - 170);
                    this.SpawnPosition = new Rectangle(positionX, positionY, 32, 32);
                }

                // adds the mob
                this.SpawnedMobs.Add(new Monster(this.MonsterTextures[numberOfTexture], this.SpawnPosition, this.SpawnedMobsNumber));

                // returns values of variables
                this.SpawnedMobsNumber++;
                this.IsCountingDownToSpawn = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            // updates the position of all monsters
            foreach (var Monster in SpawnedMobs)
            {
                Monster.Move(gameTime);
            }

            // Checks for dead monsters STARTS //
            this.IndexesForDeletion = new List<int>();

            foreach (int mobIndex in this.ShotTargets)
            {
                SpawnedMobs[mobIndex].HpPointsCurrent -= Player.Instance.WeaponDmg;
                if (SpawnedMobs[mobIndex].HpPointsCurrent <= 0)
                {
                    IndexesForDeletion.Add(mobIndex);
                }
            }

            this.ShotTargets = new List<int>();

            foreach (int mobIndex in IndexesForDeletion)
            {
                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { ">> Mob died.", Color.Cyan } });

                // adds drop on the place of the dead mob
                Drop currentDrop = new Drop(this.SpawnedMobs[mobIndex].MonsterPosition);
                Scenery.Instance.AddDrop(currentDrop);
                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> Mob drops {0} {1}.", currentDrop.Amount, currentDrop.Name), Color.SpringGreen } });

                this.SpawnedMobsNumber--;
                this.SpawnedMobs.RemoveAt(mobIndex);
            }

            // Checks for dead monsters ENDS //
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
        public List<int> ShotTargets { get; set; }
        public List<int> IndexesForDeletion { get; private set; }
    }
}
