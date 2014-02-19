namespace NegroniGame.Items.Armor
{
    using System;

    public class MajesticRobe : Armor, Interfaces.IRobe
    {
        public MajesticRobe()
            : base("Majestic Robe", 25, Screens.GameScreen.Instance.MajesticSetTextures[3])
        { }
    }
}
