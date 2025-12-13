using System.Collections.Generic;
using JUtils;
using Sockets;

namespace GameState {
    public class GameStateMachine : StateMachine {

        public string Title { get; set; } = "";
        public List<GameOptionDto> GameOptions { get; private set; } = new();
        
        protected override void OnActivate() {
            GoToState<GameOptionsState>();
        }
        
        protected override void OnDeactivate() {
        }
        
        protected override void OnNoState() {
        }
    }
}