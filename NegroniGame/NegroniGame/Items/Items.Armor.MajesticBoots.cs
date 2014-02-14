namespace NegroniGame.Items.Armor
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class MajesticBoots : Armor, Interfaces.IBoots
    {
        public MajesticBoots(Texture2D texture)
            : base("Majestic Boots", 10, texture)
        { }
    }
}
