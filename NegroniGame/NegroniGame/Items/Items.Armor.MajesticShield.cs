namespace NegroniGame.Items.Armor
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class MajesticShield : Armor, Interfaces.IShield
    {
        public MajesticShield(Texture2D texture)
            : base("Majestic Shield", 15, texture)
        { }
    }
}
