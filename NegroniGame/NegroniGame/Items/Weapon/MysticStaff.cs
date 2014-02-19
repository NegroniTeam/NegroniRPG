namespace NegroniGame.Items.Weapon
{
    using System;

    public class MysticStaff : Weapon, Interfaces.IWeapon
    {
        public MysticStaff()
            : base("Mystic Staff", 50, Screens.GameScreen.Instance.MysticStaffTexture)
        {}

    }
}
