using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KithenTableNetworking : NetworkBehaviour
{
    [SerializeField] private Animator _animator;
    private const string ANIMATOR_BOOL = "CanOpen";

    private NetworkVariable<bool> _isOpenN = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [ServerRpc(RequireOwnership = false)]
    private void SetNetworkVaribleServerRpc(bool value)
    {
        _isOpenN.Value = value;
        _animator.SetBool(ANIMATOR_BOOL, true);
    }

    public void SetIsOpen(bool value)
    {
        if(IsHost)
        {
            _isOpenN.Value = value;
        }
        else
        {
            SetNetworkVaribleServerRpc(value);
        }
    }

    public bool GetIsOpen()
    {
        return _isOpenN.Value;
    }
}
