using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ER
{
    public class Padlock : InteractableObject
    {
        private MeshRenderer _renderer;
        private const string PADLOCK = "Have you ever experienced a break-in?";

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void OnClick(InteractionContext context)
        {
            StartCoroutine(context.interactionManager.ShowTextMessage(PADLOCK));
        }

        public override void OnHover()
        {
            _renderer.materials[1].SetFloat("_Scale", 1.03f);
        }

        public override void OnUnHover()
        {
            _renderer.materials[1].SetFloat("_Scale", 0f);
        }
    }
}

