using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public abstract class Controller : IDisposable
    {
        private List<Controller> _childControllers;
        private List<View> _views;
        private bool _disposed;

        protected BasicView _view;

        public Controller(BasicView view) { _view = view; }
        public Controller() { }

        public void Update()
        {
            if(_childControllers != null) foreach(Controller controller in _childControllers) controller.Update();
            OnUpdate();
        }
        protected virtual void OnUpdate() { }

        public void Dispose()
        {
            if(_disposed) return;
            _disposed = true;

            if (_view != null)
            {
                GameObject.Destroy(_view.gameObject);
                _view = null;
            }

            if (_childControllers != null)
            {
                foreach (Controller controller in _childControllers) controller.Dispose();
                _childControllers.Clear();
            }

            if (_views != null)
            {
                foreach (View view in _views) GameObject.Destroy(view.gameObject);
                _views.Clear();
            }

            OnDispose();
        }
        protected virtual void OnDispose() { }

        protected void AddController(Controller controller)
        {
            _childControllers ??= new List<Controller>();
            _childControllers.Add(controller);
        }

        protected void AddView(View view)
        {
            _views??= new List<View>();
            _views.Add(view);
        }
    }
}
