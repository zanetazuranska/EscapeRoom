using UnityEngine;
using Unity.Netcode;

namespace ER
{
    public class PlayerNetworkController : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            transform.position = new Vector3(-10.9f, 6.57f, -13.47f);
        }
    }
}
