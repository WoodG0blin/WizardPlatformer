using JoostenProductions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace WizardsPlatformer
{
    public class InputController : Controller
    {
        private readonly LevelModel _levelModel;
        private readonly string _assetPath = "UI/InputJoystick"; //TODO: replace with SO config

        private InputView input;

        public InputController(LevelModel levelModel)
        {
            _levelModel = levelModel;
            UpdateManager.SubscribeToUpdate(CheckInput);

            GameObject temp = GameObject.Instantiate(ResourceLoader.LoadPrefab(_assetPath));
            input = temp.GetComponent<InputView>() ?? temp.AddComponent<InputView>();

            input.OnESC += OnSettings;
        }

        private void CheckInput()
        {
            //TODO insert different input methods
            _levelModel.HorizontalMove.Value = CrossPlatformInputManager.GetAxis("Horizontal");
            if (CrossPlatformInputManager.GetButtonDown("Jump")) _levelModel.Jump.Value = true;
#if !MOBILE_INPUT
            if(Input.GetKeyDown(KeyCode.Escape)) _levelModel.ESC.Value = true;
#endif
        }

        private void OnSettings() => _levelModel.ESC.Value = true; 

        protected override void OnDispose()
        {
            UpdateManager.UnsubscribeFromUpdate(Update);
            input.OnESC -= OnSettings;
            GameObject.Destroy(input.gameObject);
        }
    }
}
