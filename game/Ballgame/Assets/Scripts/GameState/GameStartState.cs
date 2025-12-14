using System;
using System.Collections;
using JUtils;
using UI;
using UnityEngine;

namespace GameState {
    public class GameStartState : State {
        [SerializeField] private CountdownScreen countdownScreen;
        [SerializeField] private GameObject startingCage;
        [SerializeField] private float timeBetweenCd = 0.5f;
        
        protected override void OnActivate() {
            countdownScreen.Show();
            StartCoroutine(StartGameRoutine());
        }
        
        protected override void OnDeactivate() {
            countdownScreen.Hide();
        }

        private IEnumerator StartGameRoutine() {
            countdownScreen.SetCountdown(3);
            yield return new WaitForSeconds(timeBetweenCd);
            countdownScreen.SetCountdown(2);
            yield return new WaitForSeconds(timeBetweenCd);
            countdownScreen.SetCountdown(1);
            yield return new WaitForSeconds(timeBetweenCd);
            countdownScreen.SetCountdown(0);
            startingCage.SetActive(false);
            stateMachine.GoToState<GamePlayState>();
        }
    }
}