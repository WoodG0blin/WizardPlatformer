using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Scarecrow : LevelObject
    {
        private InteractiveView _ammo;
        private float _coolDown1 = 0.5f;
        private float _coolDown2 = 2f;
        private float _coolDown2target = 2f;
        private bool _ready;

        private float _force = 1.2f;
        private float _counter = 0;

        public Scarecrow(Vector2Int _gridPosition) : base("ScareCrow", _gridPosition)
        {
            if(!_view.transform.Find("Ammo").TryGetComponent<InteractiveView>(out _ammo)) _ammo = _view.transform.Find("Ammo").gameObject.AddComponent<InteractiveView>();
            _ammo.onTrigger += SetReady;
            _ammo.rigidbody.gravityScale = 0;
            _ammo.transform.position = _view.transform.Find("Hand").position;
            _ready = true;
            _counter = Random.Range(0, _coolDown2-0.1f);
        }

        protected override void OnUpdate()
        {
            if (_ready)
            {
                _counter += Time.deltaTime;

                if(_counter > _coolDown1 && !_ammo.gameObject.activeSelf) _ammo.gameObject.SetActive(true);

                if (_counter > _coolDown2)
                {
                    _ready = false;
                    _ammo.rigidbody.gravityScale = 1;
                    _ammo.rigidbody.AddForce(Vector3.up * _force + Vector3.right * Random.Range(-0.2f, 0.2f), ForceMode2D.Impulse);
                    _ammo.rigidbody.AddTorque(_force * 10f, ForceMode2D.Impulse);
                    _counter = 0;
                    _coolDown2 = Random.Range(_coolDown2target - 1f, _coolDown2target + 1f);
                }
            }
        }

        private void SetReady(BasicView ammo)
        {
            _ammo.gameObject.SetActive(false);
            _ammo.rigidbody.gravityScale = 0;
            _ammo.rigidbody.velocity = Vector3.zero;
            _ammo.rigidbody.angularVelocity = 0;
            _ammo.transform.position = _view.transform.Find("Hand").position;
            _ammo.transform.rotation = Quaternion.identity;
            _ready = true;
        }
    }
}
