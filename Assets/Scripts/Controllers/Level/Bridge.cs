using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Bridge : Joint
    {
        public Bridge(Vector2Int start, Vector2Int end) : base("RopeBridge", start, end) { }

        public override void Draw(Vector2 _screenOffset)
        {
            _view.transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(_end - _start, new Vector2(1, 0)), Vector3.back);
            base.Draw(_screenOffset + new Vector2(0, 0.65f));
        }
    }
}
