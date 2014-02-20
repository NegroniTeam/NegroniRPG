﻿namespace NegroniGame.Items.Armor
{
    using System;

    public class MajesticHelmet : Armor, Interfaces.IHelmet
    {
        public MajesticHelmet()
            : base("Majestic Helmet", 4, 50, Screens.GameScreen.Instance.MajesticSetTextures[2])
        { }
    }
}
