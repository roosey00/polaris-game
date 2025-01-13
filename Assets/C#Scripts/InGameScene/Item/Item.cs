using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item
{
    // 아이템 정보
    public Sprite ItemImage = null;
    public string rank = "C";

    // 전달하고 싶은 Stats
    protected Status itemStats;
    public Status ItemStats => itemStats;

    // 가진 주인
    protected GameObject Owner = null;
    protected Status OwnerStatus = null;

    // Base
    protected BaseMovementController movementController = null;
    protected BaseAttackController attackController = null;

    // component
    protected Creature creature = null;

    // 스킬 함수들
    protected LockObject<Action<GameObject>>[] skill = new LockObject<Action<GameObject>>[5];
    public LockObject<Action<GameObject>>[] Skill => skill;

    public Item(GameObject owner)
    {
        creature ??= owner.GetComponent<Creature>();
        movementController = creature.MovementController;
        attackController = creature.AttackController;
        Owner ??= owner;
    }

    abstract public void NormalAttack();
    abstract public void AddPassive();
    abstract public void RemovePassive();
}