namespace NegroniGame.Items.Weapon
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class NewbieStaff : Weapon, Interfaces.IWeapon
    {
        public NewbieStaff(Texture2D texture)
            : base("Newbie Staff", 20, texture)
        { }

    }
}
