namespace NegroniGame.Monsters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Monster : Interfaces.IMonster
    {
        private const int HP_POINTS_INITIAL = 100;
        private const int MOBS_SPEED = 2;
        private const float TIME_TO_CHANGE_DIRECTION = 3; // sec 5
        private const int MOVE_MAX_LENGTH = 80;
        private const float ANIM_DELAY = 200f;

        private int frames = 0;
        private readonly Random randomGenerator = new Random();
        private Rectangle animSourcePosition;
        private int positionsToMove;
        private float elapsedTimeChangeAnim;
        private float elapsedTimeChangePos;
        private List<Texture2D> monsterTextures;
        private Texture2D monsterAnim;
        private Rectangle monsterPosition;

        public Monster(int numberOfMob)
        {
            this.ID = numberOfMob;
            this.HpPointsCurrent = HP_POINTS_INITIAL;
            this.DirectionForMovement = -1;
            this.Name = "Mob";

            GenerateSpawn();
        }

        public int ID { get; private set; }
        public string Name { get; private set; }
        public int DirectionForMovement { get; private set; }
        public Rectangle DestinationPosition { get; private set; }
        public int HpPointsCurrent { get; set; }

        // public Rectangle monsterPosition { get; private set; }

        public void Draw()
        {
            new SystemFunctions.Sprite(this.monsterAnim, this.DestinationPosition, this.animSourcePosition).DrawBoxAnim();
        }

        private void GenerateSpawn()
        {
            // generates picture of the mob
            sbyte numberOfTexture = (sbyte)randomGenerator.Next(0, 6);
            this.monsterTextures = Screens.GameScreen.Instance.MonstersTextures[numberOfTexture];
            this.monsterAnim = monsterTextures[3];

            // generates position
            int positionX = randomGenerator.Next(1, Screens.GameScreen.ScreenWidth - 30);
            int positionY = randomGenerator.Next(1, Screens.GameScreen.ScreenHeight - 170);
            this.monsterPosition = new Rectangle(positionX, positionY, 32, 32);

            // checks if the generated position is OK and excludes positions
            while (true)
            {
                bool doesIntersectWithMobs = false;
                bool doesIntersectWithDrop = false;

                for (int index = 0; index < Monsters.MonstersHandler.Instance.SpawnedMobs.Count; index++)
                {
                    if (this.monsterPosition.Intersects(Monsters.MonstersHandler.Instance.SpawnedMobs[index].monsterPosition))
                    {
                        doesIntersectWithMobs = true;
                        break;
                    }
                }

                for (int index = 0; index < Scenery.Instance.DropList.Count; index++)
                {
                    if (this.monsterPosition.Intersects(Scenery.Instance.DropList[index].DropPosition))
                    {
                        doesIntersectWithDrop = true;
                        break;
                    }
                }

                if (!this.monsterPosition.Intersects(Player.Instance.DestinationPosition)
                && !this.monsterPosition.Intersects(Well.Instance.WellPosition)
                && !this.monsterPosition.Intersects(Market.Instance.MarketPosition)
                && !doesIntersectWithMobs
                && !doesIntersectWithDrop)
                {
                    break;
                }

                positionX = randomGenerator.Next(1, Screens.GameScreen.ScreenWidth - 30);
                positionY = randomGenerator.Next(1, Screens.GameScreen.ScreenHeight - 170);
                this.monsterPosition = new Rectangle(positionX, positionY, 32, 32);
            }
        }

        public void Move(GameTime gameTime)
        {
            this.elapsedTimeChangePos += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.elapsedTimeChangePos >= TIME_TO_CHANGE_DIRECTION)
            {
                this.DirectionForMovement = this.randomGenerator.Next(1, 5);
                this.elapsedTimeChangePos = 0;
                this.positionsToMove = int.MinValue;
            }


            if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.North) // up
            {
                if (this.positionsToMove == int.MinValue)
                {
                    int maxPosition = (this.monsterPosition.Y > MOVE_MAX_LENGTH) ? MOVE_MAX_LENGTH : this.monsterPosition.Y;
                    maxPosition = (maxPosition <= 0) ? 0 : maxPosition;

                    this.positionsToMove = this.randomGenerator.Next(0, maxPosition);
                }
                else if (this.positionsToMove > 0)
                {
                    Rectangle newPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y - MOBS_SPEED, 32, 32);

                    if (IntersectsWithObstacles(newPosition))
                    {
                        this.positionsToMove = int.MinValue;
                    }
                    else
                    {
                        this.monsterPosition.Y -= MOBS_SPEED;
                        this.animSourcePosition = Animate(gameTime);
                        this.monsterAnim = this.monsterTextures[2];
                        this.positionsToMove -= MOBS_SPEED;
                    }
                }
            }

            else if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.South) // down
            {
                if (this.positionsToMove == int.MinValue)
                {
                    int maxPosition = ((Screens.GameScreen.ScreenHeight - 170 - this.monsterPosition.Y) > MOVE_MAX_LENGTH) ? MOVE_MAX_LENGTH : (Screens.GameScreen.ScreenHeight - 170 - this.monsterPosition.Y);
                    maxPosition = (maxPosition < 0) ? 0 : maxPosition;

                    this.positionsToMove = this.randomGenerator.Next(0, maxPosition);
                }

                else if (this.positionsToMove > 0)
                {
                    Rectangle newPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y + MOBS_SPEED, 32, 32);

                    if (IntersectsWithObstacles(newPosition))
                    {
                        this.positionsToMove = int.MinValue;
                    }
                    else
                    {
                        this.monsterPosition.Y += MOBS_SPEED;
                        this.animSourcePosition = Animate(gameTime);
                        this.monsterAnim = this.monsterTextures[3];
                        this.positionsToMove -= MOBS_SPEED;
                    }
                }
            }

            else if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.West) // left
            {
                if (this.positionsToMove == int.MinValue)
                {
                    int maxPosition = (this.monsterPosition.X > MOVE_MAX_LENGTH) ? MOVE_MAX_LENGTH : this.monsterPosition.X;
                    maxPosition = (maxPosition < 0) ? 0 : maxPosition;

                    this.positionsToMove = this.randomGenerator.Next(0, maxPosition);
                }

                else if (this.positionsToMove > 0)
                {
                    Rectangle newPosition = new Rectangle(this.monsterPosition.X - MOBS_SPEED, this.monsterPosition.Y, 32, 32);

                    if (IntersectsWithObstacles(newPosition))
                    {
                        this.positionsToMove = int.MinValue;
                    }
                    else
                    {
                        this.monsterPosition.X -= MOBS_SPEED;
                        this.animSourcePosition = Animate(gameTime);
                        this.monsterAnim = this.monsterTextures[1];
                        this.positionsToMove -= MOBS_SPEED;
                    }
                }
            }

            else if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.East) // right
            {
                if (this.positionsToMove == int.MinValue)
                {
                    int maxPosition = ((Screens.GameScreen.ScreenWidth - 30 - this.monsterPosition.X) > MOVE_MAX_LENGTH) ? MOVE_MAX_LENGTH : (Screens.GameScreen.ScreenWidth - 30 - this.monsterPosition.X);
                    maxPosition = (maxPosition < 0) ? 0 : maxPosition;

                    this.positionsToMove = this.randomGenerator.Next(0, maxPosition);
                }

                else if (this.positionsToMove > 0)
                {
                    Rectangle newPosition = new Rectangle(this.monsterPosition.X + MOBS_SPEED, this.monsterPosition.Y, 32, 32);

                    if (IntersectsWithObstacles(newPosition))
                    {
                        this.positionsToMove = int.MinValue;
                    }
                    else
                    {
                        this.monsterPosition.X += MOBS_SPEED;
                        this.animSourcePosition = Animate(gameTime);
                        this.monsterAnim = this.monsterTextures[0];
                        this.positionsToMove -= MOBS_SPEED;
                    }
                }
            }

            this.animSourcePosition = Animate(gameTime);
            this.DestinationPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y, 32, 32);

        }

        private bool IntersectsWithObstacles(Rectangle newPosition)
        {
            bool intersectsWithObstacles = false;
            bool intersectsWithAnotherMob = false;
            
            // checks if the new position intersects with another mob
            foreach (Monster monster in Monsters.MonstersHandler.Instance.SpawnedMobs)
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
            this.elapsedTimeChangeAnim += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.elapsedTimeChangeAnim >= ANIM_DELAY)
            {
                if (this.frames >= 2)
                {
                    this.frames = 0;
                }
                else
                {
                    this.frames++;
                }
                this.elapsedTimeChangeAnim = 0;
            }

            // if on frame 0 - top up position 0
            return new Rectangle(32 * frames, 0, 32, 32);
        }
    }
}
