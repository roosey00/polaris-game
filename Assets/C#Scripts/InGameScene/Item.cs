using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item : ScriptableObject
{
    Creature Owner = null;
    string itemName = "None";
    public Sprite ItemImage = null;
    public string rank = "C";
    public Dictionary<string, float> stateDict = new Dictionary<string, float>();

    // 내부의 값을 무조건 초기화 해 줘야됨
    public LockObject<Action<Creature>>[] skill = new LockObject<Action<Creature>>[5];

    // 체력
    private float hp = 5f;
    public float Hp => hp; // 읽기 전용 프로퍼티

    // 방어력 관련
    private float shield = 0f;
    public float Shield => shield;

    private float defense = 5f;
    public float Defense => defense;

    // 공격 관련
    private float attackDamage = 1f;
    public float AttackDamage => attackDamage;

    private float attackSpeed = 1f;
    public float AttackSpeed => attackSpeed;

    private float attackRange = 1f;
    public float AttackRange => attackRange;

    private float abilityPower = 0f;
    public float AbilityPower => abilityPower;

    // 치명타 관련
    private float critRate = 0f;
    public float CritRate => critRate;

    private float critDamage = 1.5f;
    public float CritDamage => critDamage;

    private float attackRate = 0.7f;
    public float AttackRate => attackRate;

    public Item(Creature crt)
    {
        Owner = crt;
    }

    abstract public void AddPassive(Creature crt);
    abstract public void RemovePassive(Creature crt);
}
    