using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [ReadOnly] protected float currentHp;
    public float CurrentHp
    {
        get { return currentHp; }
        set { currentHp = Math.Clamp(value, 0f, status.CalcuratedMaxHp); }
    }

    [ReadOnly] protected float shield;
    public float Shield
    {
        get { return shield; }
        set { shield = Math.Max(0f, value); }
    }

    [ReadOnly] private float reduce;
    public float Reduce
    {
        get { return reduce; }
        set { reduce = Math.Clamp(value, 0f, 1f); }
    }
    public string targetTag;

    public Equipment equipment;

    [SerializeField, ReadOnly] protected Status status = new Status();
    /// <summary>
    /// 최대 체력을 설정하면, 현재 체력도 바뀐다.
    /// </summary>
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
    protected AttackController attackController = null;
    protected TaskQueue taskQueue = null;                     // child
    protected Animator characterAnimator = null;

    //protected Scanner attackScanner = null;                     // child

    // Gameobject
    protected GameObject target = null;

    virtual protected void Awake()
    {
        movementController ??= GetComponent<MovementController>();
        attackController ??= GetComponent<AttackController>();
        //attackScanner ??= transform.Find("AttackRangeScanner").GetComponent<Scanner>();
        taskQueue ??= GetComponent<TaskQueue>();
        equipment ??= new Equipment();
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

    public void NormalAttack(GameObject target)
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
        float reducedDamage = damage * (100 / (100 + status.Defense)) * (1f - reduce);
        Shield -= reducedDamage;

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