using ER;
using UnityEngine;
using System.Collections.Generic;
using static GameNetworkData;

public class NetworkSessionManager : MonoBehaviour
{
    public static NetworkSessionManager Instance { get; private set; }

    [SerializeField] private List<GameNetworkData> _gameNetworkDataList = new List<GameNetworkData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddGameNetworkData (NetModeEnum netMode, string portNum, string iPAddress, string playerName)
    {

        var gameNetworkData = ScriptableObject.CreateInstance<GameNetworkData>();

        gameNetworkData.netMode = netMode;
        gameNetworkData.portNum = portNum;  
        gameNetworkData.iPAddress = iPAddress;
        gameNetworkData.playerName = playerName;

        _gameNetworkDataList.Add(gameNetworkData);
    }

}
