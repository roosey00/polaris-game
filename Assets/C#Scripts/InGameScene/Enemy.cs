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
        
        target = GameManager.Instance.PlayerObj;
        attackScanner.Target = GameManager.Instance.PlayerObj.transform;

        StartCoroutine(OneSecCoroutine());
    }

    private void Start()
    {
        status = Status.LoadFromJson("Player", "Data/CreatureData");
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
