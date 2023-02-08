using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Portal : LevelObject
    {
        public Portal(Vector2Int gridPosition) : base("Portal", gridPosition)
        {
            _view.Animated = true;
            if(_view is InteractiveView iview) iview.onTrigger += OnTrigger;
        }

        private void OnTrigger(BasicView view)
        {
            Time.timeScale = 0;
            //TODO - endGame
        }
    }
}
