namespace NegroniGame.Toolbar
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using SystemFunctions;

    public class SystemMsg
    {
        // constructor gets everything needed from the game class
        public SystemMsg(SpriteFont font)
        {
            this.Font = font;
            this.LinesAsSprites = new List<Sprite>();
            this.ScreenHeight = Screens.GameScreen.ScreenHeight;
            this.ScreenWidth = Screens.GameScreen.ScreenWidth;
        }

        public void Update()
        {
            // retrieves all last 5 messages from the method
            this.Last5Messages = GetLastMessages();

            // defines the position of the first message
            this.MsgXPos = 130;
            this.MsgYPos = this.ScreenHeight - 20;

            foreach (var Message in this.Last5Messages)
            {
                this.LinesAsSprites.Add(new Sprite(this.Font, Message.Key, new Vector2(this.MsgXPos, this.MsgYPos), Message.Value));
                this.MsgYPos -= 15;
            }
        }

        public void DrawText(SpriteBatch sb)
        {
            for (int i = 0; i < 5; i++)
            {
                this.LinesAsSprites[i].DrawText(sb);
            }
        }

        private Dictionary<string, Color> GetLastMessages()
        {
            // TO DO: loops through the array with messages and takes the last 5
            Dictionary<string, Color> last5Messages = new Dictionary<string, Color>();

            last5Messages.Add(">> You stepped on a mob", Color.Red);
            last5Messages.Add(">> You hit with 50 dmg", Color.Yellow);
            last5Messages.Add(">> You drunk from the well", Color.Aquamarine);
            last5Messages.Add(">> Mob hit you with a feather", Color.Red);
            last5Messages.Add(">> You already have a robe", Color.Pink);

            return last5Messages;
        }

        public int MsgXPos { get; private set; }
        public int MsgYPos { get; private set; }
        public SpriteFont Font { get; private set; }
        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        public Dictionary<string, Color> Last5Messages { get; private set; }
        public List<Sprite> LinesAsSprites { get; private set; }
    }
}
