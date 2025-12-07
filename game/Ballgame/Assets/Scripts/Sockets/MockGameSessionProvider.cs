using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sockets {
    public class MockGameSessionProvider : IGameSessionProvider {
        public event Action<GamePlayerDto> PlayerAdded;
        public event Action<string> CodeReceived;
        
        public string GetCode() {
            return "xyz-123";
        }

        public void SetTitle(string title) {
            Debug.Log($"[{GetType().Name}] : Set tile to ${title}");
        }
        
        public void SetOptions(IList<GameOptionDto> gameOptions) {
            Debug.Log($"[{GetType().Name}] : Changed Options");
        }

        public IEnumerator Connect() {
            Debug.Log($"[{GetType().Name}] : Connected");
            yield break;
        }

        public void Disconnect() {
            Debug.Log($"[{GetType().Name}] : Disconnected");
        }
    }
}