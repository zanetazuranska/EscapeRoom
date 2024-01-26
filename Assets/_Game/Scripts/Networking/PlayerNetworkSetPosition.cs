using ER;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetworkSetPosition : NetworkBehaviour
{
    [SerializeField] private List<GameObject> _players = new List<GameObject>();
    private PlayerNetworkController _playerNetworkController;

    public void RegisterPlayer(GameObject player)
    {
        _players.Add(player);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetPositionServerRpc()
    {
        foreach (GameObject player in _players)
        {
            Debug.Log("SetPos" + player.GetComponent<PlayerNetworkController>().IsHost);

            player.transform.position = new Vector3(20.0f, 7.7f, -3.1f);
            player.transform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);

            player.GetComponent<PlayerController>().DesactiveInventory();
        }

        SetPositionClientRpc();
    }

    [ClientRpc]
    public void SetPositionClientRpc()
    {
        if (_players.Count == 0)
        {
            Debug.Log("Brak graczy");
            return;
        }

        foreach (GameObject player in _players)
        {
            Debug.Log("SetPos" + player.GetComponent<PlayerNetworkController>().IsHost);

            player.transform.position = new Vector3(20.0f, 7.57f, -3.1f);
            player.transform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);

            player.GetComponent<PlayerController>().DesactiveInventory();
        }
    }

    public void SetPlayerNetworkController(PlayerNetworkController playerNetworkController)
    {
        _playerNetworkController = playerNetworkController;
    }
}
