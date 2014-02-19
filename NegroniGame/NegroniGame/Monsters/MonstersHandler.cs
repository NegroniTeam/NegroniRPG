namespace NegroniGame.Monsters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public sealed class MonstersHandler
    {
        // Singleton !
        private static MonstersHandler instance;

        public const int MAX_SPAWNED_MOBS = 20; // 4

        private readonly Random randomGenerator = new Random();
        private bool isCountingDownToSpawn = false;
        private float elapsedTimeToNextSpawn;
        private int timeToNextSpawn = 0;
        private List<int> indexesForDeletion;

        private MonstersHandler()
        {
            this.SpawnedMobsNumber = 0;
            this.SpawnedMobs = new List<Monster>();
            this.MobsHit = new List<int>();
        }

        public static MonstersHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MonstersHandler();
                }
                return instance;
            }
        }

        public List<Monster> SpawnedMobs { get; private set; }
        public sbyte SpawnedMobsNumber { get; private set; }
        public List<int> MobsHit { get; set; }

        public void Update(GameTime gameTime)
        {
            // updates the position of all monsters
            foreach (var monster in SpawnedMobs)
            {
                monster.Move(gameTime);
            }

            // generates new mobs
            SpawnGenerator(gameTime);

            // Checks for dead monsters STARTS //
            this.indexesForDeletion = new List<int>();

            foreach (int mobIndex in this.MobsHit)
            {
                SpawnedMobs[mobIndex].HpPointsCurrent -= Player.Instance.WeaponDmg;
                if (SpawnedMobs[mobIndex].HpPointsCurrent <= 0)
                {
                    indexesForDeletion.Add(mobIndex);
                }
            }

            this.MobsHit = new List<int>();

            foreach (int mobIndex in indexesForDeletion)
            {
                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} died.", this.SpawnedMobs[mobIndex].Name), Color.Cyan } });

                // adds drop on the place of the dead mob
                Drop currentDrop = new Drop(this.SpawnedMobs[mobIndex].DestinationPosition);
                Scenery.Instance.AddDrop(currentDrop);

                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() {
                            { String.Format(">> {0} drops {1} {2}.", this.SpawnedMobs[mobIndex].Name, currentDrop.Amount, currentDrop.Name), Color.SpringGreen } });

                this.SpawnedMobsNumber--;
                this.SpawnedMobs.RemoveAt(mobIndex);
            }

            // Checks for dead monsters ENDS //
        }

        public void Draw()
        {
            foreach (var monster in SpawnedMobs)
            {
                monster.Draw();
            }
        }

        private void SpawnGenerator(GameTime gameTime)
        {
            this.elapsedTimeToNextSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // generates time to next spawn
            if (this.SpawnedMobsNumber == 0 && this.isCountingDownToSpawn == false)
            {
                this.elapsedTimeToNextSpawn = 0;
                this.timeToNextSpawn = 1;
                this.isCountingDownToSpawn = true;
            }
            else if (this.SpawnedMobsNumber < MAX_SPAWNED_MOBS && this.isCountingDownToSpawn == false)
            {
                this.elapsedTimeToNextSpawn = 0;
                this.timeToNextSpawn = randomGenerator.Next(1, 2); ///// 5, 15 ////////////////////
                this.isCountingDownToSpawn = true;
            }

            // creates new mob
            if (this.elapsedTimeToNextSpawn >= this.timeToNextSpawn && this.isCountingDownToSpawn == true)
            {
                this.SpawnedMobs.Add(new Monster(this.SpawnedMobsNumber));

                // returns values of variables
                this.SpawnedMobsNumber++;
                this.isCountingDownToSpawn = false;
            }
        }
    }
}
