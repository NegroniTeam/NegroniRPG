namespace NegroniGame.Items.Armor
{
    using System;

    public class MajesticHelmet : Armor, Interfaces.IHelmet
    {
        public MajesticHelmet()
            : base("Majestic Helmet", 10, Screens.GameScreen.Instance.MajesticSetTextures[2])
        { }
    }
}
