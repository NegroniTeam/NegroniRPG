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
            base.Name = spriteName;
            base.Position = position;
            base.AmountOfFrames = amountOfFrames;
        }

        public override void Initialize()
        {
            base.Animation = new Animation();
            base.Animation.Initialize(base.Position, base.AmountOfFrames);
            base.Animation.Active = true;
        }

        public override void LoadContent(ContentManager content)
        {
            base.Image = content.Load<Texture2D>(base.Name);
            base.Animation.AnimationImage = base.Image;
            base.DrawRect = new Rectangle((int)base.Position.X, (int)base.Position.Y, (int)(base.Image.Width / base.AmountOfFrames.X), (int)(base.Image.Height / base.AmountOfFrames.Y));
            this.reactRect = base.DrawRect;
            this.reactRect.Width += 20;
            this.reactRect.Height += 20;
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Animation.Update(gameTime);
        }

        public override void Draw()
        {
            base.Animation.Draw();
        }

        public Rectangle ReactRect
        {
            get { return this.reactRect; }
        }

        public void DoAction<T>(IReact obj)
        {
            (obj as SpriteObject).isActive = false;
        }
    }
}