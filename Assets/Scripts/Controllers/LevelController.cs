using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using JoostenProductions;

namespace WizardsPlatformer
{
    public class LevelController : Controller
    {
        private readonly GameModel _gameModel;

        private LevelModel _levelModel;

        public LevelController(GameModel gameModel)
        {
            _gameModel = gameModel;
            _levelModel = new LevelModel();
            Init();
            UpdateManager.SubscribeToUpdate(Update);
        }

        private void Init()
        {
            Debug.Log("StartingLevel");

            var _grounds = new GroundsController(100, _levelModel);
            AddController(_grounds);
            AddController(new InputController(_levelModel));
            AddController(new PlayerController(_levelModel, _grounds.GetStartPosition()));
            AddController(new CameraController(_levelModel));

            _levelModel.ESC.SubscribeOnValueChange(OnEsc);
        }

        private void OnEsc(bool esc)
        {
            if (esc) _gameModel.CurrentState.Value = GameState.MainMenu;
        }
        protected override void OnDispose()
        {
            _levelModel.ESC.UnsubscribeOnValueChange(OnEsc);
        }
    }
}
