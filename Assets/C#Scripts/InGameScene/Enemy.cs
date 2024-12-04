using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{   
    public Animator animator;

    new protected void Start()
    {
        base.Start();
        if (animator == null)
        {
            animator = gameObject.GetComponentInChildren<Animator>();
        }
        
        //nav. .slowRotate = true;
        
        target = GameManager.Instance.playerObj;
        attackScanner.target = GameManager.Instance.playerObj.transform;
        
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
            nav.SetDestination(target.transform.position);
            yield return new WaitForSeconds(1f);
        }
    }
}
