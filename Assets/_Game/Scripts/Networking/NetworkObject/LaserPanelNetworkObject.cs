using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace ER
{
    public class LaserPanelNetworkObject : NetworkBehaviour
    {
        [SerializeField] private GameObject _laserPanelButton;

        [ClientRpc]
        private void ActiveButtonClientRpc()
        {
            _laserPanelButton.SetActive(true);
            this.gameObject.SetActive(false);
        }

        [ServerRpc (RequireOwnership = false)]
        private void ActiveButtonServerRpc()
        {
            ActiveButtonClientRpc();
        }

        public void SwitchButtons()
        {
            if(IsHost)
            {
                ActiveButtonClientRpc();
            }
            else
            {
                ActiveButtonServerRpc();
            }
        }
    }
}

