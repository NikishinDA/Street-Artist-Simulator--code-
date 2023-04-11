using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimationController
{
    private Animator _animator;

    public AgentAnimationController(Animator animator)
    {
        _animator = animator;
    }
    public void Patrol()
    {
       _animator.SetTrigger("Patrol");
    }

    public void Chase()
    {
        _animator.SetTrigger("Chase");
    }

    public void Search()
    {
        _animator.SetTrigger("Search");
    }

    public void Alert()
    {
        _animator.SetTrigger("Alert");
    }

    public void Idle()
    {
        _animator.SetTrigger("Idle");
    }

    public void Defeat()
    {
        _animator.SetTrigger("Defeat");
    }

    public void Dance()
    {
        _animator.SetTrigger("Dance");
    }

    public void Kick()
    {
        _animator.SetTrigger("Kick");
    }
}
