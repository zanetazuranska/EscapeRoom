using UnityEngine;

namespace ER
{
    public class GameNetworkData : ScriptableObject
    {
        public enum ENetMode
        {
            Host = 1,
            Client = 2,
        }

        public ENetMode netMode;
        public string portNum;
        public string iPAddress;
        public string playerName;
    }
}
