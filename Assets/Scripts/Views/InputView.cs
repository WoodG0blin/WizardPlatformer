using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace WizardsPlatformer
{
    public class InputView : MonoBehaviour
    {
        public event Action OnESC;

        public void OnSettingsClick() => OnESC?.Invoke();
    }
}
