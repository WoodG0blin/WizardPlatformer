using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Enemy : LevelObject
    {
        private BasicView _target;
        private Vector3 _patrolPoint;

        private float _speed = 2.0f;
        private float _sightDistance = 3.0f;
        private float _patrolDistance;
        private ContactsPuller _contacts;
        private float _goingDirection { get => Mathf.Sign(_view.transform.localScale.x); }
        private bool _inPatrol { get => (_view.transform.position - _patrolPoint).magnitude < _patrolDistance || _goingDirection == Mathf.Sign((_patrolPoint - _view.transform.position).x); }
        private bool _pursuing { get => (_goingDirection == Mathf.Sign((_target.transform.position - _view.transform.position).x) && (_target.transform.position - _view.transform.position).magnitude < _sightDistance && Mathf.Abs(Vector3.Dot(_target.transform.position - _view.transform.position, _view.transform.right)) > 0.8f); }
        private LayerMask _layerMask;
        private float _velocityX;
        private float _closingDistance = 1.2f;

        public Enemy(Vector2Int gridPosition, float patrolDistance = 3.0f) : base("Demon", gridPosition + new Vector2Int(0,1))
        {
            _target = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicView>();
            _patrolDistance = patrolDistance;

            _contacts = new ContactsPuller(_view.collider);
            _layerMask = LayerMask.GetMask("Background");

            _view.Animated = true;
        }

        public override void Update()
        {
            _contacts.Update();

            if (!IsPathClear()) _view.transform.localScale = new Vector3(_view.transform.localScale.x * -1, 1, 1);
            _view.rigidbody.velocity = new Vector2(_view.transform.localScale.x * _velocityX * _speed, 0);

            _view.animationState = _velocityX < 0.1f ? ActionState.Idle : ActionState.Walk;
        }

        public override void Draw(Vector2 _screenOffset)
        {
            base.Draw(_screenOffset);
            _patrolPoint = _view.transform.position;
        }

        private bool IsPathClear()
        {
            if (_pursuing || _inPatrol )
            {
                if (noGap())
                {
                    if ((_goingDirection <0 && !_contacts.HasContactLeft) || (_goingDirection >0 && !_contacts.HasContactRight))
                    {
                        if (_pursuing) _velocityX = Mathf.Abs(_target.transform.position.x - _view.transform.position.x) > _closingDistance ? 1 : 0;
                        else _velocityX = 1;
                        return true;
                    }
                    else if (_pursuing)
                    {
                        _velocityX = 0;
                        return true;
                    }
                }
            }
            _velocityX = 1;
            return false;
        }

        private bool noGap()
        {
            RaycastHit2D hit = Physics2D.Raycast(
                        new Vector2(_view.transform.position.x, _view.transform.position.y),
                        new Vector2(_view.transform.localScale.x, -1),
                        0.75f, _layerMask);
            if (hit.collider != null) return true;
            return false;
        }
    }
}
