namespace NegroniGame.Monsters
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public class Bug : Monster, Interfaces.IMonster
    {
        // base: Mob ID, starting Position, Name, Textures, starting HP, mob Speed
        public Bug(int numberOfMob, Rectangle initialMonsterPos)
            : base(numberOfMob, initialMonsterPos, "Bug", GameScreen.Instance.MonstersTextures[3], 100)
        { }
    }
}
