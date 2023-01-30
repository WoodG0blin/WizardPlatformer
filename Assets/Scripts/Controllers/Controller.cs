using System;

namespace WizardsPlatformer
{
    public abstract class Controller : IDisposable
    {
        protected BasicView _view;

        public Controller(BasicView view) { _view = view; }
        public Controller() { }

        public abstract void Update();

        public virtual void Dispose() { _view = null; }
    }
}
