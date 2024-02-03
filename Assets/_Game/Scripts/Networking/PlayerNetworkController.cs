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

        [ClientRpc]
        private void SetClientFaceClientRpc()
        {
            if (IsHost) return;

            _faceClient.SetActive(true);
        }

        [ClientRpc]
        private void SetHostFaceClientRpc()
        {
            if(!IsHost) return;

            _faceHost.SetActive(true);
        }

        [ServerRpc (RequireOwnership = false)]
        private void InvokeOnHostServerRpc()
        {
            SetHostFaceClientRpc();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            transform.position = _spawnPosition;

            if(IsOwner)
            {
                _canvas.SetActive(true);
            }

            if(IsHost)
            {
                Debug.Log("Host");

                _faceHost.SetActive(true);

                EscapeRoomApp.Instance.OnHostSpawned.Invoke();
            }

            if (IsClient && !IsHost)
            {
                Debug.Log("Client");

                _faceClient.SetActive(true);

                EscapeRoomApp.Instance.OnClientSpawned.Invoke();

            }

            GarageDoor.Instance.RegisterPlayer(this.gameObject);
        }

        private void OnDestroy()
        {
            EscapeRoomApp.Instance.OnClientSpawned.RemoveListener(SetClientFaceClientRpc);
        }
    }
}
