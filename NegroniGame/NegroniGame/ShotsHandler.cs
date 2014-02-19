﻿namespace NegroniGame
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public sealed class ShotsHandler
    {
        // Singleton !
        private static ShotsHandler instance;

        private const float SHOT_REUSE_TIME = 1200f;
        private float elapsedTimeShot;
        private List<int> indexesForDeletionShots = new List<int>();

        private ShotsHandler()
        {
            this.Shots = new List<Shot>();
        }

        public static ShotsHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShotsHandler();
                }
                return instance;
            }
        }

        public List<Shot> Shots { get; private set; }
        
        public void UpdateShots(GameTime gameTime, KeyboardState ks)
        {
            this.elapsedTimeShot += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Adds new shot to the list
            if (ks.IsKeyDown(Keys.Space))
            {
                if (this.elapsedTimeShot >= SHOT_REUSE_TIME)
                {
                    Shots.Add(new Shot(new Vector2(Player.Instance.DestinationPosition.X, Player.Instance.DestinationPosition.Y), Player.Instance.Direction));
                    this.elapsedTimeShot = 0;
                }
            }

            // Updates shots
            for (int index = 0; index < Shots.Count; index++)
            {
                bool isOutOfRange = Shots[index].Update(gameTime);

                if (isOutOfRange == true)
                {
                    this.indexesForDeletionShots.Add(index);
                }
            }

            foreach (int index in indexesForDeletionShots)
            {
                Shots.RemoveAt(index);
            }

            indexesForDeletionShots = new List<int>();
        }


        public void Draw()
        {
            foreach (var shot in Shots)
            {
                shot.Draw();
            }
        }
    }
}
