using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class JumpPlatform : Joint
    {
        public JumpPlatform(Vector2Int start, Vector2Int end) : base("JumpPlatform", start, end) { }
    }
}
