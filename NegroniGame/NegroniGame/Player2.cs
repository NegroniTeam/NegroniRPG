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
        //private readonly float moveSpeed = 120;
        private int frameCounter = 0;
        private int activeTime = 30000;

        private Rectangle reactRect;

        public Player2(string spriteName, Vector2 amountOfFrames, Vector2 playerPosition)
        {
            base.Name = spriteName;
            base.position = playerPosition;
            base.AmountOfFrames = amountOfFrames;
        }

        public override void Initialize()
        {
            base.Animation = new Animation();
            base.Animation.Initialize(base.position, base.AmountOfFrames);
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
            if (this.isActive)
            {
                this.frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (this.frameCounter > this.activeTime)
                {
                    this.frameCounter = 0;
                    this.isActive = false;
                }
            }

            if (Player.Instance.Direction == DirectionsEnum.East)
            {
                base.position.X = Player.Instance.DestinationPosition.X - 40;
                base.position.Y = Player.Instance.DestinationPosition.Y - 40;
                this.tempCurrentFrame.Y = 2;
            }
            else if (Player.Instance.Direction == DirectionsEnum.North)
            {
                base.position.X = Player.Instance.DestinationPosition.X;
                base.position.Y = Player.Instance.DestinationPosition.Y + 10;
                this.tempCurrentFrame.Y = 3;
            }
            else if (Player.Instance.Direction == DirectionsEnum.South)
            {
                base.position.X = Player.Instance.DestinationPosition.X;
                base.position.Y = Player.Instance.DestinationPosition.Y - 50;
                this.tempCurrentFrame.Y = 0;
            }
            else if (Player.Instance.Direction == DirectionsEnum.West)
            {
                base.position.X = Player.Instance.DestinationPosition.X + 10;
                base.position.Y = Player.Instance.DestinationPosition.Y - 40;
                this.tempCurrentFrame.Y = 1;
            }

            this.DrawRect = new Rectangle((int)base.position.X, (int)base.position.Y, (int)(base.Image.Width / base.AmountOfFrames.X), (int)(base.Image.Height / base.AmountOfFrames.Y));

            if (GameScreen.Instance.KeyboardState.IsKeyDown(Keys.Enter))
            {
                this.reactRect = new Rectangle(this.DrawRect.X - 10, this.DrawRect.Y - 10, this.DrawRect.Width + 10, this.DrawRect.Height + 10);
                GameManager.DoReaction<IReact>(this);
            }

            this.reactRect = Rectangle.Empty;

            this.tempCurrentFrame.X = base.Animation.CurrentFrame.X;

            base.Animation.Position = base.position;
            base.Animation.CurrentFrame = this.tempCurrentFrame;

            base.Animation.Update(gameTime);
        }

        public override void Draw()
        {
            if (IsActive)
            {
                base.Animation.Draw();
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