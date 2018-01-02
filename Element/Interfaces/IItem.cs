﻿using System;
using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IItem
    {
        SpriteSheet SpriteSheet { get; }
        string Name { get; }
        string ItemID { get; }
        Guid Guid { get; }
        Vector2 Position { get; set; }
        Rectangle BoundingBox { get; }
        IOwner Owner { get; set; }
        float Width { get; }
        float Height { get; }

        void Pickup(IOwner owner);
        void Drop(Vector2 position);

        string PopupIcon { get; }
        string ItemIcon { get; }
    }
}
