using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WinBloomController : MonoBehaviour
{
    [SerializeField] private Animator _cubeAnimator;
    private const string CUBE_ANIMATION_VARIABLE = "CanBloom";

    public void StartBloom()
    {
        _cubeAnimator.SetBool(CUBE_ANIMATION_VARIABLE, true);

        Cursor.lockState = CursorLockMode.None;
    }
}
