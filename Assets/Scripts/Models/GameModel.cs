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
            CurrentState = new SubscribrablePropertyWithEqualsCheck <GameState>();
            CurrentState.Value = initialState;
        }
    }
}
