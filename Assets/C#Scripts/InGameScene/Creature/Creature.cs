using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Creature : ComponentInitalizeBehaviour
{
    public string targetTag;

    [SerializeField, ReadOnly] protected Status _status = null;
    /// <summary>
    /// 최대 체력을 설정하면, 현재 체력도 바뀐다.
    /// </summary>
    public Status Status
    {
        set 
        {
            if (value == null) return;
            _status = value;
            _status.CurrentHp = _status.MaxHp;
            hpBarSynchronizer.UpdateHpBar();
        }
        get { return _status; }        
    }

    // 상황별 함수
    [ReadOnly] public List<Action> attackFunc = new List<Action>();
    [ReadOnly] public List<Action> dmgedFunc = new List<Action>();
    [ReadOnly] public List<Action> tickFunc = new List<Action>();

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
    [SerializeField, ReadOnly] protected BaseTaskQueue _taskQueue = null;
    public BaseTaskQueue TaskQueue
    {
        get { return _taskQueue; }
        set { _taskQueue = value; }
    }

    // component
    [Tooltip("캐릭터 이름은 Character로 해야 됩니다.")]
    [ReadOnly] public Animator characterAnimator = null;
    [ReadOnly] public HealthBarSynchronizer hpBarSynchronizer = null;

    // Gameobject
    [SerializeField, ReadOnly] protected GameObject target = null;

    override protected void InitalizeComponent()
    {
        _movementController = new BaseMovementController(GetComponent<Creature>());
        _attackController = new BaseAttackController(this as Creature);
        _taskQueue = new BaseTaskQueue(this as Creature);        
        hpBarSynchronizer ??= GameObject.Find("Canvas_UI").transform.Find("PlayerHealthBar").GetComponent<HealthBarSynchronizer>();
    }

    new protected void Reset()
    {
        base.Reset();
        characterAnimator = transform.Find("Character")?.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
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