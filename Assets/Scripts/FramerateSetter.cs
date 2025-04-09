using System;
using UnityEngine;

public class FramerateSetter : MonoBehaviour
{
    [SerializeField] private int m_TargetFrameRate = 60;

    private void Awake() => Application.targetFrameRate = m_TargetFrameRate;
}