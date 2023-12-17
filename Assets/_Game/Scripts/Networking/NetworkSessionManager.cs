using ER;
using UnityEngine;
using System.Collections.Generic;
using static GameNetworkData;
using Unity.VisualScripting;
using System;

public class NetworkSessionManager : MonoBehaviour
{
    public static NetworkSessionManager Instance { get; private set; }

    [SerializeField] private List<GameNetworkData> _gameNetworkDataList = new List<GameNetworkData>();

    public enum DataValidationErrors
    {
        None = 0,
        EmptyField = 1,
        PortNumError = 2,
        IPAddressError = 3,
    }

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

    public DataValidationErrors AddGameNetworkData (ENetMode netMode, string portNum, string iPAddress, string playerName)
    {
        if(portNum.Length == 0 || iPAddress.Length == 0 || playerName.Length == 0)
        {
            return DataValidationErrors.EmptyField;
        }

        var gameNetworkData = ScriptableObject.CreateInstance<GameNetworkData>();

        gameNetworkData.netMode = netMode;
        gameNetworkData.portNum = portNum;  

        if(!PortNumDataValidation(portNum))
        {
            return DataValidationErrors.PortNumError;
        }

        gameNetworkData.iPAddress = iPAddress;

        if (!IPAddressDataValidation(iPAddress))
        {
            return DataValidationErrors.IPAddressError;
        }

        gameNetworkData.playerName = playerName;

        _gameNetworkDataList.Add(gameNetworkData);

        return DataValidationErrors.None;
    }

    private bool PortNumDataValidation(string portNum)
    {
        for(int i=0; i<portNum.Length; i++)
        {
            if (!(portNum[i] >=48 && portNum[i] <=57))
            {
                return false;
            }
        }

        return true;
    }

    private bool IPAddressDataValidation(string iPAddress)
    {
        for (int i = 0; i < iPAddress.Length; i++)
        {
            if (iPAddress[i] == ' ')
            {
                return false;
            }
        }

        string[] splitValues = iPAddress.Split('.');

        if(splitValues.Length != 4 )
        {
            return false;
        }

        for(int i=0; i<splitValues.Length; i++)
        {
            if (splitValues[i].Length > 3 || splitValues[i].Length < 1)
            {
                return false;
            }

            string value = splitValues[i];

            if(value.Length == 3)
            {
                if (!(value[0] >= 48 && value[0] <= 50))
                {
                    return false;
                }
                else if(!(value[1] >= 48 && value[1] <= 53))
                {
                    return false;
                }
                else if (!(value[2] >= 48 && value[2] <= 53))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
