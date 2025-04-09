using System;
using UnityEngine;

namespace Canon
{
    public class CanonDrawer : MonoBehaviour
    {
        [SerializeField] private Transform m_CanonBody;
        
        private CanonConfiguration m_Configuration;
        
        private float m_KickbackPosition;
        private float m_KickbackVelocity;
        
        private bool m_IsInitialized;

        
        public void Initialize(CanonConfiguration configuration)
        {
            m_Configuration = configuration;
            m_IsInitialized = true;
        }
        private void Update() => TickPhysics(Time.deltaTime);
        
        private void TickPhysics(float deltaTime)
        {
            if(!m_IsInitialized)
                return;
            
            m_KickbackPosition += m_KickbackVelocity * deltaTime;
            m_KickbackVelocity -= m_Configuration.Kickback.AntiForce * deltaTime;
            m_KickbackVelocity =  Mathf.Lerp(m_KickbackVelocity, 0.0f, deltaTime * m_Configuration.Kickback.Drag);

            if (m_KickbackPosition > m_Configuration.Kickback.Distance)
            {
                m_KickbackPosition = m_Configuration.Kickback.Distance;
                m_KickbackVelocity = 0.0f;
            }
            else if (m_KickbackPosition < 0.0f)
            {
                m_KickbackPosition = 0.0f;
                m_KickbackVelocity = -m_KickbackVelocity * 0.25f;
            }
            
            Vector2 canonDirection = m_CanonBody.up;
            m_CanonBody.localPosition = -canonDirection * m_KickbackPosition;
        }
        
        public void Fire(float force = 1.0f)
        {
            if (!m_IsInitialized)
                throw new Exception("CanonDrawer is not initialized");
            
            m_KickbackVelocity += m_Configuration.Kickback.Force * force;
        }
        public void SetRotation(float angle)
        {
            if (!m_IsInitialized)
                throw new Exception("CanonDrawer is not initialized");
            
            m_CanonBody.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
