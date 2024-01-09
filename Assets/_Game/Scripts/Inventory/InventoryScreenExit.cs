using UnityEngine;
using UnityEngine.UI;

namespace ER
{
    public class InventoryScreenExit : MonoBehaviour
    {
        [SerializeField] private GameObject _parent;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnExitClick);
        }

        private void OnExitClick()
        {
            _parent.SetActive(false);
        }

        private void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveListener(OnExitClick);
        }
    }

}
