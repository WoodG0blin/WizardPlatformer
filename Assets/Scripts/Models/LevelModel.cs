using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class LevelModel
    {
        public SubscribtableProperty<float> HorizontalMove { get; }
        public SubscriptableTrigger Jump { get; }
        public SubscriptableTrigger Fire { get; }
        public SubscriptableTrigger ESC { get; }

        public LevelModel()
        {
            HorizontalMove = new SubscribtableProperty<float>();
            Jump = new SubscriptableTrigger();
            Fire = new SubscriptableTrigger();
            ESC = new SubscriptableTrigger();
        }
    }
}
