using JUtils;
using Sockets;
using UI;
using UnityEngine;

namespace GameState {
    public class GameOptionsState : State {

        [SerializeField] private OptionsScreen optionsScreen;
        private new GameStateMachine stateMachine => base.stateMachine as GameStateMachine;
        
        protected override void OnActivate() {
            optionsScreen.Show();
            optionsScreen.SubmitPressed += HandleScreenSubmitted;
        }
        
        protected override void OnDeactivate() {
            optionsScreen.Hide();
            optionsScreen.SubmitPressed -= HandleScreenSubmitted;
        }

        private void HandleScreenSubmitted() {
            stateMachine.GameOptions.Clear();
            foreach (GameOptionWidget optionsScreenOptionWidget in optionsScreen.OptionWidgets) {
                stateMachine.GameOptions.Add(new GameOptionDto(
                    optionsScreenOptionWidget.OptionName, 
                    "#" + ColorUtility.ToHtmlStringRGB(optionsScreenOptionWidget.OptionColor)
                ));
            }

            stateMachine.Title = optionsScreen.Title;
            stateMachine.GoToState<GameJoinState>();
        }
    }
}