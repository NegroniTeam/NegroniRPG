namespace NegroniGame.Interfaces
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public interface IItem
    {
        string Name { get; }
        Texture2D Texture { get; }
    }
}
