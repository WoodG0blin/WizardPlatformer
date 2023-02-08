using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Joint : Controller
    {
        protected Vector2 _start;
        protected Vector2 _end;
        protected Vector3 _screenPosition;

        public Joint(string name, Vector2Int start, Vector2Int end)
        {
            _view = GameObject.Instantiate(Resources.Load<GameObject>(name)).GetComponent<BasicView>();
            _view.gameObject.SetActive(false);
            _start = start;
            _end = end;
        }


        public virtual void Draw(Vector2 _screenOffset)
        {
            _view.gameObject.SetActive(true);
            _view.transform.position = new Vector3((_start.x + _end.x) / 2 + _screenOffset.x, (_start.y + _end.y) / 2 + _screenOffset.y, 0);
        }
    }
}
