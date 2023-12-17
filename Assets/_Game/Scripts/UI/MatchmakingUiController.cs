using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MatchmakingUiController : MonoBehaviour
{
    public static MatchmakingUiController Instance { get; private set; }

    public UnityEvent OnJoinGameClick = new UnityEvent();
    public UnityEvent OnHostGameClick = new UnityEvent();
    public UnityEvent OnBackClick = new UnityEvent();

    [SerializeField] private TMP_InputField _playerName;
    [SerializeField] private TMP_InputField _hostIP;
    [SerializeField] private TMP_InputField _portNum;

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

    public void BackClick()
    {
        OnBackClick.Invoke();
    }

    public void JoinGame()
    {
        OnJoinGameClick.Invoke();

        
        NetworkSessionManager.Instance.AddGameNetworkData(GameNetworkData.ENetMode.Client, _portNum.text, _hostIP.text, _playerName.text);
    }

    public void HostGame()
    {
        OnHostGameClick.Invoke();

        NetworkSessionManager.Instance.AddGameNetworkData(GameNetworkData.ENetMode.Host, _portNum.text, _hostIP.text, _playerName.text);
    }
}
