using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item : ScriptableObject
{
    string itemName = "None";
    public Sprite ItemImage = null;
    public string rank = "C";
    public Dictionary<string, float> stateDict = new Dictionary<string, float>();

    // 내부의 값을 무조건 초기화 해 줘야됨
    public LockObject<GameManager.voidCreatureFunc>[] skill = new LockObject<GameManager.voidCreatureFunc>[5];

    public float maxHp = 5f;
    public float hp = 5f;
    // 방어력
    public float shiled = 0f;
    public float defense = 5f;
    public float attackDamage = 1f;
    public float attackSpeed = 1f;
    public float attackRange = 1f;
    public float abillityPower = 0f;
    public float critRate = 0f;
    public float critDamage = 1.5f;
    public float attackRate = 0.7f;

    public Item(Creature crt)
    {
    }

    abstract public void AddPassive(Creature crt);
    abstract public void RemovePassive(Creature crt);
}
