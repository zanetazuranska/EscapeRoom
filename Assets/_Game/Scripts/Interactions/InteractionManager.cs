using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;

namespace ER
{
    public class InteractionManager : NetworkBehaviour
    {
        [SerializeField] private Transform _cursor;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _textMessageObj;
        [SerializeField] private TextMeshProUGUI _textMessage;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _maxDistance;

        private Ray _ray = new Ray();
        private RaycastHit _hit = new RaycastHit();
        private GameObject _lastHitObj;

        private const string NEW_ITEM_TEXT = "Added new item";

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
                && Physics.Raycast(_ray, out _hit, _maxDistance, _layerMask))
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
                bool canPerform = true;

                if (!EventSystem.current.IsPointerOverGameObject()
                && Physics.Raycast(_ray, out _hit, _maxDistance, _layerMask))
                {
                    InteractionContext context = new InteractionContext(GetComponentInParent<PlayerController>(), true, this);

                    WorldItem worldItem = _hit.transform.GetComponent<WorldItem>();
                    if (worldItem != null)
                    {
                        StartCoroutine(ShowTextMessage(NEW_ITEM_TEXT));
                    }

                    InteractableObject interactableObject = _hit.transform.GetComponent<InteractableObject>();

                    if (interactableObject != null)
                    {
                        if(canPerform)
                        {
                            interactableObject.OnClick(context);
                        }     
                    }
                }
            }
        }

        public IEnumerator ShowTextMessage(string message)
        {
            _textMessage.text = message;

            _textMessageObj.SetActive(true);
            yield return new WaitForSeconds(2);

            _textMessageObj.SetActive(false);
        }
    }
}
