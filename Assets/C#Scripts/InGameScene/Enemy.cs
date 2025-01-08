using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{   

    new protected void Awake()
    {
        base.Awake();
        //animator ??= gameObject.GetComponentInChildren<Animator>();
        
        //nav. .slowRotate = true;
        
        target = GameManager.Instance.PlayerObj;
        //attackScanner.Target = GameManager.Instance.PlayerObj.transform;
    }

    private void Start()
    {
        StartCoroutine(TargetFollowCoroutine(1f));
        Status = Status.LoadFromJson("Player", "Data/CreatureData");
    }

    IEnumerator TargetFollowCoroutine(float delay)
    {
        while (true)
        {
            movementController.MoveTo(target.transform.position);
            yield return new WaitForSeconds(delay);
        }
    }
}
