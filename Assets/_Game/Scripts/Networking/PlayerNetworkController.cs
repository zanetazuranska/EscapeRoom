using UnityEngine;
using Unity.Netcode;

namespace ER
{
    public class PlayerNetworkController : NetworkBehaviour
    {
        private Vector3 _spawnPosition = new Vector3(-10.9f, 6.57f, -13.47f);

        public override void OnNetworkSpawn()
        {
            transform.position = _spawnPosition;
        }
    }
}
