using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Creature : MonoBehaviour
{
    
    public string targetTag;

    [SerializeField, ReadOnly] protected Status _status = new Status();
    /// <summary>
    /// 최대 체력을 설정하면, 현재 체력도 바뀐다.
    /// </summary>
    public Status Status
    {
        set 
        {            
            _status = value;
            _status.CurrentHp = _status.MaxHp;
        }
        get { return _status; }        
    }

    // 상황별 함수
    public List<Action> attackFunc = null;
    public List<Action> dmgedFunc = null;
    public List<Action> tickFunc = null;

    // nonMonoBehaviour
    [SerializeField, ReadOnly] protected BaseMovementController _movementController = null;
    public BaseMovementController MovementController
    {
        get { return _movementController; }
        set { _movementController = value; }
    }
    [SerializeField, ReadOnly] protected BaseAttackController _attackController = null;
    public BaseAttackController AttackController
    {
        get { return _attackController; }
        set { _attackController = value; }
    }

    // component
    protected TaskQueue taskQueue = null;                     // child
    protected Animator characterAnimator = null;

    //protected Scanner attackScanner = null;                     // child

    // Gameobject
    protected GameObject target = null;

    virtual protected void Awake()
    {
        _movementController = new BaseMovementController(GetComponent<Creature>());
        _attackController = new BaseAttackController(this as Creature);
        //attackScanner ??= transform.Find("AttackRangeScanner").GetComponent<Scanner>();
        taskQueue ??= GetComponent<TaskQueue>();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (_status.CurrentHp <= 0f)
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
        float reducedDamage = damage * (100 / (100 + _status.Defense)) * (1f - _status.Reduce);
        _status.Shield -= reducedDamage;

        float finalDamge = reducedDamage - _status.Shield;
        _status.CurrentHp -= finalDamge;

        if (dmgedFunc != null)
        {
            foreach (var func in dmgedFunc)
            {
                func();
            }
        }
    }
}