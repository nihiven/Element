using System;
using Microsoft.Xna.Framework;

namespace Element.Interfaces
{
    public interface IItem
    {
        AnimatedSprite AnimatedSprite { get; }
        string Name { get; }
        Guid Guid { get; }
        Vector2 Position { get; set; }
        Rectangle BoundingBox { get; }
    }
}
