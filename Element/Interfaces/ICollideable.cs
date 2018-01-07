using Microsoft.Xna.Framework;

namespace Element.Interfaces
{
    public interface ICollideable
    {
        Rectangle BoundingBox { get; }
    }
}
