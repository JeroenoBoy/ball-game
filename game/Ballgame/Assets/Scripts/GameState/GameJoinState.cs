using System.Collections;
using JUtils;
using Sockets;
using UnityEngine;

namespace GameState {
    public class GameJoinState : State {
        [SerializeField] private GameSessionController gameSessionController;

        private new GameStateMachine stateMachine => base.stateMachine as GameStateMachine;
        
        protected override void OnActivate() {
            StartCoroutine(ConnectRoutine());
        }
        
        protected override void OnDeactivate() {
            gameSessionController.Disconnect();
        }

        private IEnumerator ConnectRoutine() {
            yield return gameSessionController.Connect();
            gameSessionController.SetTitle(stateMachine.Title);
            gameSessionController.SetOptions(stateMachine.GameOptions);
        }
    }
}