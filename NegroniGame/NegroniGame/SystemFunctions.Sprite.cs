namespace NegroniGame.SystemFunctions
{
    using System;
    using System.Text;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Sprite
    {
        public Sprite(Texture2D texture, Vector2 position, Color? tint = null)
        {
            this.Texture = texture;
            this.Position = position;
            this.Tint = tint.HasValue ? tint.Value : Color.White;
        }
        public Sprite(Texture2D texture, Rectangle destinationBox, [Optional]Rectangle sourceBox, Color? tint = null)
        {
            this.Texture = texture;
            this.DestinationBox = destinationBox;
            this.SourceBox = sourceBox;
            this.Tint = tint.HasValue ? tint.Value : Color.White;
        }
        public Sprite(SpriteFont font, string text, Vector2 position, Color? tint = null)
        {
            this.Font = font;
            this.Text = text;
            this.Position = position;
            this.Tint = tint.HasValue ? tint.Value : Color.White;
        }

        public virtual void Update(GameTime gt){}
         
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(this.Texture, this.Position, this.Tint);
        }
        public virtual void DrawBox(SpriteBatch sb)
        {
            sb.Draw(this.Texture, this.DestinationBox, this.Tint);
        }
        public virtual void DrawBoxAnim(SpriteBatch sb)
        {
            sb.Draw(this.Texture, this.DestinationBox, this.SourceBox, this.Tint);
        }
        public virtual void DrawText(SpriteBatch sb)
        {
            sb.DrawString(this.Font, this.Text, this.Position, this.Tint);
        }

        public Color Tint { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle DestinationBox { get; private set; }
        public Rectangle SourceBox { get; private set; }
        public Texture2D Texture { get; set; }
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
 
    }
}