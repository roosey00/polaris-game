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
        
        //nav. .slowRotate = true;
        
        target = GameManager.Instance.playerObj;
        attackScanner.Target = GameManager.Instance.playerObj.transform;
        
        //st = new State(5f, 1f, 2f, 1.0f, 2f);\

        StartCoroutine("OneSecCoroutine");
    }

    protected void LateUpdate()
    {
        
    }

    IEnumerator OneSecCoroutine()
    {
        while (true)
        {
            movementController.MoveTo(target.transform.position);
            yield return new WaitForSeconds(1f);
        }
    }
}
