using System;
using Canon.Bullets;
using UnityEngine;

namespace Canon
{
    [RequireComponent(typeof(CanonDrawer))]
    public class CanonBehaviour : MonoBehaviour
    {
        public CanonDrawer Drawer { get; private set; }
        public CanonConfiguration Configuration => m_Configuration;
        
        public Bullet BulletPrefab
        {
            get => m_BulletPrefab;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "BulletPrefab cannot be null");
                
                m_BulletPrefab = value;
            }
        }
        public Vector2 AimPoint
        {
            get => m_AimPoint;
            set
            {
                m_AimPoint = value;
                
                Vector2 orientation = Configuration.CanonOrientation;
                Vector2 direction   = value - (Vector2)transform.position;
                Rotation = Vector2.SignedAngle(orientation, direction);
                
                Drawer.SetRotation(Rotation);
            }
        }
        public float   Rotation { get; private set; }
        
        [SerializeField] private CanonConfiguration m_Configuration;
        [SerializeField] private Bullet m_BulletPrefab;

        private Vector2 m_AimPoint;
        

        private void Awake()
        {
            Drawer = GetComponent<CanonDrawer>();
            if(Drawer == null)
                throw new System.Exception("CanonDrawer is null");
            
            Drawer.Initialize(m_Configuration);
        }
        
        public void Fire(float force = 1.0f)
        {
            force = Mathf.Clamp01(force);
            Drawer.Fire(force);
            
            if (m_BulletPrefab == null)
                throw new ArgumentNullException(nameof(m_BulletPrefab), "BulletPrefab cannot be null");
            
            Vector2 barrelWorldPosition = Configuration.Barrel.position;
            Vector2 barrelDirection     = Configuration.Barrel.up;
            
            Bullet bullet = BulletsContext.Active.Spawn(m_BulletPrefab, new BulletConfiguration
            {
                InitialPosition = barrelWorldPosition,
                InitialVelocity = barrelDirection * (Configuration.BulletSpeed * force),
                InitialTime     = Configuration.BulletLifeTime
            });
        }
    }
}