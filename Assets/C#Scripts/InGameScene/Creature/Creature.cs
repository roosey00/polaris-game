using System;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public string targetTag;

    public Equipment equipment;
    public CreatureStatus status;

    // 상황별 함수
    public List<Action> attackFunc = null;
    public List<Action> dmgedFunc = null;
    public List<Action> tickFunc = null;

    // component
    protected MovementController movementController = null;
    //protected AttackController attackController = null;
    protected Scanner attackScanner = null;                     // child

    // Gameobject
    protected GameObject target = null;

    virtual protected void Awake()
    {
        movementController = GetComponent<MovementController>();
        //attackController = GetComponent<AttackController>();
        attackScanner = transform.Find("AttackRangeScanner").GetComponent<Scanner>();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (status.CurrentHp <= 0f)
        {
            Die();
        }
        if (tickFunc != null)
        {
            foreach (var func in tickFunc)
            {
                func();
            }
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void Attack(GameObject target)
    {
        // 공격 로직
        if (attackFunc != null)
        {
            foreach (var func in attackFunc)
            {
                func();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        status.TakeDamage(damage);
        if (dmgedFunc != null)
        {
            foreach (var func in dmgedFunc)
            {
                func();
            }
        }
    }

}
