namespace NegroniGame.Monsters
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public class RedDragon : Monster, Interfaces.IMonster
    {
        // base: Mob ID, starting Position, Name, Textures, starting HP, mob Speed
        public RedDragon(int numberOfMob, Rectangle initialMonsterPos)
            : base(numberOfMob, initialMonsterPos, "Red Dragon", Screens.GameScreen.Instance.MonstersTextures[0], 100)
        { }
    }
}
