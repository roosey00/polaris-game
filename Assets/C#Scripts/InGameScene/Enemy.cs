using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{   
    public Animator animator;

    new protected void Awake()
    {
        base.Awake();
        if (animator == null)
        {
            animator = gameObject.GetComponentInChildren<Animator>();
        }
        move.slowRotate = true;
        
        attackScanner.target = GameManager.instance.playerObj.transform;
        st = new State(5f, 1f, 2f, 1.0f, 2f);
        _targetTag = "Player";
    }

    protected void LateUpdate()
    {
        animator.SetFloat("speedv", (!move.snaped) ? 1f : 0f);
    }
}
