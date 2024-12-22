using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStats : Stats
{
    // 체력 및 보호막
    protected float currentHp;
    public float CurrentHp
    {
        get { return Mathf.Clamp(currentHp, 0f, hp); }
        set { currentHp = value; }
    }
    new public float Hp
    {
        get { return Mathf.Max(0f, hp); }
        set
        {
            currentHp = Mathf.Min(currentHp, hp);
            hp = value;
        }
    }
    protected float maxHpRate;
    public float MaxHpRate
    {
        get { return maxHpRate; }
        set
        {
            currentHp = Mathf.Min(currentHp + hp * (value - maxHpRate), hp * value);
            maxHpRate = value;
        }
    }
    public float CalcuratedMaxHp => Mathf.Max(0f, Hp * MaxHpRate);
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

    public CreatureStats(
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
        currentHp = hp;
        maxHpRate = 1f;
        AttackSpeedRate = AttackSpeedRate;
    }

    public CreatureStats(CreatureStats other)
    : base(
        other.Hp,                // 부모 클래스의 hp
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

    public static CreatureStats operator +(CreatureStats cs1, CreatureStats cs2)
    {
        return new CreatureStats(
        cs1.hp + cs2.hp,
        cs1.defense + cs2.defense,
        cs1.attackDamage + cs2.attackDamage,
        cs1.attackSpeed + cs2.attackSpeed,
        cs1.attackRange + cs2.attackRange,
        cs1.abillityPower + cs1.abillityPower,
        cs1.critRate + cs2.critRate,
        cs1.critDamageRate + cs2.critDamageRate,
        cs1.attackSpeedRate + cs2.attackSpeedRate,
        cs1.attackRangeRate + cs2.attackRangeRate,
        cs1.abillityPowerRate + cs2.abillityPowerRate,
        cs1.shiled + cs2.shiled);
    }

    //public static implicit operator CreatureStats(Stats st)
    //{
    //    // Convert Stats to CreatureStats
    //    return new CreatureStats
    //    {
    //        // Example: Assume CreatureStats has properties that correspond to Stats.
    //        Strength = st.Strength,
    //        Agility = st.Agility,
    //        Intelligence = st.Intelligence
    //    };
    //}


}
