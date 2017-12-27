using System;

namespace Element.Interfaces
{
    public interface IItem : IDraw
    {
        string Name { get; }
        Guid Guid { get; }
    }
}
