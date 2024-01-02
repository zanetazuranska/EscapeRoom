using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : InteractableObject
{
    [SerializeField] private List<Material> _materials;
    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public override void OnClick()
    {
        Debug.Log("On Click");
    }

    public override void OnHover()
    {
        _renderer.materials[1].SetFloat("_Scale", 1.03f);
    }

    public override void OnUnHover()
    {
        Debug.Log("Unhover");

        _renderer.materials[1].SetFloat("_Scale", 0f);
    }
}
