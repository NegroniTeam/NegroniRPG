namespace NegroniGame.Monsters
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public class PurpleBat : Monster, Interfaces.IMonster
    {
        // base: Mob ID, starting Position, Name, Textures, starting HP, mob Speed
        public PurpleBat(int numberOfMob, Rectangle initialMonsterPos)
            : base(numberOfMob, initialMonsterPos, "Purple Bat", GameScreen.Instance.MonstersTextures[5], 100)
        { }
    }
}
