namespace NegroniGame.Items.Armor
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class MajesticRobe : Armor, Interfaces.IRobe
    {
        public MajesticRobe(Texture2D texture)
            : base("Majestic Robe", 25, texture)
        { }
    }
}
