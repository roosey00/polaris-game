using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Creature : MonoBehaviour
{
    public Equipment equipment;
    public CreatureStats stats;

    // 상황별 함수
    public List<Action> dealFunc = null;
    public List<Action> dmgedFunc = null;
    public List<Action> tickFunc = null;

    // component
    //protected MovementController movementController = null;
    //protected AttackController attackController = null;
    protected Scanner attackScanner = null;                     // child

    // Gameobject
    protected GameObject target = null;
    protected void Awake()
    {
        //movementController = GetComponent<MovementController>();
        //attackController = GetComponent<AttackController>();
        attackScanner = transform.Find("AttackRangeScanner").GetComponent<Scanner>();
    }

    // Update is called once per frame
    protected void Update()
    {
        Stats temp;
        stats += temp;
        if (stats.CurrentHp <= 0f)
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
    }

    public void TakeDamage(float damage)
    {
        stats.TakeDamage(damage);
    }

}
