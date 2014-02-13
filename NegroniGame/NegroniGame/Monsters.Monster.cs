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
        private const float TIME_TO_CHANGE_DIRECTION = 5; // sec
        private const int MOVE_MAX_LENGTH = 80;

        public Rectangle MonsterPosition;

        public Monster(List<Texture2D> mobTextures, Rectangle spawnPosition)
        {
            this.Delay = 200f;
            this.Frames = 0;
            this.MonsterTextures = mobTextures;
            this.MonsterAnim = MonsterTextures[3];
            this.MonsterPosition = spawnPosition;
            this.HpPointsCurrent = 100;
            this.RandomGenerator = new Random();
            this.DirectionForMovement = -1;
        }

        public void Move(GameTime gameTime)
        {
            this.ElapsedTimeChangePos += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.ElapsedTimeChangePos >= TIME_TO_CHANGE_DIRECTION)
            {
                this.DirectionForMovement = this.RandomGenerator.Next(1, 5);
                this.ElapsedTimeChangePos = 0;
                this.PositionToMove = int.MinValue;
            }


            if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.North) // up
            {
                if (this.PositionToMove == int.MinValue)
                {
                    int maxPosition = (this.MonsterPosition.Y > MOVE_MAX_LENGTH) ? MOVE_MAX_LENGTH : this.MonsterPosition.Y;
                    maxPosition = (maxPosition <= 0) ? 0 : maxPosition;

                    this.PositionToMove = this.RandomGenerator.Next(0, maxPosition);
                    float positionToMove = this.MonsterPosition.X + this.PositionToMove;
                }

                if (this.PositionToMove > 0)
                {
                    this.MonsterPosition.Y -= MOBS_SPEED;
                    this.SourcePosition = Animate(gameTime);
                    this.MonsterAnim = this.MonsterTextures[2];
                    this.PositionToMove -= MOBS_SPEED;
                }
            }

            else if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.South) // down
            {
                if (this.PositionToMove == int.MinValue)
                {
                    int maxPosition = ((Screens.GameScreen.ScreenHeight - 170 - this.MonsterPosition.Y) > MOVE_MAX_LENGTH) ? MOVE_MAX_LENGTH : (Screens.GameScreen.ScreenHeight - 170 - this.MonsterPosition.Y);
                    maxPosition = (maxPosition < 0) ? 0 : maxPosition;

                    this.PositionToMove = this.RandomGenerator.Next(0, maxPosition);
                    float positionToMove = this.MonsterPosition.X + this.PositionToMove;
                }

                if (this.PositionToMove > 0)
                {
                    this.MonsterPosition.Y += MOBS_SPEED;
                    this.SourcePosition = Animate(gameTime);
                    this.MonsterAnim = this.MonsterTextures[3];
                    this.PositionToMove -= MOBS_SPEED;
                }
            }

            else if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.West) // left
            {
                if (this.PositionToMove == int.MinValue)
                {
                    int maxPosition = (this.MonsterPosition.X > MOVE_MAX_LENGTH) ? MOVE_MAX_LENGTH : this.MonsterPosition.X;
                    maxPosition = (maxPosition < 0) ? 0 : maxPosition;

                    this.PositionToMove = this.RandomGenerator.Next(0, maxPosition);
                    float positionToMove = this.MonsterPosition.X + this.PositionToMove;
                }

                if (this.PositionToMove > 0)
                {
                    this.MonsterPosition.X -= MOBS_SPEED;
                    this.SourcePosition = Animate(gameTime);
                    this.MonsterAnim = this.MonsterTextures[1];
                    this.PositionToMove -= MOBS_SPEED;
                }
            }

            else if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.East) // right
            {
                if (this.PositionToMove == int.MinValue)
                {
                    int maxPosition = ((Screens.GameScreen.ScreenWidth - 30 - this.MonsterPosition.X) > MOVE_MAX_LENGTH) ? MOVE_MAX_LENGTH : (Screens.GameScreen.ScreenWidth - 30 - this.MonsterPosition.X);
                    maxPosition = (maxPosition < 0) ? 0 : maxPosition;

                    this.PositionToMove = this.RandomGenerator.Next(0, maxPosition);
                    float positionToMove = this.MonsterPosition.X + this.PositionToMove;
                }

                if (this.PositionToMove > 0)
                {
                    this.MonsterPosition.X += MOBS_SPEED;
                    this.SourcePosition = Animate(gameTime);
                    this.MonsterAnim = this.MonsterTextures[0];
                    this.PositionToMove -= MOBS_SPEED;
                }
            }

            this.SourcePosition = Animate(gameTime);
            this.DestinationPosition = new Rectangle((int)this.MonsterPosition.X, (int)this.MonsterPosition.Y, 32, 32);

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

        public int DirectionForMovement { get; private set; }
        public int PositionToMove { get; private set; }
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
