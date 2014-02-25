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
                this.reactRect = new Rectangle(this.DrawRect.X - 100, this.DrawRect.Y - 100, this.DrawRect.Width + 100, this.DrawRect.Height + 100);

                foreach (Monsters.Monster monster in Handlers.MonstersHandler.Instance.SpawnedMobs)
                {
                    if (monster.DestinationPosition.Intersects(reactRect) && this.frameCounter % 600 == 0)
                    {
                        FireBall fireBall = new FireBall("media/sprites/fireballs", new Vector2(8, 1), new Vector2(this.DrawRect.X + this.DrawRect.Width / 2, this.DrawRect.Y + this.DrawRect.Height / 2), monster);
                        fireBall.Initialize();
                        fireBall.LoadContent(GameScreen.Instance.Content);
                        fireBall.isActive = true;
                        GameManager.AddSprite(fireBall);

                        break;
                    }
                }

                this.tempCurrentFrame.X = base.Animation.CurrentFrame.X;

                base.Animation.Position = base.position;
                base.Animation.CurrentFrame = this.tempCurrentFrame;

                base.Animation.Update(gameTime);
            }
            else
            {
                if (GameScreen.Instance.KeyboardState.IsKeyDown(Keys.Enter))
                {
                    this.DrawRect = new Rectangle((int)Player.Instance.DestinationPosition.X, (int)Player.Instance.DestinationPosition.Y, (int)(base.Image.Width / base.AmountOfFrames.X), (int)(base.Image.Height / base.AmountOfFrames.Y));
                    this.reactRect = new Rectangle(this.DrawRect.X - 10, this.DrawRect.Y - 10, this.DrawRect.Width + 10, this.DrawRect.Height + 10);
                    GameManager.DoReaction<ImPlayer>(this);
                    this.reactRect = Rectangle.Empty;
                }
            }
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