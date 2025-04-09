using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class SwitchBehaviour : MonoBehaviour
    {
        public UnityEvent<bool> OnSwitch => m_OnSwitch;
        
        [SerializeField] private TextMeshProUGUI m_Text;
        [SerializeField] private UnityEvent<bool> m_OnSwitch;
        
        [Header("Strings")]
        [SerializeField] private string m_OnString = "On";
        [SerializeField] private string m_OffString = "Off";

        private bool m_Switched;


        private void Start()
        {
            UpdateText();
        }

        public void Switch()
        {
            m_Switched = !m_Switched;
            OnSwitch?.Invoke(m_Switched);

            UpdateText();
        }
        private void UpdateText()
        {
            if (m_Text == null)
                return;

            m_Text.text = m_Switched ? m_OnString : m_OffString;
        }
    }
}