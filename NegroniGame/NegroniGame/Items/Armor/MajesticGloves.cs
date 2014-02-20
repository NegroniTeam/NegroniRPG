﻿namespace NegroniGame.Items.Armor
{
    using System;

    public class MajesticGloves : Armor, Interfaces.IGloves
    {
        public MajesticGloves()
            : base("Majestic Gloves", 4, 50, Screens.GameScreen.Instance.MajesticSetTextures[1])
        { }
    }
}
