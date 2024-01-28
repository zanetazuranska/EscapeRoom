using UnityEngine;
using UnityEngine.Events;

namespace ER.Riddle
{
    public class UpstairsDoor : InteractableObject
    {
        private MeshRenderer _renderer;

        public UnityEvent OnClickEvent = new UnityEvent();

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void OnClick(InteractionContext context)
        {
            OnClickEvent.Invoke();
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
