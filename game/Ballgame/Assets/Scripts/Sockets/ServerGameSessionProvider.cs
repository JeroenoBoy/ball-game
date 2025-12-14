using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using Sockets.Exceptions;
using UnityEngine;
using UnityEngine.Networking;

namespace Sockets {
    public class ServerGameSessionProvider : IGameSessionProvider {
        
        public event Action<GamePlayerDto> PlayerAdded;
        public event Action<string> CodeReceived;

        private string code;
        private Uri uri;

        private HashSet<string> joinedPlayes = new();
        private SocketIOUnity socket;

        public ServerGameSessionProvider(Uri uri) {
            this.uri = uri;
        }
        
        public IEnumerator Connect() {
            socket = new SocketIOUnity(uri);
            socket.JsonSerializer = new NewtonsoftJsonSerializer();
            socket.OnUnityThread("code", HandleCodeReceived);
            socket.OnUnityThread("playerJoin", HandlePlayerJoin);
            Task task = socket.ConnectAsync();
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) {
                throw task.Exception ?? new Exception("Task failed without additional data");
            }
            task.Dispose();
            yield return new WaitWhile(() => string.IsNullOrEmpty(code));
        }
        
        public void Disconnect() {
            socket.Disconnect();
        }
        
        public string GetCode() {
            if (string.IsNullOrEmpty(code)) {
                throw new CodeNotReceivedException();
            }
            return code;
        }

        public IEnumerator GetQrCode(Reference<Texture2D> textureRef) {
            if (code == null) {
                throw new Exception("No cod was set");
            }

            using UnityWebRequest request = UnityWebRequestTexture.GetTexture($"{uri}game/{code}/code");
            yield return request.SendWebRequest();
            if (request.error != null) {
                throw new Exception($"Error while gathering QR Code: {request.error}");
            }
            textureRef.Set(((DownloadHandlerTexture)request.downloadHandler).texture);
            textureRef.Get().filterMode = FilterMode.Point;
        }

        public void SetTitle(string title) {
            socket.Emit("title", title);
        }
        
        public void SetOptions(IList<GameOptionDto> gameOptions) {
            socket.Emit("options", gameOptions);
        }

        private void HandleCodeReceived(SocketIOResponse response) {
            code = response.GetValue<string>();
            CodeReceived?.Invoke(code);
        }

        private void HandlePlayerJoin(SocketIOResponse response) {
            GamePlayerDto dto = response.GetValue<GamePlayerDto>();
            if (joinedPlayes.Contains(dto.user)) {
                return;
            }

            joinedPlayes.Add(dto.user);
            PlayerAdded?.Invoke(dto);
        }
    }
}