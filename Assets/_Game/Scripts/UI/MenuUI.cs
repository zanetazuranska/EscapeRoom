using UnityEngine;
using UnityEngine.Events;

public class MenuUI : MonoBehaviour
{
    public static MenuUI Instance { get; private set; }

    public UnityEvent OnStartClick = new UnityEvent();

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

    public void StartButton()
    {
        OnStartClick.Invoke();
    }

    public void Options()
    {
        Debug.LogError("Options button not set");
    }

    public void Credits()
    {
        Debug.LogError("Credits button not set");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
