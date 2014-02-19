namespace NegroniGame.Monsters
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public class Viking : Monster, Interfaces.IMonster
    {
        // base: Mob ID, starting Position, Name, Textures, starting HP, mob Speed
        public Viking(int numberOfMob, Rectangle initialMonsterPos)
            : base(numberOfMob, initialMonsterPos, "Viking", Screens.GameScreen.Instance.MonstersTextures[1], 100)
        { }
    }
}
