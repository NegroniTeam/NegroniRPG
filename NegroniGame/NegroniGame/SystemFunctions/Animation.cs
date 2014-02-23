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
        private Vector2 currentFrame;
        private Texture2D image;
        private Rectangle sourceRect;

        public Vector2 CurrentFrame
        {
            get { return this.currentFrame; }
            set { this.currentFrame = value; }
        }

        public int AnimationSpeed
        {
            get { return this.switchFrame; }
            set { this.switchFrame = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public int FrameWidth
        {
            get { return (int)(this.image.Width / this.amountOfFrames.X); }
        }

        public int FrameHeight
        {
            get { return (int)(this.image.Height / this.amountOfFrames.Y); }
        }

        public bool Active
        {
            get { return this.active; }
            set { this.active = value; }
        }

        public Texture2D AnimationImage
        {
            set { this.image = value; }
        }

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
                this.currentFrame.X += this.FrameWidth;
                if (this.currentFrame.X >= this.image.Width)
                {
                    this.currentFrame.X = 0;
                }
            }

            this.sourceRect = new Rectangle((int)this.currentFrame.X, (int)this.currentFrame.Y * this.FrameHeight, this.FrameWidth, this.FrameHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.image, this.position, this.sourceRect, Color.White);
        }
    }
}