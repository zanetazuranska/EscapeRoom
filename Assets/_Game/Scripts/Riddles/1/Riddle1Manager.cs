using UnityEngine;

namespace ER
{
    public class Riddle1Manager : MonoBehaviour
    {
        [SerializeField] private UiRiddleModule _uiRiddleModule1;
        [SerializeField] private UiRiddleModule _uiRiddleModule2;
        [SerializeField] private UiRiddleModule _uiRiddleModule3;
        [SerializeField] private UiRiddleModule _uiRiddleModule4;

        [SerializeField] private GameObject _door;

        private int[] _correctValues = new int[4];

        private int[] _currentValues = new int[4];


        private void Awake()
        {
            _correctValues[0] = 4;
            _correctValues[1] = 3;
            _correctValues[2] = 5;
            _correctValues[3] = 7;

            _currentValues[0] = _uiRiddleModule1.GetValue();
            _currentValues[1] = _uiRiddleModule2.GetValue();
            _currentValues[2] = _uiRiddleModule3.GetValue();
            _currentValues[3] = _uiRiddleModule4.GetValue();

            _uiRiddleModule1.OnValueChanged.AddListener(CheckValues);
            _uiRiddleModule2.OnValueChanged.AddListener(CheckValues);
            _uiRiddleModule3.OnValueChanged.AddListener(CheckValues);
            _uiRiddleModule4.OnValueChanged.AddListener(CheckValues);
        }

        private void CheckValues()
        {
            _currentValues[0] = _uiRiddleModule1.GetValue();
            _currentValues[1] = _uiRiddleModule2.GetValue();
            _currentValues[2] = _uiRiddleModule3.GetValue();
            _currentValues[3] = _uiRiddleModule4.GetValue();

            if (IsTheSame())
            {
                RiddleCorrect();
            }
        }

        private void RiddleCorrect()
        {
            _door.GetComponent<UpstairsDoor>().OnExitClick();
            Destroy(_door);
            Destroy(transform.parent.gameObject);
        }

        private bool IsTheSame()
        {
            for (int i = 0; i < 4; i++)
            {
                if (_correctValues[i] != _currentValues[i])
                {
                    return false;
                }
            }

            return true;
        }

        private void OnDestroy()
        {
            _uiRiddleModule1.OnValueChanged.RemoveListener(CheckValues);
            _uiRiddleModule2.OnValueChanged.RemoveListener(CheckValues);
            _uiRiddleModule3.OnValueChanged.RemoveListener(CheckValues);
            _uiRiddleModule4.OnValueChanged.RemoveListener(CheckValues);
        }
    }
}
