using UnityEngine;
using UnityEngine.UI;

namespace ER.Riddle.UI
{
    public class MagneticCardRiddleView : RiddleView
    {
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

        private void Awake()
        {
            _exit.onClick.AddListener(SetDesactiveUI);

            _currentLevelTestMarkPositionSkeletonKey1 = 0;
            _currentLevelTestMarkPositionSkeletonKey2 = 0;
            _levelTestMarkButton.onClick.AddListener(LevelTestMarkController);

            _skeletonKeyButtons[0].onClick.AddListener(SkeletonKey1);
            _skeletonKeyButtons[1].onClick.AddListener(SkeletonKey2);
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
                if(_currentLevelTestMarkPositionSkeletonKey1 != 3)
                {
                    Transform transform = _levelTestMarkButton.gameObject.transform;
                    _currentLevelTestMarkPositionSkeletonKey1++;
                    transform.localPosition = new Vector3(_levelTestMarkPositions[_currentLevelTestMarkPositionSkeletonKey1], transform.localPosition.y, transform.localPosition.z);

                    _levetTestImage.sprite = _levelTestSprites[_currentLevelTestMarkPositionSkeletonKey1];
                }
            }
            else
            {
                if (_currentLevelTestMarkPositionSkeletonKey2 != 3)
                {
                    Transform transform = _levelTestMarkButton.gameObject.transform;
                    _currentLevelTestMarkPositionSkeletonKey2++;
                    transform.localPosition = new Vector3(_levelTestMarkPositions[_currentLevelTestMarkPositionSkeletonKey2], transform.localPosition.y, transform.localPosition.z);

                    _levetTestImage.sprite = _levelTestSprites[_currentLevelTestMarkPositionSkeletonKey2];
                }
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

        private void OnDestroy()
        {
            _exit.onClick.RemoveListener(SetDesactiveUI);
            _levelTestMarkButton.onClick.RemoveListener(LevelTestMarkController);

            _skeletonKeyButtons[0].onClick.RemoveListener(SkeletonKey1);
            _skeletonKeyButtons[1].onClick.RemoveListener(SkeletonKey2);
        }
    }
}
