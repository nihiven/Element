using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Element.Interfaces
{
    interface IAttachPoints
    {
        // TODO: try this out, maybe this should be the center point?
        // top left attachment points
        Vector2 HighArmor { get; }
        Vector2 MidArmor { get; }
        Vector2 LowArmor { get; }
        Vector2 HighWeapon { get; } 
        Vector2 MidWeapon { get; }
        Vector2 LowWeapon { get; }
        Vector2 HighPet { get; }
        Vector2 MidPet { get; }
        Vector2 LowPet { get; } 

    }
}
