using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocketIOClient;
using Sockets.Exceptions;
using UnityEngine;

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
            socket.OnUnityThread("code", HandleCodeReceived);
            socket.OnUnityThread("playerJoin", HandlePlayerJoin);
            Task task = socket.ConnectAsync();
            yield return new WaitUntil(() => task.IsCompleted);
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