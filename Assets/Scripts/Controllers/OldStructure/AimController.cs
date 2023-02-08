using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

namespace WizardsPlatformer
{
    public class AimController : Controller
    {
        protected Transform _player;
        protected float _maxDistance;
        public AimController(BasicView view, BasicView player, float maxDistance = 0) : base(view) { _player = player.transform; _maxDistance = maxDistance; }
        protected override void OnUpdate()
        {
            if (InDistance())
            {
                Vector3 towards = _player.position - _view.transform.position;
                _view.transform.rotation = Quaternion.AngleAxis(Vector3.Angle(Vector3.up, towards), Vector3.Cross(Vector3.up, towards));
            }
        }

        protected override void OnDispose()
        {
            _player = null;
        }

        public Vector3 Aim() => _view.transform.up;

        public bool InDistance() => _maxDistance > 0 ? Vector3.Magnitude(_player.position - _view.transform.position) <= _maxDistance : true;
    }
}
