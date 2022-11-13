using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MATERIAL
{
    Material_Lancer_Default,
    Material_Lancer_Outline
}

public class SelectionCharacter : MonoBehaviour
{
    private Animator animator;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        skinnedMeshRenderer = Functions.FindChild<SkinnedMeshRenderer>(gameObject, null, true);
    }

    public void SelectCharacter()
    {
        SetAnimation(true);
        SetMaterial(MATERIAL.Material_Lancer_Outline);
    }
    public void ReleaseCharacter()
    {
        SetAnimation(false);
        SetMaterial(MATERIAL.Material_Lancer_Default);
    }

    public void SetAnimation(bool isSelect)
    {
        animator.SetBool("isSelect", isSelect);
    }

    public void SetMaterial(MATERIAL material)
    {
        Managers.ResourceManager.LoadResourceAsync(material.GetEnumName(),
            (Material targetMaterial) => { skinnedMeshRenderer.material = targetMaterial; });
    }
}
