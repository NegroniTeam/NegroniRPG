using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NegroniGame.Items.Weapon
{
    class MysticStaff : Interfaces.IWeapon
    {
        public string Name { get; private set; }
        public int Attack { get; private set; }
    }
}
