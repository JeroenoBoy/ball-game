using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

namespace Sockets {
    public class GameSessionController : MonoBehaviour {
        public UnityEvent<GamePlayerDto> PlayerAdded { get; private set; } = new();
        public UnityEvent<string> CodeReceived { get; private set; } = new();

        private IGameSessionProvider provider;

        public string GetCode() {
            return provider.GetCode();
        }

        public Coroutine GetQrCode(Reference<Sprite> spriteRef) {
            return StartCoroutine(FetchCodeRoutine(spriteRef));
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

        private IEnumerator FetchCodeRoutine(Reference<Sprite> spriteRef) {
            Reference<Texture2D> textureRef = new();
            yield return provider.GetQrCode(textureRef);
            if (!textureRef.IsSet()) {
                throw new Exception("No texture was set");
            }

            Texture2D texture = textureRef.Get();
            spriteRef.Set(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * .5f, 100));
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