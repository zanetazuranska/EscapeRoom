using UnityEngine;
using UnityEngine.Events;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Out()
    {
        _animator.SetBool("Out", false);

        OnOutAnimComplete.Invoke();
    }

    public void In()
    {
        _animator.SetBool("In", false);

        OnInAnimComplete.Invoke();
    }

    public UnityEvent OnOutAnimComplete = new UnityEvent();
    public UnityEvent OnInAnimComplete = new UnityEvent();


}
