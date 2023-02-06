using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WizardsPlatformer
{
    public class LevelObject : Controller
    {
        protected Vector2Int _gridPosition;

        public LevelObject(string name, Vector2Int gridPosition)
        {
            var obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            if (!obj.TryGetComponent<BasicView>(out _view)) _view = obj.AddComponent<BasicView>();
            _view.Animated = false;
            _view.gameObject.SetActive(false);
            _gridPosition = gridPosition;
        }

        public override void Update()
        {
            
        }

        public virtual void Draw(Vector2 _screenOffset)
        {
            _view.gameObject.SetActive(true);
            _view.transform.position = new Vector3(_gridPosition.x + _screenOffset.x + 0.5f, _gridPosition.y + _screenOffset.y + 0.5f, 0);
        }
    }
}
