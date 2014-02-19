namespace NegroniGame.Items.Weapon
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Weapon : Interfaces.IWeapon
    {
        public Weapon()
        { }

        public Weapon(string name, int attack, Texture2D texture)
        {
            this.Name = name;
            this.Attack = attack;
            this.Texture = texture;
        }

        public string Name { get; private set; }
        public int Attack { get; private set; }
        public Texture2D Texture { get; private set; }
    }
}
