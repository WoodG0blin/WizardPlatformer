using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WizardsPlatformer
{
    public class MainMenuController : Controller
    {
        private readonly Transform _UIContainer;
        private readonly GameModel _GameModel;
        private readonly string _assetPath = "UI/MainMenu"; //TODO: replace with SO config

        public MainMenuController(Transform uIContainer, GameModel gameModel)
        {
            _UIContainer = uIContainer;
            _GameModel = gameModel;

            GameObject temp = GameObject.Instantiate(ResourceLoader.LoadPrefab(_assetPath), _UIContainer);
            MainMenuView _menuView = temp.GetComponent<MainMenuView>() ?? temp.AddComponent<MainMenuView>();
            _menuView.OnStart = OnGameStart;
            _menuView.OnSettings = OnSettings;
            AddView(_menuView);
        }

        private void OnGameStart() => _GameModel.CurrentState.Value = GameState.Game;
        private void OnSettings() => _GameModel.CurrentState.Value = GameState.Settings;
    }
}
