using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

namespace WizardsPlatformer
{
    public class BallisticAimController : AimController
    {
        //TODO: redo calculations of speed (from bullet and barrel params), redo hardcode in minAngle, get G from bullet params



        private int _angle = 10;
        private float _speed = 12f;
        public BallisticAimController(BasicView view, BasicView player, float maxDistance = 0) : base(view, player, maxDistance) {}
        protected override void OnUpdate()
        {
            _angle = 0;

            if(InDistance())
            {
                for (int i = 89; i > 20; i--)
                {
                    float dx = Mathf.Abs(_player.position.x - _view.transform.position.x);
                    float dy = (Mathf.Tan(i / 180f * Mathf.PI) * dx) - ((2f * 9.8f * dx * dx) / (2 * _speed * _speed * Mathf.Cos(i / 180f * Mathf.PI) * Mathf.Cos(i / 180f * Mathf.PI)));

                    if (Mathf.Abs(dy + _view.transform.position.y - _player.position.y) < 0.2f)
                    {
                        _angle = (90 - i) * (int)Mathf.Sign(_player.position.x - _view.transform.position.x);
                        break;
                    }
                }
            }

            _view.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.back);
        }

        protected override void OnDispose()
        {
            _player = null;
        }
    }
}
