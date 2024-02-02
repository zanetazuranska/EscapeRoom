using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace ER.Riddle.UI
{
    public class UpstairsRiddleView : RiddleView
    {
        [SerializeField] private List<Button> _arrowUpButtons = new List<Button>();
        [SerializeField] private List<Button> _arrowDownButtons = new List<Button>();
        [SerializeField] private List<TextMeshProUGUI> _texts = new List<TextMeshProUGUI>();

        [SerializeField] private GameObject _ui;
        [SerializeField] private Button _exit;

        private Transform _networkObject;

        private void Awake()
        {
            EscapeRoomApp.Instance.GetAplicationFlowController().OnAddRiddleController.AddListener(OnAddRiddleController);
        }

        private void OnAddRiddleController(RiddleController riddleController)
        {
            if(riddleController.GetERiddleType() == RiddleType.ERiddleType.BarrelLocker)
            {
                EscapeRoomApp.Instance.GetAplicationFlowController().OnAddRiddleController.RemoveListener(OnAddRiddleController);

                SetRiddleController(riddleController);
                riddleController.OnObjectSpawn.AddListener(OnNetworkObjectSpawn);

                for(int i = 0; i < riddleController.GetRiddleData().proposedAnswer.Count; i++)
                {
                    riddleController.GetRiddleData().proposedAnswer[i] = "0";
                }

                SetButton();

                _exit.onClick.AddListener(SetDesactiveUI);
            }
        }

        private void OnNetworkObjectSpawn(Transform transform)
        {
            if (GetRiddleController() == null)
            {
                SetRiddleController(EscapeRoomApp.Instance.GetAplicationFlowController().GetRiddleControllers()[0]);
            }

            if (transform == null)
            {
                if(GameObject.FindFirstObjectByType<ER.Riddle.UpstairsDoor>() != null)
                    transform = GameObject.FindFirstObjectByType<ER.Riddle.UpstairsDoor>().transform;
            }

            transform.gameObject.GetComponent<UpstairsDoor>().OnClickEvent.AddListener(SetActiveUI);

            _networkObject = transform;
        }

        private void SetButton()
        {
            _arrowUpButtons[0].onClick.AddListener(ButtonUp0);
            _arrowUpButtons[1].onClick.AddListener(ButtonUp1);
            _arrowUpButtons[2].onClick.AddListener(ButtonUp2);
            _arrowUpButtons[3].onClick.AddListener(ButtonUp3);

            _arrowDownButtons[0].onClick.AddListener(ButtonDown0);
            _arrowDownButtons[1].onClick.AddListener(ButtonDown1);
            _arrowDownButtons[2].onClick.AddListener(ButtonDown2);
            _arrowDownButtons[3].onClick.AddListener(ButtonDown3);
        }

        private void SetActiveUI()
        {
            _ui.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        private void SetDesactiveUI()
        {
            _ui.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            _networkObject.GetComponent<UpstairsDoor>().SetCameraTrue();
        }

        private void ChangeValueUp(int barrelId)
        {
            BarrelLockerPuzzleLogic barrelLockerPuzzleLogic = (BarrelLockerPuzzleLogic)GetRiddleController().GetRiddleLogic();
            BarrelLockerData data = (BarrelLockerData)barrelLockerPuzzleLogic.GetRiddleData();

            int i = 0;

            if (data.proposedAnswer[barrelId] == data.Barrels[0].ValueSet[9])
            {
                data.proposedAnswer[barrelId] = data.Barrels[0].ValueSet[0];
            }
            else
            {
                foreach (string value in data.Barrels[0].ValueSet)
                {
                    if(data.proposedAnswer[barrelId] == value)
                    {
                        data.proposedAnswer[barrelId] = data.Barrels[0].ValueSet[i+1];
                        break;
                    }

                    i++;
                }
            }

            _texts[barrelId].text = data.proposedAnswer[barrelId];

            if(GetRiddleController().IsAnswerCorrect(data))
            {
                OnAnswerCorrect();
            }
        }

        private void ChangeValueDown(int barrelId)
        {
            BarrelLockerPuzzleLogic barrelLockerPuzzleLogic = (BarrelLockerPuzzleLogic)GetRiddleController().GetRiddleLogic();
            BarrelLockerData data = (BarrelLockerData)barrelLockerPuzzleLogic.GetRiddleData();

            int i = 0;

            if (data.proposedAnswer[barrelId] == data.Barrels[0].ValueSet[0])
            {
                data.proposedAnswer[barrelId] = data.Barrels[0].ValueSet[9];
            }
            else
            {
                foreach (string value in data.Barrels[0].ValueSet)
                {
                    if (data.proposedAnswer[barrelId] == value)
                    {
                        data.proposedAnswer[barrelId] = data.Barrels[0].ValueSet[i - 1];
                        break;
                    }

                    i++;
                }
            }

            _texts[barrelId].text = data.proposedAnswer[barrelId];

            if (GetRiddleController().IsAnswerCorrect(data))
            {
                OnAnswerCorrect();
            }
        }

        private void ButtonUp0()
        {
            ChangeValueUp(0);
        }

        private void ButtonUp1()
        {
            ChangeValueUp(1);
        }

        private void ButtonUp2()
        {
            ChangeValueUp(2);
        }

        private void ButtonUp3()
        {
            ChangeValueUp(3);
        }

        private void ButtonDown0()
        {
            ChangeValueDown(0);
        }
        private void ButtonDown1()
        {
            ChangeValueDown(1);
        }
        private void ButtonDown2()
        {
            ChangeValueDown(2);
        }
        private void ButtonDown3()
        {
            ChangeValueDown(3);
        }

        private void OnAnswerCorrect()
        {
            SetDesactiveUI();

            _networkObject.gameObject.GetComponent<UpstairsDoor>().OnClickEvent.RemoveListener(SetActiveUI);
            _exit.onClick.RemoveListener(SetDesactiveUI);
            Destroy(this);
        }

        private void OnDestroy()
        {
            _arrowUpButtons[0].onClick.RemoveListener(ButtonUp0);
            _arrowUpButtons[1].onClick.RemoveListener(ButtonUp1);
            _arrowUpButtons[2].onClick.RemoveListener(ButtonUp2);
            _arrowUpButtons[3].onClick.RemoveListener(ButtonUp3);

            _arrowDownButtons[0].onClick.RemoveListener(ButtonDown0);
            _arrowDownButtons[1].onClick.RemoveListener(ButtonDown1);
            _arrowDownButtons[2].onClick.RemoveListener(ButtonDown2);
            _arrowDownButtons[3].onClick.RemoveListener(ButtonDown3);

            EscapeRoomApp.Instance.GetAplicationFlowController().OnAddRiddleController.RemoveListener(OnAddRiddleController);
        }
    }
}

