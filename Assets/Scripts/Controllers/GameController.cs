using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class GameController : Controller
    {
        private readonly Transform _UIContainer;
        private readonly GameModel _gameModel;

        private Controller _activeController;

        public GameController(Transform UIContainer, GameModel gameModel)
        {
            _UIContainer = UIContainer;
            _gameModel = gameModel;

            _gameModel.CurrentState.SubscribeOnValueChange(OnChangeGameState);
            OnChangeGameState(_gameModel.CurrentState.Value);
        }

        private void OnChangeGameState(GameState newState)
        {
            _activeController?.Dispose();

            switch(newState)
            {
                case GameState.Game: _activeController = new LevelController(_gameModel); break;
                case GameState.MainMenu: _activeController = new MainMenuController(_UIContainer, _gameModel); break;
                case GameState.Settings: _activeController = new SettingsController(_UIContainer, _gameModel); break;
                default: break;
            }
        }

        protected override void OnDispose()
        {
            _gameModel.CurrentState.UnsubscribeOnValueChange(OnChangeGameState);
        }
    }
}
