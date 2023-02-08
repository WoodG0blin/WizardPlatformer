using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class CameraView : View
    {
        [SerializeField] private Transform[] _backGrounds;
        public Transform[] backGrounds { get => _backGrounds; }
    }
}
