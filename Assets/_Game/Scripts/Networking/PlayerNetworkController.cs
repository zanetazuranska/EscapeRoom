using UnityEngine;
using Unity.Netcode;

public class PlayerNetworkController : NetworkBehaviour
{
    private void Awake()
    {
        NetworkManager.Singleton.OnClientStarted += OnClientStart;
    }

    public override void OnNetworkSpawn()
    {
        transform.position = new Vector3(-10.9f, 6.57f, -13.47f);
    }

    private void Update()
    {

    }

    private void OnClientStart()
    {
        Debug.Log("Client Start");
    }
}
