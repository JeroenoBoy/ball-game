using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sockets {
    public class ConnectionConfig : ScriptableObject {

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void CreateConfig() {
            if (!Resources.Load<ConnectionConfig>("ConnectionConfig")) {
                AssetDatabase.CreateAsset(CreateInstance<ConnectionConfig>(), "Assets/Resources/ConnectionConfig.asset");
            }
            if (!Resources.Load<ConnectionConfig>("ConnectionConfig.local")) {
                AssetDatabase.CreateAsset(CreateInstance<ConnectionConfig>(), "Assets/Resources/ConnectionConfig.local.asset");
            }

        }
#endif

        public static ConnectionConfig GetConfig() {
#if UNITY_EDITOR
            ConnectionConfig config = Resources.Load<ConnectionConfig>("ConnectionConfig.local");
            if (config != null) {
                return config;
            }
#endif
            return Resources.Load<ConnectionConfig>("ConnectionConfig");
        }
        
        [field: SerializeField] public bool useMockServer = false;
        [field: SerializeField] public string ConnectionUrl { get; private set; } = "https://balls.jeroenvdg.com/ws";
    }
}