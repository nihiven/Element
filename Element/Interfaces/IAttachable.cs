using Microsoft.Xna.Framework;

namespace Element.Interfaces
{
    interface IAttachable
    {
        // TODO: try this out, maybe this should be the center point?
        // top left attachment points
        Vector2 HighArmor { get; } // head 
        Vector2 MidArmor { get; } // chest
        Vector2 LowArmor { get; } // pants
        Vector2 HighWeapon { get; } // head
        Vector2 MidWeapon { get; } // shoulder
        Vector2 LowWeapon { get; } // low hands
        Vector2 HighPet { get; } // float above shoulder
        Vector2 MidPet { get; } // float along side
        Vector2 LowPet { get; } // float near ground

    }
}
