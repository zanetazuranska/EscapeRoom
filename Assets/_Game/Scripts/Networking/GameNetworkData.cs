using UnityEngine;

public class GameNetworkData : ScriptableObject
{
    public enum NetModeEnum
    {
        Host = 1,
        Client = 2,
    }

    public NetModeEnum netMode;
    public string portNum;
    public string iPAddress;
    public string playerName;
}
