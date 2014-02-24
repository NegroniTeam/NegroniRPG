namespace NegroniGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using NegroniGame.Interfaces;
    using NegroniGame.SystemFunctions;

    public class Player2 : SpriteObjectAnime, IReact
    {
        private Vector2 tempCurrentFrame;
        private readonly float moveSpeed = 120;

        private Rectangle reactRect;

        private KeyboardState keyState;

        public Player2(string spriteName, Vector2 amountOfFrames, Vector2 playerPosition)
        {
            base.Name = spriteName;
            base.Position = playerPosition;
            base.AmountOfFrames = amountOfFrames;
        }

        public override void Initialize()
        {
            base.Animation = new Animation();
            base.Animation.Initialize(base.Position, base.AmountOfFrames);
            base.Animation.Active = true;
            this.tempCurrentFrame = Vector2.Zero;
        }

        public override void LoadContent(ContentManager content)
        {
            base.Image = content.Load<Texture2D>(this.Name);
            base.Animation.AnimationImage = base.Image;
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (this.IsActive)
            {
                this.DrawRect = new Rectangle((int)base.Position.X, (int)base.Position.Y, (int)(base.Image.Width / base.AmountOfFrames.X), (int)(base.Image.Height / base.AmountOfFrames.Y));
                this.reactRect = new Rectangle(this.DrawRect.X - 10, this.DrawRect.Y - 10, this.DrawRect.Width + 10, this.DrawRect.Height + 10);
                this.keyState = Keyboard.GetState();

                if (this.keyState.IsKeyDown(Keys.Down))
                {
                    base.Position.Y += this.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    this.tempCurrentFrame.Y = 0;
                }
                else if (this.keyState.IsKeyDown(Keys.Up))
                {
                    base.Position.Y -= this.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    this.tempCurrentFrame.Y = 3;
                }
                else if (this.keyState.IsKeyDown(Keys.Right))
                {
                    base.Position.X += this.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    this.tempCurrentFrame.Y = 2;
                }
                else if (this.keyState.IsKeyDown(Keys.Left))
                {
                    base.Position.X -= this.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    this.tempCurrentFrame.Y = 1;
                }

                this.BounceLeftRight();
                this.BounceTopBottom();

                this.tempCurrentFrame.X = base.Animation.CurrentFrame.X;

                base.Animation.Position = base.Position;
                base.Animation.CurrentFrame = this.tempCurrentFrame;

                base.Animation.Update(gameTime);
            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                base.Animation.Draw();
            }
        }

        private void BounceTopBottom()
        {
            if (base.Position.Y < 0)
            {
                // bounce off top
                base.Position.Y = 0;
            }
            else if ((base.Position.Y + base.Animation.FrameHeight) > GameScreen.ScreenHeight - 130)
            {
                // bounce off bottom
                base.Position.Y = GameScreen.ScreenHeight - base.Animation.FrameHeight - 130;
            }
        }

        private void BounceLeftRight()
        {
            if (base.Position.X < 0)
            {
                // bounc off left
                base.Position.X = 0;
            }
            else if ((base.Position.X + base.Animation.FrameWidth) > GameScreen.ScreenWidth)
            {
                // bounce off right
                base.Position.X = GameScreen.ScreenWidth - base.Animation.FrameWidth;
            }
        }

        public Rectangle ReactRect
        {
            get { return this.reactRect; }
        }

        public void DoAction<T>(IReact obj)
        {
            throw new System.NotImplementedException();
        }
    }
}