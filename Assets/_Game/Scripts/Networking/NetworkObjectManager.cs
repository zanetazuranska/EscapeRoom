using ER;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkObjectManager : MonoBehaviour
{
    public static NetworkObjectManager Instance { get; private set; }

    private List<GameObject> _networkObjects = new List<GameObject>();

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

    public void AddNetworkObject(GameObject gameObject)
    {
        _networkObjects.Add(gameObject);
    }

    public void RemoveNetworkObject(GameObject gameObject)
    {
        _networkObjects.Remove(gameObject);
    }

    public List<GameObject> GetNetworkObjects()
    {
        return _networkObjects;
    }
}
