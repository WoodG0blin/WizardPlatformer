using JoostenProductions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class InputController : Controller
    {
        private readonly LevelModel _levelModel;

        public InputController(LevelModel levelModel)
        {
            _levelModel = levelModel;
            UpdateManager.SubscribeToUpdate(CheckInput);
        }

        private void CheckInput()
        {
            //TODO insert different input methods
            _levelModel.HorizontalMove.Value = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space)) _levelModel.Jump.Value = true;
            if(Input.GetKeyDown(KeyCode.Escape)) _levelModel.ESC.Value = true;
        }

        protected override void OnDispose()
        {
            UpdateManager.UnsubscribeFromUpdate(Update);
        }
    }
}
