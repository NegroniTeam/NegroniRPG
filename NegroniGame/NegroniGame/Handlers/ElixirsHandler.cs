namespace NegroniGame.Handlers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    public sealed class ElixirsHandler
    {
        // Singleton !
        private static ElixirsHandler instance;

        public const int REUSE_TIME = 10;

        private float elapsedTimeLastMsgElixir;
        private float elapsedTimeElixir;

        private ElixirsHandler()
        {
            this.elapsedTimeElixir = REUSE_TIME;
        }

        public static ElixirsHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ElixirsHandler();
                }
                return instance;
            }
        }

        public void Update(GameTime gameTime)
        {
            this.elapsedTimeElixir += (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.elapsedTimeLastMsgElixir += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public bool UseElixir()
        {
            if (this.elapsedTimeElixir >= REUSE_TIME)
            {
                this.RestoredPoints = ((Player.HP_POINTS_INITIAL - Player.Instance.HpPointsCurrent) >= Items.ElixirsHP.RECOVERY_AMOUNT) ? Items.ElixirsHP.RECOVERY_AMOUNT : Player.HP_POINTS_INITIAL - Player.Instance.HpPointsCurrent;

                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You restored {0} HP.", this.RestoredPoints), Color.Aquamarine } });
                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> 1 HP Elixir destroyed."), Color.Red } });

                this.elapsedTimeElixir = 0;
                this.elapsedTimeLastMsgElixir = 0;

                return true;
            }
            else
            {
                if (this.elapsedTimeLastMsgElixir >= 1)
                {
                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() {
                        { String.Format(">> {0} seconds to reuse Elixir.", REUSE_TIME - (int)this.elapsedTimeElixir), Color.Pink } });
                    this.elapsedTimeLastMsgElixir = 0;
                }
            }

            return false;
        }

        public int RestoredPoints { get; private set; }

    }
}
