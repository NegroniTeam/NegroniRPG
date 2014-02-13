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
        private const float TIME_TO_CHANGE_DIRECTION = 200f;

        public Rectangle MonsterPosition;

        public Monster(List<Texture2D> mobTextures, Rectangle spawnPosition)
        {
            this.Delay = 200f;
            this.Frames = 0;
            this.MonsterTextures = mobTextures;
            this.MonsterAnim = MonsterTextures[3];
            this.MonsterPosition = spawnPosition;
            this.HpPointsCurrent = 100;
        }

        public void Move(GameTime gameTime)
        {
            // some logic for movement
            Random randomGenerator = new Random();
            int direction = -1;

            this.ElapsedChangeMonsterPosition += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.ElapsedChangeMonsterPosition >= TIME_TO_CHANGE_DIRECTION)
            {
                direction = randomGenerator.Next(1, 5);
                this.ElapsedChangeMonsterPosition = 0;
            }

            if (direction == 1)// right
            {
                if (this.MonsterPosition.X < Screens.GameScreen.ScreenWidth - 30)
                {
                    this.MonsterPosition.X += 10;
                    this.SourcePosition = Animate(gameTime);
                    this.MonsterAnim = this.MonsterTextures[0];
                    this.Direction = SystemFunctions.DirectionsEnum.East;
                }
            }

            else if (direction == 2)// left
            {
                if (this.MonsterPosition.X > 0)
                {
                    this.MonsterPosition.X -= 10;
                    this.SourcePosition = Animate(gameTime);
                    this.MonsterAnim = this.MonsterTextures[1];
                    this.Direction = SystemFunctions.DirectionsEnum.West;
                }
            }

            else if (direction == 3)// top
            {
                if (this.MonsterPosition.Y > 0)
                {
                    this.MonsterPosition.Y -= 10;
                    this.SourcePosition = Animate(gameTime);
                    this.MonsterAnim = this.MonsterTextures[2];
                    this.Direction = SystemFunctions.DirectionsEnum.North;
                }
            }

            else if (direction == 4) // bottom
            {
                if (this.MonsterPosition.Y <= Screens.GameScreen.ScreenHeight - 170)
                {
                    this.MonsterPosition.Y += 10;
                    this.SourcePosition = Animate(gameTime);
                    this.MonsterAnim = this.MonsterTextures[3];
                    this.Direction = SystemFunctions.DirectionsEnum.South;
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

        public string Name { get; private set; }
        public List<Texture2D> MonsterTextures { get; private set; }
        public float Elapsed { get; private set; }
        public float ElapsedChangeMonsterPosition { get; private set; }
        public float Delay { get; private set; }
        public int Frames { get; private set; }
        public Texture2D MonsterAnim { get; private set; }
        public Rectangle SourcePosition { get; private set; }
        public int HpPointsCurrent { get; set; }
        public SystemFunctions.DirectionsEnum Direction { get; private set; }

        public Rectangle DestinationPosition { get; private set; }
    }
}
