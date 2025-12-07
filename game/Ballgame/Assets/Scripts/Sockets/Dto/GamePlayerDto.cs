using Newtonsoft.Json;

namespace Sockets {
    [System.Serializable]
    [JsonObject]
    public class GamePlayerDto {
        [JsonProperty]
        public string user;
        [JsonProperty]
        public string option;
    }
}