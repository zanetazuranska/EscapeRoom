using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ER
{
    public class InteractionManager : NetworkBehaviour
    {
        [SerializeField] private Transform _cursor;
        [SerializeField] private Camera _camera;

        private Ray _ray = new Ray();
        private RaycastHit _hit = new RaycastHit();
        private GameObject _lastHitObj;

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }

            _ray = _camera.ScreenPointToRay(_cursor.position);

            CheckHover();
            CheckClick();
        }

        private void CheckHover()
        {
            if (!EventSystem.current.IsPointerOverGameObject()
                && Physics.Raycast(_ray, out _hit))
            {
                InteractableObject interactableObject = _hit.transform.GetComponent<InteractableObject>();

                if (interactableObject != null)
                {
                    interactableObject.OnHover();
                    _lastHitObj = _hit.transform.gameObject;
                }
                else
                {
                    if (_lastHitObj != null)
                    {
                        _lastHitObj.GetComponent<InteractableObject>().OnUnHover();
                    }
                }
            }
            else
            {
                if (_lastHitObj != null)
                {
                    _lastHitObj.GetComponent<InteractableObject>().OnUnHover();
                }
            }
        }

        private void CheckClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject()
                && Physics.Raycast(_ray, out _hit))
                {
                    WorldItem worldItem = _hit.transform.GetComponent<WorldItem>();
                    if (worldItem != null)
                    {
                        GetComponentInParent<PlayerController>().GetInventory().Add(worldItem.GetItemType());
                    }

                    InteractableObject interactableObject = _hit.transform.GetComponent<InteractableObject>();

                    if (interactableObject != null)
                    {
                        interactableObject.OnClick();
                    }
                }
            }
        }
    }
}
