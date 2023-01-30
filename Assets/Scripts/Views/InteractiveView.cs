using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class InteractiveView : BasicView
    {
        public Action<BasicView> onTrigger;

        public virtual void Interact(Controller target) { }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            onTrigger?.Invoke(this);
        }
    }
}
