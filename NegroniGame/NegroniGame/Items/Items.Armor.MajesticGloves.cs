namespace NegroniGame.Items.Armor
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class MajesticGloves : Armor, Interfaces.IGloves
    {
        public MajesticGloves(Texture2D texture)
            : base("Majestic Gloves", 10, texture)
        { }
    }
}
