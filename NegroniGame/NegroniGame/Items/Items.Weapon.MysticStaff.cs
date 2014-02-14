namespace NegroniGame.Items.Weapon
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class MysticStaff : Weapon, Interfaces.IWeapon
    {
        public MysticStaff(Texture2D texture)
            : base("Mystic Staff", 50, texture)
        { }
    }
}
