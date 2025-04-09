using System;
using System.Collections.Generic;
using UnityEngine;

namespace Canon.Bullets
{
    public class BulletsContext : MonoBehaviour
    {
        public static BulletsContext Active { get; private set; }

        public Bounds Bounds
        {
            get
            {
                Vector3 min = m_Camera.ViewportToWorldPoint(Vector3.zero);
                Vector3 max = m_Camera.ViewportToWorldPoint(Vector3.one);
                
                return new Bounds((min + max) * 0.5f, max - min);
            }
        }

        private          Camera       m_Camera;
        private readonly List<Bullet> m_Bullets = new();

        
        private void Awake()
        {
            if(Active != null)
                throw new Exception("BulletsWorld is already initialized");
            Active = this;
            
            m_Camera = Camera.main;
            if (m_Camera == null)
                throw new Exception("Camera is null");
        }
        private void Update()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < m_Bullets.Count; i++)
            {
                Bullet bullet = m_Bullets[i];
                bullet.Tick(deltaTime);

                if (!bullet.IsAlive)
                {
                    m_Bullets.RemoveAt(i);
                    Destroy(bullet.gameObject);
                    i--;
                }
            }
        }

        public T Spawn<T>(T bulletPrefab, BulletConfiguration configuration) where T : Bullet
        {
            T bullet = Instantiate(bulletPrefab, transform);
            bullet.Init(this, configuration);
            
            m_Bullets.Add(bullet);
            return bullet;
        }
    }
}