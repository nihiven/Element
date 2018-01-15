using Element.Classes;
using Element.Interfaces;
using System;
using System.Collections.Generic;

namespace Element.Interfaces
{
    public interface IEntity : IDisposable
    {
        bool Expired { get; }
    }

    public interface IEntityManager
    {
        List<IEntity> Entities { get; }
        List<IUpdate> Updates { get; }
        List<IDraw> Draws { get; }

        void Add(IEntity entity);
        void Remove(IEntity entity);
    }
}

namespace Element.Classes
{
    public class EntityManager : IEntityManager
    {
        public List<IEntity> Entities => _entities;
        public List<IUpdate> Updates => _updates;
        public List<IDraw> Draws => _draws;

        private List<IEntity> _entities = new List<IEntity>();
        private List<IUpdate> _updates = new List<IUpdate>();
        private List<IDraw> _draws = new List<IDraw>();

        public void Add(IEntity entity)
        {
            _entities.Add(entity);

            if (entity is IUpdate)
                _updates.Add((IUpdate)entity);

            if (entity is IDraw)
                _draws.Add((IDraw)entity);
        }

        public void Remove(IEntity entity)
        {
            _entities.Remove(entity);

            if (entity is IUpdate)
                _updates.Remove((IUpdate)entity);

            if (entity is IDraw)
                _draws.Remove((IDraw)entity);

            // TODO: find out how this should be done
            entity.Dispose();
            entity = null; 
        }
    }

    /// <summary>
    /// weapons, enemies, powerups
    /// anything that is not a game component
    /// </summary>
    public static class EntityFactory
    {
        public static IEntity New(string name)
        {
            switch (name)
            {
                case Entities.PlayerBullet:
                    return new Bullet(
                        ObjectManager.Get<IContentManager>(ComponentStrings.ContentManager).GetAnimatedSprite("bullet"),
                        ObjectManager.Get<IActiveGear>(ComponentStrings.ActiveGear).Weapon.FirePosition,
                        ObjectManager.Get<IInput>(ComponentStrings.Input).RightThumbstickAngle,
                        ObjectManager.Get<IActiveGear>(ComponentStrings.ActiveGear).Weapon.BaseVelocity, // TODO: replace base
                        ObjectManager.Get<IActiveGear>(ComponentStrings.ActiveGear).Weapon.BaseRange // TODO: replace base
                    );
            }

            return null;
        }
    }
}