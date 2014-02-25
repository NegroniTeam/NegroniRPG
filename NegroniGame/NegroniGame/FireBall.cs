namespace NegroniGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using NegroniGame.Interfaces;
    using NegroniGame.Monsters;
    using NegroniGame.SystemFunctions;
    using System;

    public class FireBall : SpriteObjectAnime, IBurn
    {
        private Vector2 target;
        private Vector2 tempCurrentFrame;
        private Vector2 velocity;
        private float moveSpeed;

        //public override void LoadContent(ContentManager content)
        //{
        //    Image = content.Load<Texture2D>("sprites/FireBall");
        //    animation.AnimationImage = Image;
        //}

        public FireBall(string spriteName, Vector2 amountOfFrames, Vector2 position, IMonster targetObj)
        {
            base.Name = spriteName;
            base.position = position;
            base.AmountOfFrames = amountOfFrames;
            this.target.X = (targetObj as Monster).DestinationPosition.X;
            this.target.Y = (targetObj as Monster).DestinationPosition.Y;
        }

        public override void Initialize()
        {
            base.Animation = new Animation();
            base.Animation.Initialize(base.position, base.AmountOfFrames);
            base.Animation.Active = true;
            this.moveSpeed = 120;
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
            if (base.isActive == true)
            {
                float distance = Vector2.Distance(new Vector2(this.target.X, this.target.Y), this.position);
                float rotation = (float)Math.Atan(distance);

                Vector2 velocityTmp = Vector2.Subtract(new Vector2(this.target.X, this.target.Y), this.position);
                velocity.X = velocityTmp.X.CompareTo(0) * (float)Math.Sin(rotation);
                velocity.Y = velocityTmp.Y.CompareTo(0) * (float)Math.Sin(rotation);

                tempCurrentFrame.Y = 0;

                position.X += velocity.X * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position.Y += velocity.Y * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                base.Animation.Position = position;
                tempCurrentFrame.X = base.Animation.CurrentFrame.X;
                base.Animation.CurrentFrame = tempCurrentFrame;
                base.Animation.Update(gameTime);

                if (tempCurrentFrame.X / base.Animation.FrameWidth >= base.AmountOfFrames.X - 1)
                {
                    base.IsDeleted = true;
                }
            }
        }

        public override void Draw()
        {
            if (base.Animation.Active)
            {
                base.Animation.Draw();
            }
        }

        public Rectangle ReactRect
        {
            get { throw new NotImplementedException(); }
        }

        public void DoAction<T>(IReact obj)
        {
            throw new NotImplementedException();
        }
    }
}