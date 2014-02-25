namespace NegroniGame.SystemFunctions
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Animation
    {
        private int frameCounter;
        private int switchFrame;
        private bool active;

        private Vector2 position;
        private Vector2 amountOfFrames;
        private Rectangle sourceRect;

        public Vector2 CurrentFrame;

        public int AnimationSpeed { get; set; }

        public Vector2 Position { get { return this.position; } set { this.position = value; } }

        public int FrameWidth
        {
            get { return (int)(this.AnimationImage.Width / this.amountOfFrames.X); }
        }

        public int FrameHeight
        {
            get { return (int)(this.AnimationImage.Height / this.amountOfFrames.Y); }
        }

        public bool Active { get { return this.active; } set { this.active = value; } }

        public Texture2D AnimationImage { get; set; }

        public void Initialize(Vector2 position, Vector2 frames)
        {
            this.active = false;
            this.switchFrame = 170;             // Set default value
            this.position = position;
            this.amountOfFrames = frames;
        }

        public void Update(GameTime gameTime)
        {
            if (this.active)
            {
                this.frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                this.frameCounter = 0;
            }

            if (this.frameCounter >= this.switchFrame)
            {
                this.frameCounter = 0;
                this.CurrentFrame.X += this.FrameWidth;
                if (this.CurrentFrame.X >= this.AnimationImage.Width)
                {
                    this.CurrentFrame.X = 0;
                }
            }

            this.sourceRect = new Rectangle((int)this.CurrentFrame.X, (int)this.CurrentFrame.Y * this.FrameHeight, this.FrameWidth, this.FrameHeight);
        }

        public void Draw()
        {
            GameScreen.Instance.SpriteBatch.Draw(this.AnimationImage, this.position, this.sourceRect, Color.White);
        }
    }
}