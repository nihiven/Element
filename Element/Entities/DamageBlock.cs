using Element.Classes;
using Element.Interfaces;
using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace Element.Entities 
{
    public class DamageBlock : IEntity, IDraw, IUpdate, ICollideable, IMoveable
    {
        public bool Expired => false;
        public Rectangle BoundingBox => throw new System.NotImplementedException();
        public int Width => throw new System.NotImplementedException();
        public int Height => throw new System.NotImplementedException();
        public float Acceleration => throw new System.NotImplementedException();
        public float Velocity => throw new System.NotImplementedException();
        public Vector2 Position => throw new System.NotImplementedException();
        public Rectangle MoveConstraint => _moveConstraint;

        private Rectangle _moveConstraint;

        public DamageBlock(IEntityManager entityManager)
        {

        }

        public void Dispose()
        {
        }

        public void Draw(SpriteRender spriteRender)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
