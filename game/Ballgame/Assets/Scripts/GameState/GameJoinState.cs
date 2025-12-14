using System.Collections;
using JUtils;
using Sockets;
using UI;
using UnityEngine;

namespace GameState {
    public class GameJoinState : State {
        [SerializeField] private GameSessionController gameSessionController;
        [SerializeField] private GameJoinScreen gameJoinScreen;

        private new GameStateMachine stateMachine => base.stateMachine as GameStateMachine;
        
        protected override void OnActivate() {
            gameJoinScreen.Show();
            gameJoinScreen.SubmitPressed += HandleSubmitPressed;
            gameSessionController.CodeReceived.AddListener(HandleCodeReceived);
            gameSessionController.PlayerAdded.AddListener(HandlePlayerAdded);
            StartCoroutine(ConnectRoutine());
        }
        
        protected override void OnDeactivate() {
            gameJoinScreen.Hide();
            gameJoinScreen.SubmitPressed -= HandleSubmitPressed;
            gameSessionController.CodeReceived.RemoveListener(HandleCodeReceived);
            gameSessionController.PlayerAdded.RemoveListener(HandlePlayerAdded);
            gameSessionController.Disconnect();
        }

        private void HandleCodeReceived(string code) {
            gameJoinScreen.SetCode(code);
            StartCoroutine(GatherQrCodeRoutine());
        }

        private void HandlePlayerAdded(GamePlayerDto gamePlayer) {
        }

        private void HandleSubmitPressed() {
            stateMachine.GoToState<GameStartState>();
        }

        private IEnumerator ConnectRoutine() {
            yield return gameSessionController.Connect();
            gameSessionController.SetTitle(stateMachine.Title);
            gameSessionController.SetOptions(stateMachine.GameOptions);
        }

        private IEnumerator GatherQrCodeRoutine() {
            Reference<Sprite> spriteRef = new();
            yield return gameSessionController.GetQrCode(spriteRef);
            gameJoinScreen.SetQrCode(spriteRef.Get());
        }
    }
}