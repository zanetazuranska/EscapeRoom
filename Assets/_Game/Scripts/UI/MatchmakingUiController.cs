using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;

namespace ER
{
    public class MatchmakingUiController : MonoBehaviour
    {
        public static MatchmakingUiController Instance { get; private set; }

        public UnityEvent OnJoinGameClick = new UnityEvent();
        public UnityEvent OnHostGameClick = new UnityEvent();
        public UnityEvent OnBackClick = new UnityEvent();

        [SerializeField] private TMP_InputField _playerName;
        [SerializeField] private TMP_InputField _hostIP;
        [SerializeField] private TMP_InputField _portNum;

        [SerializeField] private GameObject _dataValidationErrors;
        private TextMeshProUGUI _errorText;

        private NetworkSessionManager.DataValidationErrors _validationError;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            _errorText = _dataValidationErrors.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void BackClick()
        {
            OnBackClick.Invoke();
        }

        public void JoinGame()
        {
            _validationError = NetworkSessionManager.Instance.AddGameNetworkData(GameNetworkData.ENetMode.Client, _portNum.text, _hostIP.text, _playerName.text);

            if (_validationError != NetworkSessionManager.DataValidationErrors.None)
            {
                OnErrorBehavior(_validationError);
            }
            else
            {
                _dataValidationErrors.SetActive(false);
                OnJoinGameClick.Invoke();
            }
        }

        public void HostGame()
        {
            _validationError = NetworkSessionManager.Instance.AddGameNetworkData(GameNetworkData.ENetMode.Host, _portNum.text, _hostIP.text, _playerName.text);

            if (_validationError != NetworkSessionManager.DataValidationErrors.None)
            {
                OnErrorBehavior(_validationError);
            }
            else
            {
                _dataValidationErrors.SetActive(false);
                OnHostGameClick.Invoke();
            }
        }

        private void OnErrorBehavior(NetworkSessionManager.DataValidationErrors _validationError)
        {
            if (_validationError == NetworkSessionManager.DataValidationErrors.EmptyField)
            {
                _errorText.text = "Please fill in the required field.";
            }
            else if (_validationError == NetworkSessionManager.DataValidationErrors.PortNumError)
            {
                _errorText.text = "Please provide a valid port number.";
            }
            else
            {
                _errorText.text = "Invalid IP address. Please enter a valid IP address.";
            }

            StartCoroutine(ErrorUI());
        }

        private IEnumerator ErrorUI()
        {
            _dataValidationErrors.SetActive(true);

            yield return new WaitForSeconds(2);

            _dataValidationErrors.SetActive(false);
        }
    }

}