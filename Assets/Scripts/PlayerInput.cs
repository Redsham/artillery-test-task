using System;
using System.Collections.Generic;
using Canon;
using Canon.Bullets;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_InputAsset;
    [SerializeField] private CanonBehaviour   m_Canon;
    [SerializeField] private float            m_MaxHoldTime = 1.0f;
    
    [Header("UI")]
    [SerializeField] private SwitchBehaviour m_BulletsSwitch;
    [SerializeField] private Bullet[] m_BulletsTypes;

    private InputAction m_PointAction;
    private InputAction m_TouchAction;
    
    private Camera m_Camera;
    private bool   m_IsHolding;
    private float  m_HoldTime;
    
    
    private void OnEnable()
    {
        m_InputAsset.FindActionMap("Player").Enable();

        m_PointAction = m_InputAsset["Player/Point"];
        m_TouchAction = m_InputAsset["Player/Touch"];
        
        m_TouchAction.started  += OnBeginHold;
        m_TouchAction.canceled += OnEndHold;
        
        m_BulletsSwitch.OnSwitch.AddListener(OnBulletTypeChanged);
    }
    private void OnDisable()
    {
        m_InputAsset.FindActionMap("Player").Disable();
        
        m_BulletsSwitch.OnSwitch.RemoveListener(OnBulletTypeChanged);
    }

    private void Awake()
    {
        if (m_InputAsset == null)
            throw new ArgumentNullException(nameof(m_InputAsset), "InputActionAsset cannot be null");
        
        if (m_Canon == null)
            throw new ArgumentNullException(nameof(m_Canon), "CanonBehaviour cannot be null");
        
        m_Camera = Camera.main;
        if (m_Camera == null)
            throw new Exception("Camera is null");
    }
    private void Update()
    {
        if (m_IsHolding)
        {
            Vector2 point      = m_PointAction.ReadValue<Vector2>();
            Vector3 worldPoint = m_Camera.ScreenToWorldPoint(new Vector3(point.x, point.y, 0.0f));
            m_Canon.AimPoint = worldPoint;
        }
    }
    
    private void OnBeginHold(InputAction.CallbackContext context)
    {
        Vector2             touchPosition = m_PointAction.ReadValue<Vector2>();
        PointerEventData    eventData     = new(EventSystem.current) {position = touchPosition};
        List<RaycastResult> results       = new();
        EventSystem.current.RaycastAll(eventData, results);
        
        if (results.Count > 0)
            return;
        
        m_IsHolding = true;
        m_HoldTime  = Time.time;
    }
    private void OnEndHold(InputAction.CallbackContext context)
    {
        if (!m_IsHolding)
            return;
        
        m_IsHolding = false;
        float holdTime = 0.1f + Mathf.Clamp01((Time.time - m_HoldTime) / m_MaxHoldTime) * 0.9f;
        m_Canon.Fire(holdTime);
    }
    
    private void OnBulletTypeChanged(bool isOn)
    {
        m_Canon.BulletPrefab = m_BulletsTypes[isOn ? 1 : 0];
    }
}