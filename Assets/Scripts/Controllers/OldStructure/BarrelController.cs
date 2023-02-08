using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class BarrelController : Controller
    {
        private Pool _bullets;
        private float _coolDown = 2.0f;
        private bool _ready;

        private float _force = 1.2f;
        private float _counter = 0;

        public BarrelController(BasicView barrelView, Pool bullets) : base(barrelView) { _bullets = bullets; }

        protected override void OnUpdate()
        {
            if (!_ready)
            {
                _counter += Time.deltaTime;

                if (_counter > _coolDown)
                {
                    _ready = true;
                    _counter = 0;
                }
            }
        }

        public void Fire(Vector3 direction)
        {
            if(_ready)
            {
                BasicView bullet = _bullets.GetAt(_view.transform.position);
                bullet.rigidbody.AddForce(direction * _force, ForceMode2D.Impulse);
                _ready = false;
            }
        }
    }
}
