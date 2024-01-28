using UnityEngine;
using UnityEngine.Events;

namespace ER
{
    public class SceneFade : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private const string ANIMATOR_BOOL_OUT = "Out";
        private const string ANIMATOR_BOOL_IN = "In";

        public void Out()
        {
            _animator.SetBool(ANIMATOR_BOOL_OUT, false);

            OnOutAnimComplete.Invoke();
        }

        public void In()
        {
            _animator.SetBool(ANIMATOR_BOOL_IN, false);

            OnInAnimComplete.Invoke();
        }

        public UnityEvent OnOutAnimComplete = new UnityEvent();
        public UnityEvent OnInAnimComplete = new UnityEvent();
    }

}
