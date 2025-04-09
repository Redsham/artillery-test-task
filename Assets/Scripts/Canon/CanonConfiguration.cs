using UnityEngine;

namespace Canon
{
    [System.Serializable]
    public class CanonConfiguration
    {
        public Vector2 CanonOrientation => m_CanonOrientation;
        public Transform     Barrel => m_Barrel;
        public CanonKickback Kickback => m_Kickback;
        public float BulletSpeed => m_BulletSpeed;
        public float BulletLifeTime => m_BulletLifeTime;
        
        [Header("Aiming")]
        [SerializeField] private Vector2       m_CanonOrientation = new(0.0f, 1.0f);
        [SerializeField] private Transform     m_Barrel;
        
        [Header("Kickback")]
        [SerializeField] private CanonKickback m_Kickback;
        
        [Header("Bullet")]
        [SerializeField] private float m_BulletSpeed = 10.0f;
        [SerializeField] private float m_BulletLifeTime = 5.0f;
    }
    
    [System.Serializable]
    public class CanonKickback
    {
        public float Force     => m_Force;
        public float Distance  => m_Distance;
        public float AntiForce => m_AntiForce;
        public float Drag      => m_Drag;

        
        [SerializeField] private float m_Force     = 2.5f;
        [SerializeField] private float m_Distance  = 0.5f;
        [SerializeField] private float m_AntiForce = 10.0f;
        [SerializeField] private float m_Drag      = 7.5f;
    }
}