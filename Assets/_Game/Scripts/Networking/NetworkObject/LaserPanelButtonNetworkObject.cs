using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace ER
{
    public class LaserPanelButtonNetworkObject : NetworkBehaviour
    {
        [SerializeField] private GameObject _lasers;

        [ClientRpc]
        private void DesactiveLasersClientRpc()
        {
            _lasers.SetActive(false);
        }

        [ServerRpc (RequireOwnership = false)]
        private void DesactiveLasersServerRpc()
        {
            DesactiveLasersClientRpc();
        }

        public void DesactiveLasers()
        {
            if(IsHost)
            {
                DesactiveLasersClientRpc();
            }
            else
            {
                DesactiveLasersServerRpc();
            }
        }
    }

}
