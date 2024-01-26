using UnityEngine;
using Unity.Netcode;

namespace ER
{
    public class PlayerNetworkController : NetworkBehaviour
    {
        [SerializeField] private GameObject _canvas;

        private Vector3 _spawnPosition = new Vector3(-10.9f, 7.7f, -13.47f);

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
                EscapeRoomApp.Instance.OnHostSpawned.Invoke();
            }

            if (IsClient)
            {
                EscapeRoomApp.Instance.OnClientSpawned.Invoke();
                Debug.Log("Client spawn");
            }

            GarageDoor.Instance.RegisterPlayer(this.gameObject);
        }
    }
}
