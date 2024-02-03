using ER;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ER
{
    public class Cabinet : InteractableObject
    {
        [SerializeField] private Animator _animator;
        private MeshRenderer _renderer;

        private const string ANIMATOR_BOOL = "CanOpen";

        [SerializeField] private CabinetNetworkObject _networkObject;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void OnClick(InteractionContext context)
        {
            _networkObject.SetIsOpen(true);

            _animator.SetBool(ANIMATOR_BOOL, true);
        }

        public override void OnHover()
        {
            if (!_networkObject.GetIsOpen())
            {
                _renderer.materials[2].SetFloat("_Scale", 1.03f);
            }
        }

        public override void OnUnHover()
        {
            _renderer.materials[2].SetFloat("_Scale", 0f);
        }
    }
}


