﻿namespace NegroniGame
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Shot
    {
        private const float SHOT_SPEED = 4f;
        private const float SHOT_ANIM_SPEED = 100f;
        private const int RANGE = 150;
        private Vector2 shotPosition;
        private readonly SystemFunctions.DirectionsEnum direction;

        public Shot(Vector2 playerPosition, SystemFunctions.DirectionsEnum currentDirection)
        {
            this.ShotTextures = Screens.GameScreen.Instance.ShotsTextures;
            this.ShotTexture = this.ShotTextures[0];
            this.direction = currentDirection;

            if (this.direction == SystemFunctions.DirectionsEnum.South)
            {
                this.shotPosition = new Vector2(playerPosition.X + 8, playerPosition.Y + 15);
                this.EndPoint = new Point((int)(shotPosition.X), (int)(shotPosition.Y + RANGE));
            }
            else if (this.direction == SystemFunctions.DirectionsEnum.North)
            {
                this.shotPosition = new Vector2(playerPosition.X + 8, playerPosition.Y - 8);
                this.EndPoint = new Point((int)(shotPosition.X), (int)(shotPosition.Y - RANGE));
            }
            else if (this.direction == SystemFunctions.DirectionsEnum.East)
            {
                this.shotPosition = new Vector2(playerPosition.X + 24, playerPosition.Y + 12);
                this.EndPoint = new Point((int)(shotPosition.X + RANGE), (int)(shotPosition.Y));
            }
            else if (this.direction == SystemFunctions.DirectionsEnum.West)
            {
                this.shotPosition = new Vector2(playerPosition.X - 8, playerPosition.Y + 12);
                this.EndPoint = new Point((int)(shotPosition.X - RANGE), (int)(shotPosition.Y));
            }
        }

        // returns info if the shot should be removed
        public bool Update(GameTime gameTime)
        {
            if (this.direction == SystemFunctions.DirectionsEnum.South)
            {
                this.shotPosition.Y += SHOT_SPEED;
                this.ShotSourcePosition = AnimateShot(gameTime);
            }
            else if (this.direction == SystemFunctions.DirectionsEnum.North)
            {
                this.shotPosition.Y -= SHOT_SPEED;
                this.ShotSourcePosition = AnimateShot(gameTime);
            }
            else if (this.direction == SystemFunctions.DirectionsEnum.East)
            {
                this.shotPosition.X += SHOT_SPEED;
                this.ShotSourcePosition = AnimateShot(gameTime);
            }
            else if (this.direction == SystemFunctions.DirectionsEnum.West)
            {
                this.shotPosition.X -= SHOT_SPEED;
                this.ShotSourcePosition = AnimateShot(gameTime);
            }

            this.ShotDestPosition = new Rectangle((int)this.shotPosition.X, (int)this.shotPosition.Y, 16, 16);

            if (this.ShotDestPosition.Contains(EndPoint) // The shot is removed if the shot reaches the end point of the range
                || this.ShotDestPosition.Intersects(Well.Instance.WellPosition) // The shot is removed if the shot reaches object on the map
                || this.ShotDestPosition.Intersects(Scenery.Instance.MarketPosition)
                || this.ShotDestPosition.Y > Screens.GameScreen.ScreenHeight - 135)
            {
                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { ">> You missed the target.", Color.Pink } });
                return true;
            }

            // The shot is removed if it reaches mob
            for (int index = 0; index < Monsters.MonstersHandler.Instance.SpawnedMobs.Count; index++)
            {
                if (this.ShotDestPosition.Intersects(Monsters.MonstersHandler.Instance.SpawnedMobs[index].DestinationPosition)) // if (this.ShotDestPosition.Intersects(Monsters.MonsterGroup.Instance.SpawnedMobs[index].MonsterPosition))
                {
                    Monsters.MonstersHandler.Instance.MobsHit.Add(index);

                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You did {0} dmg to {1}.", Player.Instance.WeaponDmg, Monsters.MonstersHandler.Instance.SpawnedMobs[index].Name), Color.Yellow } });
                    return true;
                }
            }

            return false;
        }

        private Rectangle AnimateShot(GameTime gameTime)
        {
            this.Elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            
            if (this.Elapsed >= SHOT_ANIM_SPEED)
            {
                if (this.Frames >= 7)
                {
                    this.Frames = 0;
                }
                else
                {
                    this.Frames++;
                }
                this.Elapsed = 0;
            }

            // if on frame 0 - top up position 0
            return new Rectangle(16 * this.Frames, 0, 16, 16);
        }

        public void Draw()
        {
            new SystemFunctions.Sprite(this.ShotTexture, this.ShotDestPosition, this.ShotSourcePosition).DrawBoxAnim();
        }


        public float Elapsed { get; private set; }
        public int Frames { get; private set; }

        //public Rectangle SourcePosition { get; private set; }
        //public Rectangle DestinationPosition { get; private set; }

        public Point EndPoint { get; private set; }
        public List<Texture2D> ShotTextures { get; private set; }
        public Texture2D ShotTexture { get; private set; }
        public Rectangle ShotDestPosition { get; private set; }
        public Rectangle ShotSourcePosition { get; private set; }

        // public Rectangle shotPosition { get; private set; }
    }
}
