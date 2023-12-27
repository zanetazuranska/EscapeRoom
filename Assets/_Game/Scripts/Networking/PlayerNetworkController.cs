using UnityEngine;
using Unity.Netcode;

public class PlayerNetworkController : NetworkBehaviour
{
    private void Awake()
    {
        NetworkManager.Singleton.OnClientStarted += OnClientStart;
    }

    private void Update()
    {

    }

    private void OnClientStart()
    {
        Debug.Log("Client Start");
    }
}
