namespace NegroniGame
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;

    public class Shots
    {
        private const float SHOT_SPEED = 4f;
        private const float SHOT_ANIM_SPEED = 100f;
        private const int RANGE = 150;
        private Vector2 ShotPosition;
        private readonly SystemFunctions.DirectionsEnum Direction;

        public Shots(Vector2 playerPosition, SystemFunctions.DirectionsEnum direction)
        {
            this.ShotTextures = Screens.GameScreen.Instance.shotsTextures;
            this.ShotTexture = this.ShotTextures[0];
            this.Direction = direction;

            if (this.Direction == SystemFunctions.DirectionsEnum.South)
            {
                this.ShotPosition = new Vector2(playerPosition.X + 8, playerPosition.Y + 15);
                this.EndPoint = new Point((int)(ShotPosition.X), (int)(ShotPosition.Y + RANGE));
            }
            else if (this.Direction == SystemFunctions.DirectionsEnum.North)
            {
                this.ShotPosition = new Vector2(playerPosition.X + 8, playerPosition.Y - 8);
                this.EndPoint = new Point((int)(ShotPosition.X), (int)(ShotPosition.Y - RANGE));
            }
            else if (this.Direction == SystemFunctions.DirectionsEnum.East)
            {
                this.ShotPosition = new Vector2(playerPosition.X + 24, playerPosition.Y + 12);
                this.EndPoint = new Point((int)(ShotPosition.X + RANGE), (int)(ShotPosition.Y));
            }
            else if (this.Direction == SystemFunctions.DirectionsEnum.West)
            {
                this.ShotPosition = new Vector2(playerPosition.X - 8, playerPosition.Y + 12);
                this.EndPoint = new Point((int)(ShotPosition.X - RANGE), (int)(ShotPosition.Y));
            }
        }

        // returns info if the shot should be removed
        public bool Update(GameTime gameTime)
        {
            if (this.Direction == SystemFunctions.DirectionsEnum.South)
            {
                this.ShotPosition.Y += SHOT_SPEED;
                this.ShotSourcePosition = AnimateShot(gameTime);
            }
            else if (this.Direction == SystemFunctions.DirectionsEnum.North)
            {
                this.ShotPosition.Y -= SHOT_SPEED;
                this.ShotSourcePosition = AnimateShot(gameTime);
            }
            else if (this.Direction == SystemFunctions.DirectionsEnum.East)
            {
                this.ShotPosition.X += SHOT_SPEED;
                this.ShotSourcePosition = AnimateShot(gameTime);
            }
            else if (this.Direction == SystemFunctions.DirectionsEnum.West)
            {
                this.ShotPosition.X -= SHOT_SPEED;
                this.ShotSourcePosition = AnimateShot(gameTime);
            }

            this.ShotDestPosition = new Rectangle((int)this.ShotPosition.X, (int)this.ShotPosition.Y, 16, 16);

            if (this.ShotDestPosition.Contains(EndPoint) // The shot is removed if the shot reaches the end point of the range
                || this.ShotDestPosition.Intersects(Well.Instance.WellPosition) // The shot is removed if the shot reaches object on the map
                || this.ShotDestPosition.Intersects(Market.Instance.MarketPosition))
            {
                return true;
            }

            // The shot is removed if it reaches mob
            for (int index = 0; index < Monsters.MonsterGroup.Instance.SpawnedMobs.Count; index++)
            {
                if (this.ShotDestPosition.Intersects(Monsters.MonsterGroup.Instance.SpawnedMobs[index].MonsterPosition))
                {
                    Monsters.MonsterGroup.Instance.ShotTargets.Add(index);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            new SystemFunctions.Sprite(this.ShotTexture, this.ShotDestPosition, this.ShotSourcePosition).DrawBoxAnim(spriteBatch);
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

        // public Rectangle ShotPosition { get; private set; }
    }
}
