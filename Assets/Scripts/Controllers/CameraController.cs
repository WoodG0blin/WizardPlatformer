using UnityEngine;
using UnityEngine.UIElements;

namespace WizardsPlatformer
{
    public class CameraController : Controller
    {
        private Transform _camera;
        private BasicView _player;
        private BackGroundManager _backGround;

        private float _offset = 0.5f;
        private float _offsetThreshold = 2f;
        private float _speed = 10f;

        private float _targetX;
        private float _targetY;

        private Vector3 _oldPosition;

        public CameraController(BasicView player, Transform[] backgrounds)
        {
            _camera = Camera.main.transform;
            _player = player;

            _backGround = new BackGroundManager(_camera, backgrounds);
        }

        public override void Update()
        {
            _targetX = _player.transform.position.x;
            if (Mathf.Abs(_player.rigidbody.velocity.x) > _offsetThreshold) _targetX += _offset * Mathf.Sign(_player.rigidbody.velocity.x);

            _targetY = _player.transform.position.y;
            if (Mathf.Abs(_player.rigidbody.velocity.y) > _offsetThreshold) _targetY += _offset * Mathf.Sign(_player.rigidbody.velocity.y);
            _targetY = Mathf.Clamp(_targetY, -3.5f, 3.5f);

            _oldPosition = _camera.transform.position;
            _camera.position = Vector3.Lerp(_camera.position, new Vector3(_targetX, _targetY, _camera.position.z), Time.deltaTime * _speed);

            _backGround.Update(_camera.position - _oldPosition);
        }

        public override void Dispose()
        {
            _camera = null;
            _player = null;
        }
    }
}
