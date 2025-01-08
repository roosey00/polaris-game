using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

//public class Stats
//{
//    public float Hp { get; init; }
//    public float Defense { get; init; }
//    public float AttackDamage { get; init; }
//    public float AttackSpeed { get; init; }
//    public float AttackRange { get; init; }
//    public float AttackRangeRate { get; init; }
//    public float CritRate { get; init; }
//    public float CritDamageRate { get; init; }
//    public float AbilityPower { get; init; }
//    public float AbilityPowerRate { get; init; }
//    public float Shield { get; init; }
//}

/// <summary>
/// 아이템, 버프등으로 추가하기 위한 스탯
/// </summary>
[System.Serializable]
public class Status
{
    // 체력
    [SerializeField, ReadOnly] protected float hp;
    public float Hp
    {
        get { return Mathf.Max(0f, hp); }
        set { hp = value; }
    }
    [ReadOnly] protected float hpRate;
    public float HpRate
    {
        get { return hpRate; }
        set { hpRate = value; }
    }
    [ReadOnly] public float CalcuratedMaxHp => Mathf.Max(0f, hp * hpRate);

    // 방어력
    protected float defense;
    public float Defense
    {
        get { return Mathf.Max(0f, defense); }
        set { defense = value; }
    }
    // 기본 공격
    protected float attackDamage;
    public float AttackDamage
    {
        get { return Mathf.Max(0f, attackDamage); }
        set { attackDamage = value; }
    }

    protected float attackSpeed;
    public float AttackSpeed
    {
        get { return Mathf.Max(0f, attackSpeed); }
        set { attackSpeed = value; }
    }
    protected float attackSpeedRate;
    public float AttackSpeedRate
    {
        get { return Mathf.Max(0f, attackSpeedRate); }
        set { attackSpeedRate = value; }
    }
    public float CalcuratedAttackSpeed => Mathf.Max(0f, AttackSpeed * AttackSpeedRate);

    protected float attackRange;
    public float AttackRange
    {
        get { return Mathf.Max(attackRange, 0f); }
        set { attackRange = value; }
    }
    protected float attackRangeRate;
    public float AttackRangeRate
    {
        get { return Mathf.Max(attackRangeRate, 0f); }
        set { attackRangeRate = value; }
    }
    public float CalcuratedAttackRange => Mathf.Max(0f, AttackRange * AttackRangeRate);

    // 치명타
    protected float critRate;
    public float CritRate
    {
        get { return Mathf.Clamp(critRate, 0f, 100f); }
        set { critRate = value; }
    }
    protected float critDamageRate;
    public float CritDamageRate
    {
        get { return Mathf.Max(critDamageRate, 1f); }
        set { critDamageRate = value; }
    }

    // 마법 등의 능력치 관련
    protected float abillityPower;
    public float AbillityPower
    {
        get { return Mathf.Max(abillityPower, 0f); }
        set { abillityPower = value; }
    }
    protected float abillityPowerRate;
    public float AbillityPowerRate
    {
        get { return Mathf.Max(abillityPowerRate, 0f); }
        set { abillityPowerRate = value; }
    }
    public float AbillityPowerSpeed => Mathf.Max(1f, abillityPower * abillityPowerRate);

    // 속도
    protected float speed;
    public float Speed
    {
        get { return Mathf.Max(speed, 1f); }
        set { speed = value; }
    }
    protected float speedRate;
    public float SpeedRate
    {
        get { return Mathf.Max(speedRate, 1f); }
        set { speedRate = value; }
    }
    public float CalcuratedSpeed => Mathf.Max(1f, speed * speedRate);

    public Status(
        float hp = 0f,
        float hpRate = 0f,
        float defense = 0f,
        float attackDamage = 0f,
        float attackSpeed = 0f,
        float attackSpeedRate = 0f,
        float attackRange = 0f,
        float attackRangeRate = 0f,
        float abillityPower = 0f,
        float abillityPowerRate = 0f,
        float critRate = 0f,
        float critDamageRate = 0f,
        float speed = 1f,
        float speedRate = 1f
    )
    {
        this.hp = hp;
        this.hpRate = hpRate;
        this.defense = defense;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        this.attackSpeedRate = attackSpeedRate;
        this.attackRange = attackRange;
        this.attackRangeRate = attackRangeRate;
        this.critRate = critRate;
        this.critDamageRate = critDamageRate;
        this.abillityPower = abillityPower;
        this.abillityPowerRate = abillityPowerRate;
        this.speed = speed;
        this.speedRate = speedRate;
    }

