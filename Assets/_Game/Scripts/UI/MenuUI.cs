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
        Debug.Log("Start click");
        OnStartClick.Invoke();
    }

    public void Options()
    {

    }

    public void Credits()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
