using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LockerNetworkObject : NetworkBehaviour
{
    [SerializeField] private Animator _animator;
    private const string ANIMATOR_BOOL = "CanOpen";

    private NetworkVariable<bool> _isOpenN = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [ServerRpc(RequireOwnership = false)]
    private void StartAnimationServerRpc(bool value)
    {
        StartAnimationClientRpc();
        _isOpenN.Value = value;
    }

    [ClientRpc]
    private void StartAnimationClientRpc()
    {
        _animator.SetBool(ANIMATOR_BOOL, true);
    }

    public void SetIsOpen(bool value)
    {
        if (IsHost)
        {
            StartAnimationClientRpc();
            _isOpenN.Value = value;
        }
        else
        {
            StartAnimationServerRpc(true);
        }
    }

    public bool GetIsOpen()
    {
        return _isOpenN.Value;
    }
}
