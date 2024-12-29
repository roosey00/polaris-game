using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Creature : MonoBehaviour
{
    protected float currentHp;
    public float CurrentHp => currentHp;

    protected float shield;
    public float Shield => shield;

    public string targetTag;

    public Equipment equipment;

    private Status status;
    public Status Status
    {
        set 
        {            
            status = value;
            currentHp = status.Hp;
        }
        get { return status; }        
    }

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
        equipment = new Equipment();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (CurrentHp <= 0f)
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
        float reducedDamage = damage * (100 / (100 + status.Defense));
        shield -= reducedDamage;

        float finalDamge = reducedDamage - shield;
        currentHp -= finalDamge;

        if (dmgedFunc != null)
        {
            foreach (var func in dmgedFunc)
            {
                func();
            }
        }
    }
}