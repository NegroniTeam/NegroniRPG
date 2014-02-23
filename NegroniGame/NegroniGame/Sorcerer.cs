namespace NegroniGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using NegroniGame.Interfaces;
    using NegroniGame.SystemFunctions;

    public class Sorcerer : SpriteObjectAnime, IReact
    {
        private Rectangle reactRect;

        public Sorcerer(string spriteName, Vector2 amountOfFrames, Vector2 position)
        {
            this.name = spriteName;
            this.position = position;
            this.amountOfFrames = amountOfFrames;
        }

        public override void Initialize()
        {
            this.animation = new Animation();
            this.animation.Initialize(this.position, this.amountOfFrames);
            this.animation.Active = true;
        }

        public override void LoadContent(ContentManager content)
        {
            this.image = content.Load<Texture2D>(this.name);
            this.animation.AnimationImage = this.image;
            this.drawRect = new Rectangle((int)this.position.X, (int)this.position.Y, (int)(this.image.Width / this.amountOfFrames.X), (int)(this.image.Height / this.amountOfFrames.Y));
            this.reactRect = this.drawRect;
            this.reactRect.Width += 20;
            this.reactRect.Height += 20;
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            this.animation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.animation.Draw(spriteBatch);
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