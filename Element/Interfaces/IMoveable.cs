using Microsoft.Xna.Framework;

namespace Element.Interfaces
{
    public interface IMoveable : IUpdate
    {
        int Width { get; }
        int Height { get; }
        float Acceleration { get; }
        float Velocity { get; }
        Vector2 Position { get; } // bottom right corner of the player's movement box
        Rectangle MoveConstraint { get; }
        Rectangle BoundingBox { get; }
    }
}
