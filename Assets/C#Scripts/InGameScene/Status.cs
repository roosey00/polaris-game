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
    [Tooltip("최대 체력")]
    [SerializeField, ReadOnly] protected float _maxHp;
    public float MaxHp
    {
        get { return Mathf.Max(0f, _maxHp); }
        set { 
            _maxHp = value;
            CurrentHp += value;
        }
    }
    [Tooltip("체력 비율")]
    [SerializeField, ReadOnly] protected float _hpRate;
    public float HpRate
    {
        get { return _hpRate; }
        set { _hpRate = value; }
    }
    [Tooltip("현재 체력")]
    [SerializeField, ReadOnly] protected float _currentHp;
    public float CurrentHp
    {
        get { return _currentHp; }
        set { _currentHp = Math.Clamp(value, 0f, CalcuratedMaxHp); }
    }
    [Tooltip("계산된 최대 체력")]
    public float CalcuratedMaxHp => Mathf.Max(0f, _maxHp * _hpRate);

    [Tooltip("보호막")]
    [SerializeField, ReadOnly] protected float shield;
    public float Shield
    {
        get { return shield; }
        set { shield = Math.Max(0f, value); }
    }

    // 방어력
    [Tooltip("방어력")]
    [SerializeField, ReadOnly] protected float defense;
    public float Defense
    {
        get { return Mathf.Max(0f, defense); }
        set { defense = value; }
    }
    [Tooltip("피해 감소량")]
    [SerializeField, ReadOnly] private float reduce;
    public float Reduce
    {
        get { return reduce; }
        set { reduce = Math.Clamp(value, 0f, 1f); }
    }
    [Tooltip("기본 공격 피해량")]    
    [SerializeField, ReadOnly] protected float attackDamage;
    public float AttackDamage
    {
        get { return Mathf.Max(0f, attackDamage); }
        set { attackDamage = value; }
    }
    [Tooltip("기본 공격 속도")]
    [SerializeField, ReadOnly] protected float attackSpeed;
    public float AttackSpeed
    {
        get { return Mathf.Max(0f, attackSpeed); }
        set { attackSpeed = value; }
    }
    [Tooltip("기본 공격 속도 비율")]
    [SerializeField, ReadOnly] protected float attackSpeedRate;
    public float AttackSpeedRate
    {
        get { return Mathf.Max(0f, attackSpeedRate); }
        set { attackSpeedRate = value; }
    }
    public float CalcuratedAttackSpeed => Mathf.Max(0f, AttackSpeed * AttackSpeedRate);

    [SerializeField, ReadOnly] protected float attackRange;
    public float AttackRange
    {
        get { return Mathf.Max(attackRange, 0f); }
        set { attackRange = value; }
    }
    [SerializeField, ReadOnly] protected float attackRangeRate;
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
        this._maxHp = hp;
        this._hpRate = hpRate;
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
        this._maxHp = other._maxHp;
        this._hpRate = other._hpRate;
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
cs1._maxHp + cs2.MaxHp,
cs1._hpRate + cs2.HpRate ,
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

    public static Status operator -(Status cs1, Status cs2) => new Status(
cs1._maxHp - cs2.MaxHp,
cs1._hpRate - cs2.HpRate,
cs1.defense - cs2.Defense,
cs1.attackDamage - cs2.AttackDamage,
cs1.attackSpeed - cs2.AttackSpeed,
cs1.attackSpeedRate - cs2.AttackSpeedRate,
cs1.attackRange - cs2.AttackRange,
cs1.attackRangeRate - cs2.AttackRangeRate,
cs1.abillityPower - cs2.AbillityPower,
cs1.abillityPowerRate - cs2.AbillityPowerRate,
cs1.critRate - cs2.CritRate,
cs1.critDamageRate - cs2.CritDamageRate,
cs1.speed - cs2.Speed,
cs1.speedRate - cs2.SpeedRate);

    public static Status operator *(Status cs1, int num) => new Status(
    cs1._maxHp * num,
    cs1._hpRate * num,
    cs1.defense * num,
    cs1.attackDamage * num,
    cs1.attackSpeed * num,
    cs1.attackSpeedRate * num,
    cs1.attackRange * num,
    cs1.attackRangeRate * num,
    cs1.abillityPower * num,
    cs1.abillityPowerRate * num,
    cs1.critRate * num,
    cs1.critDamageRate * num,
    cs1.speed * num,
    cs1.speedRate * num
);


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
