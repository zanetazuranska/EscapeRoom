using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkButtonsUi : MonoBehaviour
{
    [SerializeField] private Button _host;
    [SerializeField] private Button _client;

    private void Awake()
    {
        _host.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            _host.onClick.RemoveAllListeners();
        });

        _client.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            _client.onClick.RemoveAllListeners();
        });
    }
}
