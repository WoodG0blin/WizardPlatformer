using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public abstract class LevelElement
    {
        protected int _length;
        public int Length { get => _length; }
    }
}
