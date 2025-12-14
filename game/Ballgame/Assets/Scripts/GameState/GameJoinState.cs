using System.Collections;
using JUtils;
using Sockets;
using UI;
using UnityEngine;

namespace GameState {
    public class GameJoinState : State {
        [SerializeField] private GameSessionController gameSessionController;
        [SerializeField] private GameJoinScreen gameJoinScreen;
        [SerializeField] private BallSpawner ballSpawner;
        [SerializeField] private int initialPerOption = 20;
        [SerializeField] private int spawnPerChoice = 5;

        private new GameStateMachine stateMachine => base.stateMachine as GameStateMachine;
        
        protected override void OnActivate() {
            gameJoinScreen.Show();
            gameJoinScreen.SubmitPressed += HandleSubmitPressed;
            gameSessionController.CodeReceived.AddListener(HandleCodeReceived);
            gameSessionController.PlayerAdded.AddListener(HandlePlayerAdded);
            StartCoroutine(ConnectRoutine());
            
            foreach (GameOptionDto gameOption in stateMachine.GameOptions) {
                for (int i = 0; i < initialPerOption; i++) {
                    ballSpawner.Spawn(gameOption);
                }
            }
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
            GameOptionDto gameOption = stateMachine.GameOptions.Find(it => it.name == gamePlayer.option);
            if (gameOption == null) {
                Debug.LogError("Player spawned with unkown option");
                return;
            }
            for (int i = 0; i < spawnPerChoice; i++) {
                ballSpawner.Spawn(gameOption);
            }
        }

        private void HandleSubmitPressed() {
            stateMachine.GoToState<GameStartState>();
        }

        private IEnumerator ConnectRoutine() {
            Debug.Log("Connecting to server");
            yield return gameSessionController.Connect();
            Debug.Log("Connected to server");
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