using JoostenProductions;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace WizardsPlatformer
{
    public class PlayerController : Controller
    {
        private readonly LevelModel _levelModel;
        private readonly string _assetPath = "Player"; //TODO: replace with SO config

        private float _speed = 3f;
        private float _jumpForce = 5f;
        private float _moveThreshold = 0.02f;
        private float _input = 0f;
        private bool _doJump = false;
        private bool _doWalk = false;

        private ContactsPuller _contacts;

        private PlayerView playerView;

        public BasicView PlayerView { get => playerView; }

        public PlayerController(LevelModel levelModel)
        {
            _levelModel = levelModel;

            GameObject temp = GameObject.Instantiate(ResourceLoader.LoadPrefab(_assetPath));
            playerView = temp.GetComponent<PlayerView>() ?? temp.AddComponent<PlayerView>();

            _levelModel.HorizontalMove.SubscribeOnValueChange(OnHorizontalMove);
            _levelModel.Jump.SubscribeOnValueChange(OnJump);

            _contacts = new ContactsPuller(playerView.collider);
        }

        public PlayerController(LevelModel level, Vector3 startPosition) : this(level)
        {
            SetPosition(startPosition);
        }

        private void Move()
        {
            _contacts.Update();

            _doWalk = Mathf.Abs(_input) > _moveThreshold;
            if (_doWalk && HasNoBarrier()) playerView.SetVelocity(_input * _speed);

            if (_doJump && _contacts.HasContactDown)
            {
                playerView.Jump(_jumpForce);
                _doJump = false;
            }
            _levelModel.PlayerPosition.Value = playerView.Position;
        }

        private bool HasNoBarrier() => (_input > 0 && !_contacts.HasContactRight) || (_input < 0 && !_contacts.HasContactLeft);

        public void OnHorizontalMove(float newValue)
        {
            _input = newValue;
            Move();
        }
        public void OnJump(bool jump)
        {
            _doJump = jump;
            Move();
        }

        public void SetPosition(Vector3 position) { playerView.transform.position = position; }

        protected override void OnDispose()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
            _levelModel.HorizontalMove.UnsubscribeOnValueChange(OnHorizontalMove);
            _levelModel.Jump.UnsubscribeOnValueChange(OnJump);
            GameObject.Destroy(PlayerView.gameObject);
        }
    }
}
