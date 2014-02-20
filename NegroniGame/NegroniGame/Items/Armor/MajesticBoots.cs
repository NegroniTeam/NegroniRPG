namespace NegroniGame.Items.Armor
{
    using System;

    public class MajesticBoots : Armor, Interfaces.IBoots
    {
        public MajesticBoots()
            : base("Majestic Boots", 10, 100, Screens.GameScreen.Instance.MajesticSetTextures[0])
        { }
    }
}
