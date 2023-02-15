using UnityEngine;
using UnityEngine.UIElements;

namespace WizardsPlatformer
{
    public class CameraController : Controller
    {
        private Transform _camera;
        private LevelModel _level;
        private BackGroundManager _backGround;

        private float _offset = 0.5f;
        private float _offsetThreshold = 2f;
        private float _speed = 10f;

        private float _targetX;
        private float _targetY;

        private Vector3 _oldPosition;

        public CameraController(LevelModel level)
        {
            _camera = Camera.main.transform;
            _level = level;

            _level.PlayerPosition.SubscribeOnValueChange(OnPlayerPositionChange);

            _backGround = new BackGroundManager(_camera, _camera.GetComponent<CameraView>().backGrounds);
        }

        protected override void OnUpdate()
        {
            //_targetX = _player.transform.position.x;
            //if (Mathf.Abs(_player.rigidbody.velocity.x) > _offsetThreshold) _targetX += _offset * Mathf.Sign(_player.rigidbody.velocity.x);

            //_targetY = _player.transform.position.y;
            //if (Mathf.Abs(_player.rigidbody.velocity.y) > _offsetThreshold) _targetY += _offset * Mathf.Sign(_player.rigidbody.velocity.y);
            //_targetY = Mathf.Clamp(_targetY, -3.5f, 3.5f);

            Vector3 position = _camera.position;

            _oldPosition = position;
            position = Vector3.Lerp(position, new Vector3(_targetX, _targetY, position.z), Time.deltaTime * _speed);
            _camera.position = position;

            _backGround.Update(position - _oldPosition);
        }

        protected override void OnDispose()
        {
            _camera = null;
            _level.PlayerPosition.UnsubscribeOnValueChange(OnPlayerPositionChange);
        }

        private void OnPlayerPositionChange(Vector3 newPosition)
        {
            _targetX = newPosition.x;
            _targetY = Mathf.Clamp(newPosition.y, -3.5f, 3.5f);


        }
    }
}
