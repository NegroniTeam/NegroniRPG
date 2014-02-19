namespace NegroniGame.Items.Armor
{
    using System;

    public class MajesticGloves : Armor, Interfaces.IGloves
    {
        public MajesticGloves()
            : base("Majestic Gloves", 10, Screens.GameScreen.Instance.MajesticSetTextures[1])
        { }
    }
}
