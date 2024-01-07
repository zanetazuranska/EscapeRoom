using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UiRiddleModule : MonoBehaviour
{
    [SerializeField] private Button _arrowUp;
    [SerializeField] private Button _arrowDown;
    [SerializeField] private TextMeshProUGUI _text;

    private int _number = 0;

    public UnityEvent OnValueChanged = new UnityEvent();

    private void Awake()
    {
        _text.text = _number.ToString();

        _arrowUp.onClick.AddListener(OnUpArrowClick);
        _arrowDown.onClick.AddListener(OnDownArrowClick);
    }

    private void OnUpArrowClick()
    {
        _number++;
        if(_number<=9)
        {
            _text.text = _number.ToString();
        }
        else
        {
            _number = 9;
            _text.text = _number.ToString();
        }

        OnValueChanged.Invoke();
    }

    private void OnDownArrowClick()
    {
        _number--;

        if(_number >=0)
        {
            _text.text = _number.ToString();
        }
        else
        {
            _number = 0;
            _text.text = _number.ToString();
        }

        OnValueChanged.Invoke();
    }

    private void OnDestroy()
    {
        _arrowUp.onClick.RemoveListener(OnUpArrowClick);
        _arrowDown.onClick.RemoveListener(OnDownArrowClick);
    }

    public int GetValue()
    {
        return _number;
    }
}
