using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ER
{
    public class LaserPanelButton : InteractableObject
    {
        private MeshRenderer _renderer;

        [SerializeField] private LaserPanelButtonNetworkObject _laserPanelButtonNetworkObject;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void OnClick(InteractionContext context)
        {
            _laserPanelButtonNetworkObject.DesactiveLasers();
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

