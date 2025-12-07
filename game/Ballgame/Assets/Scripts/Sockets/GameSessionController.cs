using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sockets {
    public class GameSessionController : MonoBehaviour {
        public UnityEvent<GamePlayerDto> PlayerAdded { get; private set; } = new();
        public UnityEvent<string> CodeReceived { get; private set; } = new();

        private IGameSessionProvider provider;

        public string GetCode() {
            return provider.GetCode();
        }

        public void SetTitle(string name) {
            provider.SetTitle(name);
        }

        public void SetOptions(IList<GameOptionDto> gameOption) {
            provider.SetOptions(gameOption);
        }

        public Coroutine Connect() {
            if (provider != null) { return null; }
            return StartCoroutine(CreateProvider());
        } 

        public void Disconnect() {
            if (provider == null) { return; }
            DestroyProvider();
        }

        private void OnDestroy() {
            Disconnect();
        }

        private IEnumerator CreateProvider() {
            provider = MakeProvider();
            provider.CodeReceived += EmitCodeReceived;
            provider.PlayerAdded += EmitPlayerAdded;
            yield return provider.Connect();
        }

        private void DestroyProvider() {
            provider.CodeReceived -= EmitCodeReceived;
            provider.PlayerAdded -= EmitPlayerAdded;
            IGameSessionProvider prov = provider;
            provider = null;
            prov.Disconnect();
        }

        private void EmitCodeReceived(string code) {
            CodeReceived.Invoke(code);
        }

        private void EmitPlayerAdded(GamePlayerDto dto) {
            PlayerAdded.Invoke(dto);
        }

        private IGameSessionProvider MakeProvider() {
            ConnectionConfig config = ConnectionConfig.GetConfig();
#if UNITY_EDITOR
            if (config.useMockServer) {
                return new MockGameSessionProvider();
            }
#endif
            Uri uri = new(config.ConnectionUrl);
            return new ServerGameSessionProvider(uri);
        }
    }
}