using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WizardsPlatformer
{
    public class BasicView : View
    {
        [SerializeField] private bool _animated = false;
        [SerializeField] private AnimationSequence[] animations;

        private SpriteRenderer _renderer;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;

        private AnimationController _animator;

        new public SpriteRenderer renderer
        {
            get
            {
                if (!_renderer)
                    if (!TryGetComponent<SpriteRenderer>(out _renderer)) _renderer = transform.AddComponent<SpriteRenderer>();
                return _renderer;
            }
            private set
            {
                _renderer = value;
            }
        }

        new public Rigidbody2D rigidbody
        {
            get
            {
                if(!_rigidbody)
                    if (!TryGetComponent<Rigidbody2D>(out _rigidbody)) _rigidbody = transform.AddComponent<Rigidbody2D>();
                return _rigidbody;
            }
            private set
            { 
                _rigidbody = value;
            }
        }
        new public Collider2D collider
        {
            get
            {
                if(!_collider)
                    if (!TryGetComponent<Collider2D>(out _collider)) _collider = transform.AddComponent<CircleCollider2D>();
                return _collider;
            }
            private set
            {
                _collider = value;
            }
        }

        public ActionState animationState
        {
            get => _animator.CurrentState;
            set
            {
                _animator.ChangeAnimationState(value);
            }
        }

        protected void ChangeAnimation(ActionState newAnimation, bool forceChange = false)
        {
            _animator.ChangeAnimationState(newAnimation, forceChange);
        }

        public bool Animated { get => _animated; set => _animated = value; }

        private void Awake()
        {
            if(_animated) _animator = new AnimationController(renderer, animations);
        }

        private void Update()
        {
            if(_animated) _animator.Update();
            OnUpdate();
        }

        protected virtual void OnUpdate() { }
    }
}
