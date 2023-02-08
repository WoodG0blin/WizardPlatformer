using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WizardsPlatformer
{
    public class SettingsView : View
    {
        [SerializeField] private Button _backButton;

        public UnityAction OnReturn { set => _backButton.onClick.AddListener(value); }
    }
}
