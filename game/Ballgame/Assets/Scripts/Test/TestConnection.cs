using System;
using System.Collections;
using System.Collections.Generic;
using Sockets;
using UnityEngine;

namespace Test {
    public class TestConnection : MonoBehaviour {
        private GameSessionController controller;

        private void Awake() {
            controller = GetComponent<GameSessionController>();
            controller.PlayerAdded.AddListener(HandlePlayerAdded);
            controller.CodeReceived.AddListener(HandleCodeReceivedDto);
        }

        private void Start() {
            StartCoroutine(Connect());
        }
        
        private IEnumerator Connect() {
            Debug.Log("Starting Connection");
            yield return controller.Connect();
            Debug.Log("Controller successfully connected");
            controller.SetOptions(new List<GameOptionDto>() {
                new ("A", ColorUtility.ToHtmlStringRGB(Color.HSVToRGB(1f / 4f, 0.5f, 0.5f))),
                new ("B", ColorUtility.ToHtmlStringRGB(Color.HSVToRGB(2f / 4f, 0.5f, 0.5f))),
                new ("C", ColorUtility.ToHtmlStringRGB(Color.HSVToRGB(3f / 4f, 0.5f, 0.5f))),
                new ("D", ColorUtility.ToHtmlStringRGB(Color.HSVToRGB(4f / 4f, 0.5f, 0.5f)))
            });
        }

        private void Disconnect() {
            controller.Disconnect();
            Debug.Log("Controller disconnected");
        }

        private void HandlePlayerAdded(GamePlayerDto dto) {
            Debug.Log($"Player {dto.user} picked {dto.option}");
        }

        private void HandleCodeReceivedDto(string code) {
            
        }
    }
}