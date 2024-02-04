using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private GameObject _lightHolder;
    private bool _isFlickering = false;
    private float _timeDelay;

    void Update()
    {
        if(!_isFlickering)
        {
            StartCoroutine(FlickeringLightCoroutine());
        }
    }

    private IEnumerator FlickeringLightCoroutine()
    {
        _isFlickering = true;
        _lightHolder.SetActive(false);
        _timeDelay = Random.Range(0.01f, 0.4f);
        yield return new WaitForSeconds(_timeDelay);

        _lightHolder.SetActive(true);
        _timeDelay = Random.Range(0.01f, 0.4f);
        yield return new WaitForSeconds(_timeDelay);

        _isFlickering = false;
    }
}
