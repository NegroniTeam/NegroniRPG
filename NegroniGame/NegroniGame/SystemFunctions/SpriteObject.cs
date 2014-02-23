namespace NegroniGame.SystemFunctions
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class SpriteObject
    {
        protected string name;
        protected Texture2D image;
        protected Vector2 position;
        protected Rectangle drawRect;
        protected Vector2 amountOfFrames;
        protected bool isActive;

        public string Name { get { return this.name; } }
        public Texture2D Image { get { return this.image; } }
        public Vector2 Position { get { return this.position; } }
        public Rectangle DrawRect { get { return this.drawRect; } }
        public bool IsActive { get { return this.isActive; } set { this.isActive = value; } }

        // TODO: Add your initialization logic here
        public abstract void Initialize();

        // TODO: use this.Content to load your game content here
        public abstract void LoadContent(ContentManager content);

        // TODO: Unload any non ContentManager content here
        public abstract void UnloadContent();

        // TODO: Add your update logic here
        public abstract void Update(GameTime gameTime);

        // TODO: Add your drawing code here
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}