using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Sockets {
    public interface IGameSessionProvider {
        public event Action<GamePlayerDto> PlayerAdded;
        public event Action<string> CodeReceived;

        public IEnumerator Connect();
        public void Disconnect();
        
        public string GetCode();
        public IEnumerator GetQrCode(Reference<Texture2D> textureRef);
        public void SetTitle(string title);
        public void SetOptions(IList<GameOptionDto> gameOptions);
    }
}