    // 복사 생성자
    public Status(Status other)
    {
        this.hp = other.hp;
        this.hpRate = other.hpRate;
        this.defense = other.defense;
        this.attackDamage = other.attackDamage;
        this.attackSpeed = other.attackSpeed;
        this.attackSpeedRate = other.attackSpeedRate;
        this.attackRange = other.attackRange;
        this.attackRangeRate = other.attackRangeRate;
        this.critRate = other.critRate;
        this.critDamageRate = other.critDamageRate;
        this.abillityPower = other.abillityPower;
        this.abillityPowerRate = other.abillityPowerRate;
        this.speed = other.Speed;
        this.speedRate = other.SpeedRate;
    }

    public static Status operator +(Status cs1, Status cs2) => new Status(
cs1.hp + cs2.Hp,
cs1.hpRate + cs2.HpRate,
cs1.defense + cs2.Defense,
cs1.attackDamage + cs2.AttackDamage,
cs1.attackSpeed + cs2.AttackSpeed,
cs1.attackSpeedRate + cs2.AttackSpeedRate,
cs1.attackRange + cs2.AttackRange,
cs1.attackRangeRate + cs2.AttackRangeRate,
cs1.abillityPower + cs2.AbillityPower,
cs1.abillityPowerRate + cs2.AbillityPowerRate,
cs1.critRate + cs2.CritRate,
cs1.critDamageRate + cs2.CritDamageRate,
cs1.speed + cs2.Speed,
cs1.speedRate + cs2.SpeedRate);

    [System.Serializable]
    private class StatusJson
    {
        public string name;
        public float hp;
        public float hpRate;
        public float defense;
        public float attackDamage;
        public float attackSpeed;
        public float attackSpeedRate;
        public float attackRange;
        public float attackRangeRate;
        public float abillityPower;
        public float abillityPowerRate;
        public float critRate;
        public float critDamageRate;
        public float speed;
        public float speedRate;
    }

    private class StatusJsonListWrapper
    {
        public List<StatusJson> items = new List<StatusJson>();
    }

    static private Dictionary<string, StatusJsonListWrapper> StatusJsonWrapperDictionary = new Dictionary<string, StatusJsonListWrapper>();

    public static Status LoadFromJson(string name, string path)
    {
        StatusJsonListWrapper statusJsonListWrapper;

        if (!StatusJsonWrapperDictionary.TryGetValue(path, out statusJsonListWrapper))
        {
            TextAsset jsonFile = Resources.Load<TextAsset>(path);

            string wrappedJson = $"{{\"items\" : {jsonFile.text}}}";
            statusJsonListWrapper = StatusJsonWrapperDictionary["path"]
                = JsonUtility.FromJson<StatusJsonListWrapper>(wrappedJson);
        }

        //Debug.Log($"{statusJsonListWrapper.items}");

        foreach (var statusJson in statusJsonListWrapper.items)
        {
            if (statusJson.name == name)
                return new Status(
                    hp : statusJson.hp,
                    hpRate : statusJson.hpRate,
                    defense : statusJson.defense,
                    attackDamage : statusJson.attackDamage,
                    attackSpeed : statusJson.attackSpeed,
                    attackSpeedRate : statusJson.attackSpeedRate,
                    attackRange : statusJson.attackRange,
                    attackRangeRate : statusJson.attackRangeRate,
                    abillityPower : statusJson.abillityPower,
                    abillityPowerRate : statusJson.abillityPowerRate,
                    critRate : statusJson.critRate,
                    critDamageRate : statusJson.critDamageRate,
                    speed : statusJson.speed,
                    speedRate : statusJson.speedRate
                );
        }
        Debug.LogError($"{name} is not in Json!");
        return null;
    }
}
