using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class GameModel
    {
        public SubscribtableProperty<GameState> CurrentState { get; }

        public GameModel(GameState initialState)
        {
            CurrentState = new SubscribtableProperty<GameState>();
            CurrentState.Value = initialState;
        }
    }
}
