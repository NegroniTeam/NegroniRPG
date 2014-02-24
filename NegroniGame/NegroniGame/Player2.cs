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
        private float moveSpeed = 120;

        private Rectangle reactRect;

        private KeyboardState keyState;

        public Player2(string spriteName, Vector2 amountOfFrames, Vector2 playerPosition)
        {
            this.name = spriteName;
            this.position = playerPosition;
            this.amountOfFrames = amountOfFrames;
        }

        public override void Initialize()
        {
            this.animation = new Animation();
            this.animation.Initialize(this.Position, this.amountOfFrames);
            this.animation.Active = true;
            this.tempCurrentFrame = Vector2.Zero;
        }

        public override void LoadContent(ContentManager content)
        {
            this.image = content.Load<Texture2D>(this.Name);
            this.animation.AnimationImage = this.Image;
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (this.IsActive)
            {
                this.drawRect = new Rectangle((int)this.position.X, (int)this.position.Y, (int)(this.image.Width / this.amountOfFrames.X), (int)(this.image.Height / this.amountOfFrames.Y));
                this.reactRect = new Rectangle(this.drawRect.X - 10, this.drawRect.Y - 10, this.drawRect.Width + 10, this.drawRect.Height + 10);
                this.keyState = Keyboard.GetState();

                if (this.keyState.IsKeyDown(Keys.Down))
                {
                    this.position.Y += this.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    this.tempCurrentFrame.Y = 0;
                }
                else if (this.keyState.IsKeyDown(Keys.Up))
                {
                    this.position.Y -= this.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    this.tempCurrentFrame.Y = 3;
                }
                else if (this.keyState.IsKeyDown(Keys.Right))
                {
                    this.position.X += this.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    this.tempCurrentFrame.Y = 2;
                }
                else if (this.keyState.IsKeyDown(Keys.Left))
                {
                    this.position.X -= this.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    this.tempCurrentFrame.Y = 1;
                }

                this.BounceLeftRight();
                this.BounceTopBottom();

                this.tempCurrentFrame.X = this.animation.CurrentFrame.X;

                this.animation.Position = this.Position;
                this.animation.CurrentFrame = this.tempCurrentFrame;

                this.animation.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                this.animation.Draw(spriteBatch);
            }
        }

        private void BounceTopBottom()
        {
            if (this.Position.Y < 0)
            {
                // bounce off top
                this.position.Y = 0;
            }
            else if ((this.Position.Y + this.animation.FrameHeight) > GameScreen.ScreenHeight - 130)
            {
                // bounce off bottom
                this.position.Y = GameScreen.ScreenHeight - this.animation.FrameHeight - 130;
            }
        }

        private void BounceLeftRight()
        {
            if (this.Position.X < 0)
            {
                // bounc off left
                this.position.X = 0;
            }
            else if ((this.Position.X + this.animation.FrameWidth) > GameScreen.ScreenWidth)
            {
                // bounce off right
                this.position.X = GameScreen.ScreenWidth - this.animation.FrameWidth;
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