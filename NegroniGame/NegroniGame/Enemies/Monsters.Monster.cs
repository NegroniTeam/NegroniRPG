namespace NegroniGame.Monsters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;

    public class Monster : Interfaces.IMonster
    {
        private const int HP_POINTS_INITIAL = 100;
        private const int MOBS_SPEED = 2;
        private const float TIME_TO_CHANGE_DIRECTION = 1; // sec 5
        private const int MOVE_MAX_LENGTH = 80;

        public Rectangle MonsterPosition;

        public Monster(List<Texture2D> mobTextures, Rectangle spawnPosition, int numberOfMob)
        {
            this.Delay = 200f;
            this.Frames = 0;
            this.MonsterTextures = mobTextures;
            this.MonsterAnim = MonsterTextures[3];
            this.MonsterPosition = spawnPosition;
            this.HpPointsCurrent = 100;
            this.RandomGenerator = new Random();
            this.DirectionForMovement = -1;
            this.ID = numberOfMob;
        }

        public void Move(GameTime gameTime)
        {
            this.ElapsedTimeChangePos += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.ElapsedTimeChangePos >= TIME_TO_CHANGE_DIRECTION)
            {
                this.DirectionForMovement = this.RandomGenerator.Next(1, 5);
                this.ElapsedTimeChangePos = 0;
                this.PositionsToMove = int.MinValue;
            }


            if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.North) // up
            {
                if (this.PositionsToMove == int.MinValue)
                {
                    int maxPosition = (this.MonsterPosition.Y > MOVE_MAX_LENGTH) ? MOVE_MAX_LENGTH : this.MonsterPosition.Y;
                    maxPosition = (maxPosition <= 0) ? 0 : maxPosition;

                    this.PositionsToMove = this.RandomGenerator.Next(0, maxPosition);
                }
                else if (this.PositionsToMove > 0)
                {
                    Rectangle newPosition = new Rectangle((int)this.MonsterPosition.X, (int)(this.MonsterPosition.Y - MOBS_SPEED), 32, 32);

                    if (IntersectsWithObstacles(newPosition))
                    {
                        this.PositionsToMove = int.MinValue;
                    }
                    else
                    {
                        this.MonsterPosition.Y -= MOBS_SPEED;
                        this.SourcePosition = Animate(gameTime);
                        this.MonsterAnim = this.MonsterTextures[2];
                        this.PositionsToMove -= MOBS_SPEED;
                    }
                }
            }

            else if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.South) // down
            {
                if (this.PositionsToMove == int.MinValue)
                {
                    int maxPosition = ((Screens.GameScreen.ScreenHeight - 170 - this.MonsterPosition.Y) > MOVE_MAX_LENGTH) ? MOVE_MAX_LENGTH : (Screens.GameScreen.ScreenHeight - 170 - this.MonsterPosition.Y);
                    maxPosition = (maxPosition < 0) ? 0 : maxPosition;

                    this.PositionsToMove = this.RandomGenerator.Next(0, maxPosition);
                }

                else if (this.PositionsToMove > 0)
                {
                    Rectangle newPosition = new Rectangle((int)this.MonsterPosition.X, (int)(this.MonsterPosition.Y + MOBS_SPEED), 32, 32);

                    if (IntersectsWithObstacles(newPosition))
                    {
                        this.PositionsToMove = int.MinValue;
                    }
                    else
                    {
                        this.MonsterPosition.Y += MOBS_SPEED;
                        this.SourcePosition = Animate(gameTime);
                        this.MonsterAnim = this.MonsterTextures[3];
                        this.PositionsToMove -= MOBS_SPEED;
                    }
                }
            }

            else if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.West) // left
            {
                if (this.PositionsToMove == int.MinValue)
                {
                    int maxPosition = (this.MonsterPosition.X > MOVE_MAX_LENGTH) ? MOVE_MAX_LENGTH : this.MonsterPosition.X;
                    maxPosition = (maxPosition < 0) ? 0 : maxPosition;

                    this.PositionsToMove = this.RandomGenerator.Next(0, maxPosition);
                }

                else if (this.PositionsToMove > 0)
                {
                    Rectangle newPosition = new Rectangle((int)(this.MonsterPosition.X - MOBS_SPEED), (int)this.MonsterPosition.Y, 32, 32);

                    if (IntersectsWithObstacles(newPosition))
                    {
                        this.PositionsToMove = int.MinValue;
                    }
                    else
                    {
                        this.MonsterPosition.X -= MOBS_SPEED;
                        this.SourcePosition = Animate(gameTime);
                        this.MonsterAnim = this.MonsterTextures[1];
                        this.PositionsToMove -= MOBS_SPEED;
                    }
                }
            }

            else if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.East) // right
            {
                if (this.PositionsToMove == int.MinValue)
                {
                    int maxPosition = ((Screens.GameScreen.ScreenWidth - 30 - this.MonsterPosition.X) > MOVE_MAX_LENGTH) ? MOVE_MAX_LENGTH : (Screens.GameScreen.ScreenWidth - 30 - this.MonsterPosition.X);
                    maxPosition = (maxPosition < 0) ? 0 : maxPosition;

                    this.PositionsToMove = this.RandomGenerator.Next(0, maxPosition);
                }

                else if (this.PositionsToMove > 0)
                {
                    Rectangle newPosition = new Rectangle((int)(this.MonsterPosition.X + MOBS_SPEED), (int)this.MonsterPosition.Y, 32, 32);

                    if (IntersectsWithObstacles(newPosition))
                    {
                        this.PositionsToMove = int.MinValue;
                    }
                    else
                    {
                        this.MonsterPosition.X += MOBS_SPEED;
                        this.SourcePosition = Animate(gameTime);
                        this.MonsterAnim = this.MonsterTextures[0];
                        this.PositionsToMove -= MOBS_SPEED;
                    }
                }
            }

            this.SourcePosition = Animate(gameTime);
            this.DestinationPosition = new Rectangle((int)this.MonsterPosition.X, (int)this.MonsterPosition.Y, 32, 32);

        }

        private bool IntersectsWithObstacles(Rectangle newPosition)
        {
            bool intersectsWithObstacles = false;
            bool intersectsWithAnotherMob = false;
            
            // checks if the new position intersects with another mob
            foreach (Monster monster in Monsters.MonsterGroup.Instance.SpawnedMobs)
            {
                if (monster.ID != this.ID && monster.DestinationPosition.Intersects(newPosition))
                {
                    intersectsWithAnotherMob = true;
                    break;
                }
            }

            // checks if the new position is not well or market
            if (Well.Instance.WellPosition.Intersects(newPosition)
            || Market.Instance.MarketPosition.Intersects(newPosition)
            || Player.Instance.DestinationPosition.Intersects(newPosition)
            || intersectsWithAnotherMob == true)
            {
                intersectsWithObstacles = true;
            }

            return intersectsWithObstacles;
        }

        private Rectangle Animate(GameTime gameTime)
        {
            this.Elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Elapsed >= this.Delay)
            {
                if (this.Frames >= 2)
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
            return new Rectangle(32 * Frames, 0, 32, 32);
        }

        public void Draw(SpriteBatch sb)
        {
            new SystemFunctions.Sprite(this.MonsterAnim, this.DestinationPosition, this.SourcePosition).DrawBoxAnim(sb);
        }

        public int ID { get; private set; }
        public int DirectionForMovement { get; private set; }
        public int PositionsToMove { get; private set; }
        public Random RandomGenerator { get; private set; }
        public string Name { get; private set; }
        public List<Texture2D> MonsterTextures { get; private set; }
        public float Elapsed { get; private set; }
        public float ElapsedTimeChangePos { get; private set; }
        public float Delay { get; private set; }
        public int Frames { get; private set; }
        public Texture2D MonsterAnim { get; private set; }
        public Rectangle SourcePosition { get; private set; }
        public int HpPointsCurrent { get; set; }
        public SystemFunctions.DirectionsEnum Direction { get; private set; }
        public Rectangle DestinationPosition { get; private set; }
    }
}
