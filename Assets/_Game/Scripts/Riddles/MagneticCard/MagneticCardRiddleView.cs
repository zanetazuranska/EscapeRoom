using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace ER.Riddle.UI
{
    public class MagneticCardRiddleView : RiddleView
    {
        [SerializeField] private TextMeshProUGUI _textMessage;
        [SerializeField] private TextMeshProUGUI _degrees;
        [SerializeField] private GameObject _ui;
        [SerializeField] private Button _exit;

        [SerializeField] private int[] _levelTestMarkPositions = new int[4];
        [SerializeField] private Button _levelTestMarkButton;
        private int _currentLevelTestMarkPositionSkeletonKey1;
        private int _currentLevelTestMarkPositionSkeletonKey2;

        private int _currentActiveSkeletonKey = 1;

        [SerializeField] private Button[] _skeletonKeyButtons = new Button[2];
        [SerializeField] private GameObject[] _skeletonKeyMarks = new GameObject[2];

        [SerializeField] private Sprite[] _levelTestSprites = new Sprite[4];
        [SerializeField] private Image _levetTestImage;

        [SerializeField] private Slider _slider;
        private float _currentSkeletonKeyAngle1 = 0.5f;
        private float _currentSkeletonKeyAngle2 = 0.5f;

        private Transform _networkObject;

        private const string TOO_LARGE = "The angle is too large";
        private const string TOO_SMALL = "The angle is too small";

        [SerializeField] private Vector3[] _skeletonKey1Positions = new Vector3[3];
        [SerializeField] private Vector3[] _skeletonKey2Positions = new Vector3[3];

        private void Awake()
        {
            _exit.onClick.AddListener(SetDesactiveUI);

            _currentLevelTestMarkPositionSkeletonKey1 = 0;
            _currentLevelTestMarkPositionSkeletonKey2 = 0;
            _levelTestMarkButton.onClick.AddListener(LevelTestMarkController);

            _skeletonKeyButtons[0].onClick.AddListener(SkeletonKey1);
            _skeletonKeyButtons[1].onClick.AddListener(SkeletonKey2);

            _slider.onValueChanged.AddListener(SetRotation);

            EscapeRoomApp.Instance.GetAplicationFlowController().OnAddRiddleController.AddListener(OnAddRiddleController);
        }

        private void OnAddRiddleController(RiddleController riddleController)
        {
            if (riddleController.GetERiddleType() == RiddleType.ERiddleType.MagneticCard)
            {
                EscapeRoomApp.Instance.GetAplicationFlowController().OnAddRiddleController.RemoveListener(OnAddRiddleController);

                SetRiddleController(riddleController);

                riddleController.OnObjectSpawn.AddListener(OnNetworkObjectSpawn);
            }
        }

        private void OnNetworkObjectSpawn(Transform transform)
        {
            if (GetRiddleController() == null)
            {
                foreach (RiddleController riddleController in EscapeRoomApp.Instance.GetAplicationFlowController().GetRiddleControllers())
                {
                    if(riddleController.GetERiddleType() == RiddleType.ERiddleType.MagneticCard)
                    {
                        SetRiddleController(riddleController);
                    }
                }
            }

            if (transform == null)
            {
                if (GameObject.FindFirstObjectByType<ER.Padlock>() != null)
                    transform = GameObject.FindFirstObjectByType<ER.Padlock>().transform;
            }

            transform.gameObject.GetComponent<Padlock>().OnClickEvent.AddListener(SetActiveUI);

            _networkObject = transform;
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
        }

        private void LevelTestMarkController()
        {
            if(_currentActiveSkeletonKey == 1)
            {
                MagneticCardRiddlelogic riddleLogic = (MagneticCardRiddlelogic)GetRiddleController().GetRiddleLogic();

                if(riddleLogic.CheckSkeletonKeyStep(_currentActiveSkeletonKey-1, _currentLevelTestMarkPositionSkeletonKey1))
                {
                    if (_currentLevelTestMarkPositionSkeletonKey1 != 3)
                    {
                        SetMarkPosition(_currentLevelTestMarkPositionSkeletonKey1);
                        _currentLevelTestMarkPositionSkeletonKey1++;

                        SetSkeletonKeyPosition(0, _currentLevelTestMarkPositionSkeletonKey1 - 1);

                        if(riddleLogic.CheckAnswer(riddleLogic.GetRiddleData()))
                        {
                            OnAnswerCorrect();
                        }
                    }
                }
                else
                {
                    MagneticCardData riddledata = (MagneticCardData)riddleLogic.GetRiddleData();
                    if (riddledata.PuzzleAnswer[_currentActiveSkeletonKey-1].ValueSet[_currentLevelTestMarkPositionSkeletonKey1] < riddledata.PuzzleProposedAnswer[_currentActiveSkeletonKey-1].ValueSet[_currentLevelTestMarkPositionSkeletonKey1])
                    {
                        StartCoroutine(ShowTextMessage(TOO_LARGE));
                    }
                    else
                    {
                        StartCoroutine(ShowTextMessage(TOO_SMALL));
                    }
                }
            }
            else
            {
                MagneticCardRiddlelogic riddleLogic = (MagneticCardRiddlelogic)GetRiddleController().GetRiddleLogic();

                if (riddleLogic.CheckSkeletonKeyStep(_currentActiveSkeletonKey - 1, _currentLevelTestMarkPositionSkeletonKey2))
                {
                    if (_currentLevelTestMarkPositionSkeletonKey2 != 3)
                    {
                        SetMarkPosition(_currentLevelTestMarkPositionSkeletonKey2);
                        _currentLevelTestMarkPositionSkeletonKey2++;

                        SetSkeletonKeyPosition(1, _currentLevelTestMarkPositionSkeletonKey2 - 1);

                        if (riddleLogic.CheckAnswer(riddleLogic.GetRiddleData()))
                        {
                            OnAnswerCorrect();
                        }
                    }
                }
                else
                {
                    MagneticCardData riddledata = (MagneticCardData)riddleLogic.GetRiddleData();
                    if (riddledata.PuzzleAnswer[_currentActiveSkeletonKey - 1].ValueSet[_currentLevelTestMarkPositionSkeletonKey2] < riddledata.PuzzleProposedAnswer[_currentActiveSkeletonKey - 1].ValueSet[_currentLevelTestMarkPositionSkeletonKey2])
                    {
                        StartCoroutine(ShowTextMessage(TOO_LARGE));
                    }
                    else
                    {
                        StartCoroutine(ShowTextMessage(TOO_SMALL));
                    }
                }
            }
        }

        private void SetMarkPosition(int keyId)
        {
            Transform transform = _levelTestMarkButton.gameObject.transform;
            keyId++;
            transform.localPosition = new Vector3(_levelTestMarkPositions[keyId], transform.localPosition.y, transform.localPosition.z);

            _levetTestImage.sprite = _levelTestSprites[keyId];
        }

        private void SetSkeletonKeyPosition(int keyId, int positionId)
        {
            GameObject gameObject = _skeletonKeyButtons[keyId].gameObject;

            if(keyId==0)
            {
                gameObject.transform.localPosition = _skeletonKey1Positions[positionId];
            }
            else
            {
                gameObject.transform.localPosition = _skeletonKey2Positions[positionId];
            }
        }

        private void SkeletonKey1()
        {
            ActivGameObject(_skeletonKeyMarks[0]);
            DesactivegameObject(_skeletonKeyMarks[1]);

            _currentActiveSkeletonKey = 1;

            Transform transform = _levelTestMarkButton.gameObject.transform;
            transform.localPosition = new Vector3(_levelTestMarkPositions[_currentLevelTestMarkPositionSkeletonKey1], transform.localPosition.y, transform.localPosition.z);

            _levetTestImage.sprite = _levelTestSprites[_currentLevelTestMarkPositionSkeletonKey1];
        }

        private void SkeletonKey2()
        {
            ActivGameObject(_skeletonKeyMarks[1]);
            DesactivegameObject(_skeletonKeyMarks[0]);

            _currentActiveSkeletonKey = 2;

            Transform transform = _levelTestMarkButton.gameObject.transform;
            transform.localPosition = new Vector3(_levelTestMarkPositions[_currentLevelTestMarkPositionSkeletonKey2], transform.localPosition.y, transform.localPosition.z);

            _levetTestImage.sprite = _levelTestSprites[_currentLevelTestMarkPositionSkeletonKey2];
        }

        private void ActivGameObject(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        private void DesactivegameObject(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        private void SetRotation(float value)
        {
            GameObject gameObject = _skeletonKeyButtons[_currentActiveSkeletonKey - 1].gameObject;

            MagneticCardRiddlelogic riddleLogic = (MagneticCardRiddlelogic)GetRiddleController().GetRiddleLogic();
            MagneticCardData riddledata = (MagneticCardData)riddleLogic.GetRiddleData();

            if (_currentActiveSkeletonKey == 1)
            {
                if(_currentLevelTestMarkPositionSkeletonKey1 == 3)
                {
                    return;
                }

                _currentSkeletonKeyAngle1 = value;
            }
            else
            {
                if (_currentLevelTestMarkPositionSkeletonKey2 == 3)
                {
                    return;
                }

                _currentSkeletonKeyAngle2 = value;
            }

            if (value == 0.5f)
            {
                gameObject.transform.eulerAngles = Vector3.zero;

                if (_currentActiveSkeletonKey == 1)
                {
                    riddledata.PuzzleProposedAnswer[_currentActiveSkeletonKey - 1].ValueSet[_currentLevelTestMarkPositionSkeletonKey1] = 0;
                    _degrees.text = "0°";
                }
                else
                {
                    riddledata.PuzzleProposedAnswer[_currentActiveSkeletonKey - 1].ValueSet[_currentLevelTestMarkPositionSkeletonKey2] = 0;
                    _degrees.text = "0°";
                }
            }
            else if (value < 0.5f)
            {
                value = 0.5f - value;
                int angle = (int)(value / 0.01d);

                if (_currentActiveSkeletonKey == 1)
                {
                    gameObject.transform.eulerAngles = new Vector3(0, 0, angle);
                    _currentSkeletonKeyAngle1 = angle;

                    riddledata.PuzzleProposedAnswer[_currentActiveSkeletonKey - 1].ValueSet[_currentLevelTestMarkPositionSkeletonKey1] = angle;
                    _degrees.text = angle.ToString() + "°";
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(0, 0, -angle);
                    _currentSkeletonKeyAngle2 = -angle;

                    riddledata.PuzzleProposedAnswer[_currentActiveSkeletonKey - 1].ValueSet[_currentLevelTestMarkPositionSkeletonKey2] = -angle;
                    _degrees.text = "-" + angle.ToString() + "°";
                }
            }
            else
            {
                value = value - 0.5f;
                int angle = (int)(value / 0.01d);

                if (_currentActiveSkeletonKey == 1)
                {
                    gameObject.transform.eulerAngles = new Vector3(0, 0, -angle);
                    _currentSkeletonKeyAngle1 = -angle;

                    riddledata.PuzzleProposedAnswer[_currentActiveSkeletonKey - 1].ValueSet[_currentLevelTestMarkPositionSkeletonKey1] = -angle;
                    _degrees.text = "-" + angle.ToString() + "°";
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(0, 0, angle);
                    _currentSkeletonKeyAngle2 = angle;

                    riddledata.PuzzleProposedAnswer[_currentActiveSkeletonKey - 1].ValueSet[_currentLevelTestMarkPositionSkeletonKey2] = angle;
                    _degrees.text = angle.ToString() + "°";
                }
            }
        }

        private IEnumerator ShowTextMessage(string message)
        {
            _textMessage.text = message;

            _textMessage.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);

            _textMessage.gameObject.SetActive(false);
        }

        private void OnAnswerCorrect()
        {
            SetDesactiveUI();
            _slider.onValueChanged.RemoveListener(SetRotation);
            _networkObject.gameObject.GetComponent<Padlock>().OnClickEvent.RemoveListener(SetActiveUI);
        }

        private void OnDestroy()
        {
            _exit.onClick.RemoveListener(SetDesactiveUI);
            _levelTestMarkButton.onClick.RemoveListener(LevelTestMarkController);

            _skeletonKeyButtons[0].onClick.RemoveListener(SkeletonKey1);
            _skeletonKeyButtons[1].onClick.RemoveListener(SkeletonKey2);

            EscapeRoomApp.Instance.GetAplicationFlowController().OnAddRiddleController.RemoveListener(OnAddRiddleController);

            _networkObject.gameObject.GetComponent<Padlock>().OnClickEvent.RemoveListener(SetActiveUI);
        }
    }
}
