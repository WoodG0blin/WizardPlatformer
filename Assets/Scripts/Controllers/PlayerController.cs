using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class PlayerController : Controller
    {
        private float _speed = 5f;
        private float _jumpForce = 5f;
        private float _moveThreshold = 0.02f;
        private float _input;
        private bool _doJump = false;
        private bool _doWalk = false;

        private ContactsPuller _contacts;

        public PlayerController(BasicView view) : base(view)
        {
            _contacts = new ContactsPuller(_view.collider);        
        }

        public override void Update()
        {
            _contacts.Update();

            _input = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space)) _doJump = true;

            _doWalk = Mathf.Abs(_input) > _moveThreshold;
            if (_doWalk && HasNoBarrier()) _view.rigidbody.velocity = new Vector2(_input * _speed, _view.rigidbody.velocity.y);

            //if (_doJump && _contacts.HasContactDown && Mathf.Abs(_view.rigidbody.velocity.y) < 0.02f)
            //{
            //    _view.rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
            //    _doJump = false;
            //}
            if (_doJump && _contacts.HasContactDown)
            {
                _view.rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
                _doJump = false;
            }
            
        }

        private bool HasNoBarrier() => (_input > 0 && !_contacts.HasContactRight) || (_input < 0 && !_contacts.HasContactLeft);

        public void SetPlayer(Vector3 position)
        {
            _view.transform.position = position;
        }
    }
}
