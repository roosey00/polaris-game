using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStatus : Status
{
    // 속도
    new public float Speed
    {
        get { return Mathf.Max(speed, 1f); }
        set { speed = value; }
    }
    protected float speedRate;
    protected float SpeedRate
    {
        get { return Mathf.Max(speedRate, 1f); }
        set { speedRate = value; }
    }
    public float CalcuratedSpeedRate => Mathf.Max(1f, Speed * SpeedRate);

    // 체력 및 보호막
    protected float currentHp;
    public float CurrentHp
    {
        get { return Mathf.Clamp(currentHp, 0f, maxHp); }
        set { currentHp = value; }
    }
    new public float MaxHp
    {
        get { return Mathf.Max(0f, maxHp); }
        set
        {
            currentHp = Mathf.Min(currentHp, maxHp);
            maxHp = value;
        }
    }
    protected float maxHpRate;
    public float MaxHpRate
    {
        get { return maxHpRate; }
        set
        {
            currentHp = Mathf.Min(currentHp + maxHp * (value - maxHpRate), maxHp * value);
            maxHpRate = value;
        }
    }
    public float CalcuratedMaxHp => Mathf.Max(0f, MaxHp * MaxHpRate);
    new public float Shiled
    {
        get { return Mathf.Max(0f, shiled); }
        set { shiled = value; }
    }

    // 방어력
    new public float Defense
    {
        get { return Mathf.Max(0f, defense); }
        set { defense = value; }
    }

    // 기본 공격 및 치명타
    new public float AttackDamage
    {
        get { return Mathf.Max(0f, attackDamage); }
        set { attackDamage = value; }
    }
    new public float AttackSpeed
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
    new public float AttackRange
    {
        get { return Mathf.Max(0f, attackRange); }
        set { attackRange = value; }
    }
    new public float AttackRangeRate
    {
        get { return Mathf.Max(0f, attackRangeRate); }
        set { attackRangeRate = value; }
    }
    public float CalcuratedAttackRange => Mathf.Max(0f, AttackRange * AttackRangeRate);
    new public float CritRate
    {
        get { return Mathf.Clamp(critRate, 0f, 100f); }
        set { critRate = value; }
    }
    new public float CritDamageRate
    {
        get { return Mathf.Max(1f, critDamageRate); }
        set { critDamageRate = value; }
    }

    // 마법 등의 능력치 관련
    new public float AbillityPower
    {
        get { return Mathf.Max(0f, abillityPower); }
        set { abillityPower = value; }
    }
    new public float AbillityPowerRate
    {
        get { return Mathf.Max(0f, abillityPowerRate); }
        set { abillityPowerRate = value; }
    }
    public float CalcuratedAbillityPower => Mathf.Max(0f, AbillityPower * AbillityPowerRate);

    public CreatureStatus(
        float hp = 5f,
        float defense = 5f,
        float attackDamage = 1f,
        float attackSpeed = 1f,
        float attackRange = 2f, 
        float abillityPower = 0f,
        float critRate = 1f,
        float critDamageRate = 2f,
        float attackSpeedRate = 1f,
        float attackRangeRate = 1f,
        float abillityPowerRate = 1f,
        float currentHp = float.PositiveInfinity,
        float shiled = 0f)
        : base(
        hp,                // hp 
        defense,           // defense 
        attackDamage,      // attackDamage 
        attackSpeed,       // attackSpeed 
        attackRange,       // attackRange 
        attackRangeRate,   // attackRangeRate 
        abillityPower,     // abillityPower 
        abillityPowerRate, // abillityPowerRate 
        critRate,          // critRate 
        critDamageRate,    // critDamageRate 
        shiled             // shiled
    )
    {
        this.currentHp = currentHp == float.PositiveInfinity ? hp : currentHp;
        this.attackSpeedRate = attackSpeedRate;
        maxHpRate = 1f;
    }

    public CreatureStatus(CreatureStatus other)
    : base(
        other.MaxHp,                // 부모 클래스의 hp
        other.Defense,           // 부모 클래스의 defense
        other.AttackDamage,      // 부모 클래스의 attackDamage
        other.AttackSpeed,       // 부모 클래스의 attackSpeed
        other.AttackRange,       // 부모 클래스의 attackRange
        other.AttackRangeRate,   // 부모 클래스의 attackRangeRate
        other.AbillityPower,     // 부모 클래스의 abillityPower
        other.AbillityPowerRate, // 부모 클래스의 abillityPowerRate
        other.CritRate,          // 부모 클래스의 critRate
        other.CritDamageRate,    // 부모 클래스의 critDamageRate
        other.Shiled             // 부모 클래스의 shiled
    )
    {
        // 자식 클래스에서만 관리하는 속성 복사
        this.currentHp = other.currentHp;
        this.maxHpRate = other.maxHpRate;
        this.AttackSpeedRate = other.AttackSpeedRate;
    }

    public float TakeDamage(float damage)
    {
        float reducedDamage = damage * (100 / (100 + Defense));
        Shiled -= reducedDamage;

        float finalDamge = reducedDamage - Shiled;
        CurrentHp -= finalDamge;

        return finalDamge;
    }

    public static CreatureStatus operator +(CreatureStatus cs1, CreatureStatus cs2) => new CreatureStatus(
        cs1.maxHp + cs2.maxHp,
        cs1.defense + cs2.defense,
        cs1.attackDamage + cs2.attackDamage,
        cs1.attackSpeed + cs2.attackSpeed,
        cs1.attackRange + cs2.attackRange,
        cs1.abillityPower + cs2.abillityPower,
        cs1.critRate + cs2.critRate,
        cs1.critDamageRate + cs2.critDamageRate,
        cs1.attackSpeedRate + cs2.attackSpeedRate,
        cs1.attackRangeRate + cs2.attackRangeRate,
        cs1.abillityPowerRate + cs2.abillityPowerRate,
        Mathf.Clamp(cs1.currentHp + cs2.currentHp, 0f, cs1.maxHp + cs2.MaxHp),
        cs1.shiled + cs2.shiled);

    public static CreatureStatus operator +(CreatureStatus cs1, Status cs2) => new CreatureStatus(
    cs1.maxHp + cs2.MaxHp,
    cs1.defense + cs2.Defense,
    cs1.attackDamage + cs2.AttackDamage,
    cs1.attackSpeed + cs2.AttackSpeed,
    cs1.attackRange + cs2.AttackRange,
    cs1.abillityPower + cs2.AbillityPower,
    cs1.critRate + cs2.CritRate,
    cs1.critDamageRate + cs2.CritDamageRate,
    cs1.attackSpeedRate,
    cs1.attackRangeRate + cs2.AttackRangeRate,
    cs1.abillityPowerRate + cs2.AbillityPowerRate,
    Mathf.Clamp(cs1.currentHp, 0f, cs1.maxHp + cs2.MaxHp),
    cs1.shiled + cs2.Shiled);


}
