namespace NegroniGame.Toolbar
{
    using System;
    using System.Collections.Generic;
    using SystemFunctions;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public sealed class SystemMsg
    {
        // Singleton !
        private static SystemMsg instance;

        private SystemMsg()
        {
            this.Font = Screens.GameScreen.Instance.FontMessages;
            this.LinesAsSprites = new List<Sprite>();
            this.ScreenHeight = Screens.GameScreen.ScreenHeight;
            this.ScreenWidth = Screens.GameScreen.ScreenWidth;
            this.AllMessages = new List<Dictionary<string, Color>>();
        }

        public static SystemMsg Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SystemMsg();
                }
                return instance;
            }
        }

        public void GetLastMessages()
        {
            // Update(); // updates the messages

            // defines the position of the first message
            this.MsgXPos = 130;
            this.MsgYPos = this.ScreenHeight - 20;

            LinesAsSprites = new List<Sprite>();

            if (AllMessages.Count > 0)
            {
                int endIndex;

                if (AllMessages.Count > 5)
                {
                    endIndex = AllMessages.Count - 6;
                }
                else
                {
                    endIndex = 0;
                }

                for (int i = AllMessages.Count - 1; i >= endIndex; i--)
                {
                    // gets the one record in the Dictionary
                    foreach (var record in AllMessages[i])
                    {
                        this.LinesAsSprites.Add(new Sprite(this.Font, record.Key, new Vector2(this.MsgXPos, this.MsgYPos), record.Value));
                        this.MsgYPos -= 15;
                    }
                }
            }
        }

        public void DrawText()
        {
            int msgForDisplay = (LinesAsSprites.Count <= 5) ? LinesAsSprites.Count : 5;

            for (int i = 0; i < msgForDisplay; i++)
            {
                this.LinesAsSprites[i].DrawText();
            }
        }

        //private void Update()
        //{
        //    // TO DO: auto fill with new msgs
        //}

        public int MsgXPos { get; private set; }
        public int MsgYPos { get; private set; }
        public SpriteFont Font { get; private set; }
        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        public List<Dictionary<string, Color>> AllMessages { get; set; }
        public List<Sprite> LinesAsSprites { get; private set; }
    }
}
