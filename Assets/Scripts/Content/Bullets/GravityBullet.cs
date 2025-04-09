using Canon.Bullets;
using UnityEngine;

namespace Content.Bullets
{
    public class GravityBullet : Bullet
    {
        public override bool IsAlive => LifeTime > 0.0f && !m_IsColliding;
        private bool m_IsColliding;

        public override void Tick(float deltaTime)
        {
            Velocity += Physics2D.gravity * deltaTime;
            
            if (Position.x < Context.Bounds.min.x || Position.x > Context.Bounds.max.x || Position.y < Context.Bounds.min.y)
                m_IsColliding = true;
            
            base.Tick(deltaTime);
            Sync();
        }
    }
}