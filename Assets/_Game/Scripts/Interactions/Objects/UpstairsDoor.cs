using UnityEngine;
using UnityEngine.UI;

namespace ER
{
    public class UpstairsDoor : InteractableObject
    {
        private MeshRenderer _renderer;

        [SerializeField] private GameObject _upstairsRiddleUI;
        [SerializeField] private Button _exit;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();

            _exit.onClick.AddListener(OnExitClick);
        }

        public override void OnClick()
        {
            _upstairsRiddleUI.SetActive(true);
        }

        public override void OnHover()
        {
            _renderer.materials[1].SetFloat("_Scale", 1.03f);
        }

        public override void OnUnHover()
        {
            _renderer.materials[1].SetFloat("_Scale", 0f);
        }

        public void OnExitClick()
        {
            _upstairsRiddleUI.SetActive(false);
        }

        public override void OnDestroy()
        {
            _exit.onClick.RemoveListener(OnExitClick);
        }
    }
}

