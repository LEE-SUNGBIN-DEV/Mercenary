using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPanel : UIBase
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimation(bool isOpen)
    {
        if (animator != null)
        {
            animator.SetBool("isOpen", isOpen);
        }
    }
}
