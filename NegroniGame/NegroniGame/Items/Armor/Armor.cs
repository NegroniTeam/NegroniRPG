namespace NegroniGame.Items.Armor
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Armor : Interfaces.IArmor
    {
        public Armor()
        { }

        public Armor(string name, int defence, Texture2D texture)
        {
            this.Name = name;
            this.Defence = defence;
            this.Texture = texture;
        }

        public string Name { get; private set; }
        public int Defence { get; private set; }
        public Texture2D Texture { get; private set; }
    }
}
