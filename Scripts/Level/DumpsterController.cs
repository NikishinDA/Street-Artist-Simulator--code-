using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpsterController : DistractionController
{
    
    [SerializeField] private Animator animator;

    protected override void ActivateEffect()
    {
        animator.SetBool("Fall", true);
        Taptic.Heavy();
    }

    protected override void DeactivateEffect()
    {
        animator.SetBool("Fall", false);

    }
}
