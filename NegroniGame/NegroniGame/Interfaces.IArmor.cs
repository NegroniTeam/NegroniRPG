namespace NegroniGame.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IArmor : IItem
    {
        int Defence { get; }
    }
}
