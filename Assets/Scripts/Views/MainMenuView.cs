using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

namespace WizardsPlatformer
{
    public class MainMenuView : View
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;

        public UnityAction OnStart { set => _startButton?.onClick.AddListener(value); }
        public UnityAction OnSettings { set => _settingsButton?.onClick.AddListener(value); }
    }
}
