using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Lift : JumpPlatform
    {
        private float _minY;
        private float _maxY;
        private bool _goingUp = true;

        public Lift(Vector2Int start, Vector2Int end) : base(start, end)
        {
            _view.rigidbody.isKinematic = true;
        }

        protected override void OnUpdate()
        {
            _view.rigidbody.velocity = new Vector2(0, (_goingUp ? 1:-1));
            if(_goingUp && _view.transform.position.y > _maxY) _goingUp = false;
            if (!_goingUp && _view.transform.position.y < _minY) _goingUp = true;
        }

        public override void Draw(Vector2 _screenOffset)
        {
            base.Draw(_screenOffset);
            _minY = Mathf.Min(_start.y, _end.y) + _screenOffset.y;
            _maxY = Mathf.Max(_start.y, _end.y) + _screenOffset.y;
            _view.transform.position = new Vector3(_view.transform.position.x, _minY, 0);
        }
    }
}
