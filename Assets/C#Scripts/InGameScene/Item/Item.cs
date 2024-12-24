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

    // 내부의 값을 무조건 초기화 해 줘야됨
    public LockObject<Action<GameObject>>[] skill = new LockObject<Action<GameObject>>[5];
    // 가진 주인
    protected GameObject Owner = null;

    // component
    protected MovementController movementController = null;
    protected AttackController attackController = null;
    protected Creature creature = null;

    public Item(GameObject owner)
    {
        movementController = owner.GetComponent<MovementController>();
        attackController = owner.GetComponent<AttackController>();
        creature = owner.GetComponent<Creature>();
        
        Owner = owner;
    }

    abstract public void AddPassive();
    abstract public void RemovePassive();
}