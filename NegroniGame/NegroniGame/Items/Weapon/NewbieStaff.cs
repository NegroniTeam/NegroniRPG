namespace NegroniGame.Items.Weapon
{
    using System;

    public class NewbieStaff : Weapon, Interfaces.IWeapon
    {
        public NewbieStaff()
            : base("Newbie Staff", 20, Screens.GameScreen.Instance.NewbieStaffTexture)
        {}

    }
}
