using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace ER
{
    public class BedroomDoorNetworkObject : NetworkBehaviour
    {
        [ClientRpc]
        private void DestroyDoorClientRpc()
        {
            Destroy(this.gameObject);
        }

        [ServerRpc (RequireOwnership = false)]
        private void DestroyDoorServerRpc()
        {
            DestroyDoorClientRpc();
        }

        public void DestroyDoor()
        {
            if(IsHost)
            {
                DestroyDoorClientRpc();
            }
            else
            {
                DestroyDoorServerRpc();
            }
        }
    }
}

