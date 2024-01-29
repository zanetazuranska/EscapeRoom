using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MetalBoxNetworkObject : NetworkBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _padlock;
    [SerializeField] private GameObject _card;
    private const string ANIMATOR_BOOL = "CanOpen";

    private NetworkVariable<bool> _isOpenN = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [ServerRpc(RequireOwnership = false)]
    private void SetNetworkVaribleServerRpc(bool value)
    {
        _padlock.SetActive(false);
        _isOpenN.Value = value;
        _animator.SetBool(ANIMATOR_BOOL, true);

        _card.SetActive(true);
    }

    public void SetIsOpen(bool value)
    {
        if (IsHost)
        {
            _padlock.SetActive(false);
            _isOpenN.Value = value;

            _card.SetActive(true);
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
