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

        private Ray _ray = new Ray();
        private RaycastHit _hit = new RaycastHit();
        private GameObject _lastHitObj;

        //Texts to write
        private const string NEW_ITEM_TEXT = "Added new item";
        private const string DOOR = "The door is closed... How to get out?";
        private const string EMPTY_CHEST = "The chest is empty";

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
                bool canPerform = true;

                if (!EventSystem.current.IsPointerOverGameObject()
                && Physics.Raycast(_ray, out _hit))
                {
                    WorldItem worldItem = _hit.transform.GetComponent<WorldItem>();
                    if (worldItem != null)
                    {
                        GetComponentInParent<PlayerController>().GetInventory().Add(worldItem.GetItemType());
                        StartCoroutine(ShowTextMessage(NEW_ITEM_TEXT));
                    }

                    GarageDoor garageDoor = _hit.transform.GetComponent<GarageDoor>();
                    if(garageDoor != null)
                    {
                        if (GetComponentInParent<PlayerController>().GetInventory().GetItems().Contains(ItemRegister.Instance.GetItem(Item.ItemType.DoorKey)))
                        {
                            GetComponentInParent<PlayerController>().PlayerCanUseInput(false);
                            garageDoor.OnClick();
                        }
                        else
                        {
                            StartCoroutine(ShowTextMessage(DOOR));
                            canPerform = false;
                        }
                    }

                    TreasureChest treasureChest = _hit.transform.GetComponent<TreasureChest>();
                    if (treasureChest != null)
                    {
                        StartCoroutine(ShowTextMessage(EMPTY_CHEST));
                    }

                    InteractableObject interactableObject = _hit.transform.GetComponent<InteractableObject>();

                    if (interactableObject != null)
                    {
                        if(canPerform)
                        {
                            interactableObject.OnClick();
                            Debug.Log(canPerform + "Click");
                        }     
                    }
                }
            }
        }

        private IEnumerator ShowTextMessage(string message)
        {
            _textMessage.text = message;

            _textMessageObj.SetActive(true);
            yield return new WaitForSeconds(2);

            _textMessageObj.SetActive(false);
        }
    }
}
