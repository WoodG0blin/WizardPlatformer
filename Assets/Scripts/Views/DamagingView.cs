using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class DamagingView : InteractiveView
    {
        [SerializeField] private float _damage = 2f;

        public override void Interact(Controller target)
        {
            Debug.Log($"Target {target} receives {_damage} damage points!");
        }
    }
}
