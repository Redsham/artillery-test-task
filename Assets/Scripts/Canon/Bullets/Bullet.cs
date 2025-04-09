using UnityEngine;

namespace Canon.Bullets
{
    public abstract class Bullet : MonoBehaviour
    {
        public         Vector2        Position { get; protected set; }
        public         Vector2        Velocity { get; protected set; }
        public         float          LifeTime { get; protected set; }
        public         BulletsContext Context  { get; private set; }
        public virtual bool IsAlive => LifeTime > 0.0f;

        
        public virtual void Init(BulletsContext context, BulletConfiguration configuration)
        {
            Context = context;
            
            Position = configuration.InitialPosition;
            Velocity = configuration.InitialVelocity;
            LifeTime = configuration.InitialTime;
            
            Sync();
        }

        public virtual void Tick(float deltaTime)
        {
            LifeTime -= deltaTime;
            if (LifeTime <= 0)
            {
                Destroy(gameObject);
                return;
            }
            
            Position += Velocity * deltaTime;
        }
        public virtual  void Sync() => transform.position = Position;
    }
}