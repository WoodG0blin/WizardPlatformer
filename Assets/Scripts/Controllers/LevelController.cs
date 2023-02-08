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
            
            var _player = new PlayerController(_levelModel);
            AddController(_player);
            AddController(new InputController(_levelModel));
            var _grounds = new GroundsController(100, _levelModel);
            AddController(_grounds);

            _player.SetPosition(_grounds.GetStartPosition());
            _levelModel.HorizontalMove.SubscribeOnValueChange(_player.OnHorizontalMove);
            _levelModel.Jump.SubscribeOnValueChange(_player.OnJump);

            AddController(new CameraController(_player.PlayerView));

            _levelModel.ESC.SubscribeOnValueChange((b) => { if (b) _gameModel.CurrentState.Value = GameState.MainMenu; });
        }
    }
}
