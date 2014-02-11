namespace NegroniGame.Toolbar
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using SystemFunctions;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public sealed class SystemMsg
    {
        //// Singleton !
        //private static SystemMsg instance;

        //private SystemMsg() { }

        //public static SystemMsg Instance
        //{
        //    get        //    {        //        if (instance == null)        //        {
        //            instance = new SystemMsg();        //        }        //        return instance;
        //     }
        //}

        // constructor gets everything needed from the game class
        public SystemMsg(SpriteFont font)
        {
            this.Font = font;
            this.LinesAsSprites = new List<Sprite>();
            this.ScreenHeight = Screens.GameScreen.ScreenHeight;
            this.ScreenWidth = Screens.GameScreen.ScreenWidth;
            this.AllMessages = new List<Dictionary<string, Color>>();
        }

        public void GetLastMessages()
        {
            Update(); // updates the messages

            // defines the position of the first message
            this.MsgXPos = 130;
            this.MsgYPos = this.ScreenHeight - 20;

            for (int i = AllMessages.Count - 1; i > AllMessages.Count - 6; i--)
            {
                // gets the one record in the Dictionary
                foreach (var Record in AllMessages[i])
                {
                    this.LinesAsSprites.Add(new Sprite(this.Font, Record.Key, new Vector2(this.MsgXPos, this.MsgYPos), Record.Value));
                    this.MsgYPos -= 15;
                }
            }
        }

        public void DrawText(SpriteBatch sb)
        {
            for (int i = 0; i < 5; i++)
            {
                this.LinesAsSprites[i].DrawText(sb);
            }
        }

        private void Update()
        {
            // TO DO: auto fill with new msgs
            AllMessages.Add(new Dictionary<string, Color>() { { ">> You stepped on a mob", Color.Red } });
            AllMessages.Add(new Dictionary<string, Color>() { { ">> You hit with 50 dmg", Color.Yellow } });
            AllMessages.Add(new Dictionary<string, Color>() { { ">> You drunk from the well", Color.Aquamarine } });
            AllMessages.Add(new Dictionary<string, Color>() { { ">> Mob hit you with a feather", Color.Red } });
            AllMessages.Add(new Dictionary<string, Color>() { { ">> You already have a robe", Color.Pink } });
        }

        public int MsgXPos { get; private set; }
        public int MsgYPos { get; private set; }
        public SpriteFont Font { get; private set; }
        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        public List<Dictionary<string, Color>> AllMessages { get; private set; }
        // public List<Dictionary<string, Color>> Last5Messages { get; private set; }
        public List<Sprite> LinesAsSprites { get; private set; }
    }
}
