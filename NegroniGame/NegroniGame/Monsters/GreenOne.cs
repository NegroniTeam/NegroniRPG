namespace NegroniGame.Monsters
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public class GreenOne : Monster, Interfaces.IMonster
    {
        // base: Mob ID, starting Position, Name, Textures, starting HP, mob Speed
        public GreenOne(int numberOfMob, Rectangle initialMonsterPos)
            : base(numberOfMob, initialMonsterPos, "Green One", Screens.GameScreen.Instance.MonstersTextures[2], 100)
        { }
    }
}
