using JoostenProductions;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace WizardsPlatformer
{
    public class PlayerController : Controller
    {
        private readonly LevelModel _levelModel;
        private readonly string _assetPath = "Player"; //TODO: replace with SO config

        private float _speed = 5f;
        private float _jumpForce = 5f;
        private float _moveThreshold = 0.02f;
        private float _input = 0f;
        private bool _doJump = false;
        private bool _doWalk = false;

        private ContactsPuller _contacts;

        public BasicView PlayerView { get => _view; }

        public PlayerController(LevelModel levelModel)
        {
            _levelModel = levelModel;

            GameObject temp = GameObject.Instantiate(ResourceLoader.LoadPrefab(_assetPath));
            _view = temp.GetComponent<BasicView>() ?? temp.AddComponent<BasicView>();

            _contacts = new ContactsPuller(_view.collider);
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void Move()
        {
            _contacts.Update();

            _doWalk = Mathf.Abs(_input) > _moveThreshold;
            if (_doWalk && HasNoBarrier()) _view.rigidbody.velocity = new Vector2(_input * _speed, _view.rigidbody.velocity.y);

            if (_doJump && _contacts.HasContactDown)
            {
                _view.rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
                _doJump = false;
            }
            
        }

        private bool HasNoBarrier() => (_input > 0 && !_contacts.HasContactRight) || (_input < 0 && !_contacts.HasContactLeft);

        public void OnHorizontalMove(float newValue) { _input = newValue; }
        public void OnJump(bool jump) { _doJump = jump; }

        public void SetPosition(Vector3 position) { _view.transform.position = position; }

        protected override void OnDispose()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }
    }
}
