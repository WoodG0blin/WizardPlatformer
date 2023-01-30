using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Pool
    {
        private Stack<BasicView> _views = new Stack<BasicView>();
        private GameObject _prefab;
        private int _maxCount;

        public Pool(GameObject prefab, int count)
        {
            _prefab = prefab;
            _maxCount = count;

            for (int i = 0; i < _maxCount; i++)
            {
                _views.Push(GameObject.Instantiate(_prefab).GetComponent<BasicView>());
                _views.Peek().gameObject.SetActive(false);
            }
        }

        public BasicView GetAt(Vector3 position)
        {
            if (_views.Count == 0)
            {
                _views.Push(GameObject.Instantiate(_prefab).GetComponent<BasicView>());
                _views.Peek().gameObject.SetActive(false);
            }
            BasicView temp = _views.Pop();
            temp.transform.position = position;
            temp.gameObject.SetActive(true);
            if (temp is InteractiveView iView) iView.onTrigger += Return;
            return temp;
        }

        public void Return(BasicView view)
        {
            if(view is InteractiveView iView) iView.onTrigger -= Return;

            if (_views.Count < _maxCount)
            {
                _views.Push(view);
                _views.Peek().gameObject.SetActive(false);
            }
            else GameObject.Destroy(view.gameObject);
        }
    }
}
