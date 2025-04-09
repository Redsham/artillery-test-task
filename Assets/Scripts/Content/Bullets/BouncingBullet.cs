
using Canon.Bullets;
using UnityEngine;

namespace Content.Bullets
{
    public class BouncingBullet : Bullet
    {
        public override void Tick(float deltaTime)
        {
            Vector2 velocity = Velocity;
            Vector2 position = Position;
            
            // Check for collision with the bounds of the screen
            if (Position.x < Context.Bounds.min.x || Position.x > Context.Bounds.max.x)
            {
                velocity.x = -velocity.x;
                position.x = Mathf.Clamp(position.x, Context.Bounds.min.x, Context.Bounds.max.x);
            }
            
            if (Position.y < Context.Bounds.min.y || Position.y > Context.Bounds.max.y)
            {
                velocity.y = -velocity.y;
                position.y = Mathf.Clamp(position.y, Context.Bounds.min.y, Context.Bounds.max.y);
            }
            
            Velocity = velocity;
            Position = position;
            
            base.Tick(deltaTime);
            Sync();
        }
    }
}