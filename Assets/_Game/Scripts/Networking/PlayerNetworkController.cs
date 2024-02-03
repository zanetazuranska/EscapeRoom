using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

namespace ER
{
    public class PlayerNetworkController : NetworkBehaviour
    {
        [SerializeField] private GameObject _canvas;

        private Vector3 _spawnPosition = new Vector3(-10.9f, 7.7f, -13.47f);

        [SerializeField] private GameObject _faceHost;
        [SerializeField] private GameObject _faceClient;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            transform.position = _spawnPosition;

            GarageDoor.Instance.RegisterPlayer(this.gameObject);

            if (IsOwner)
            {
                _canvas.SetActive(true);
            }

            if(IsHost)
            {
                EscapeRoomApp.Instance.OnHostSpawned.Invoke();
                _faceHost.SetActive(true);
            }

            if (IsClient && !IsHost)
            {
                EscapeRoomApp.Instance.OnClientSpawned.Invoke();
                _faceClient.SetActive(true);
            }
        }
    }
}
