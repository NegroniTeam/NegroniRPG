namespace NegroniGame.SystemFunctions
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class SpriteObject
    {
        public bool isActive;
        protected Vector2 position;

        public string Name { get; protected set; }
        public Texture2D Image { get; protected set; }
        public Rectangle DrawRect { get; protected set; }
        public Vector2 AmountOfFrames { get; protected set; }
        public Vector2 Position { get { return this.position; } protected set { this.position = value; } }
        public bool IsActive { get { return this.isActive; } protected set { this.isActive = value; } }
        public bool IsDeleted { get; protected set; }

        // TODO: Add your initialization logic here
        public abstract void Initialize();

        // TODO: use this.Content to load your game content here
        public abstract void LoadContent(ContentManager content);

        // TODO: Unload any non ContentManager content here
        public abstract void UnloadContent();

        // TODO: Add your update logic here
        public abstract void Update(GameTime gameTime);

        // TODO: Add your drawing code here
        public abstract void Draw();
    }
}