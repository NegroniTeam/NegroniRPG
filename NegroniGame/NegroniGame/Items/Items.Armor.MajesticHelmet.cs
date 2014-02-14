namespace NegroniGame.Items.Armor
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class MajesticHelmet : Armor, Interfaces.IHelmet
    {
        public MajesticHelmet(Texture2D texture)
            : base("Majestic Helmet", 10, texture)
        { }
    }
}
