using Newtonsoft.Json;

namespace Sockets {
    [System.Serializable]
    [JsonObject]
    public class GameOptionDto {
        [JsonProperty]
        public string name;
        [JsonProperty]
        public string color;
        
        public GameOptionDto(string name, string color) {
            this.name = name;
            this.color = color;
        }
    }
}