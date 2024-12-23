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
[CreateAssetMenu(fileName = "New Stats", menuName = "Character/Stats")]
public class Status : ScriptableObject
{
    // 속도
    protected float speed;
    protected float Speed => speed;

    // 체력 및 보호막
    protected float maxHp;
    public float MaxHp => maxHp;

    protected float shiled;
    public float Shiled => shiled;

    // 방어력
    protected float defense;
    public float Defense => defense;

    // 기본 공격 및 치명타
    protected float attackDamage;
    public float AttackDamage => attackDamage;

    protected float attackSpeed;
    public float AttackSpeed => attackSpeed;

    protected float attackRange;
    public float AttackRange => attackRange;

    protected float attackRangeRate;
    public float AttackRangeRate => attackRangeRate;

    protected float critRate;
    public float CritRate => critRate;

    protected float critDamageRate;
    public float CritDamageRate => critDamageRate;

    // 마법 등의 능력치 관련
    protected float abillityPower;
    public float AbillityPower => abillityPower;

    protected float abillityPowerRate;
    public float AbillityPowerRate => abillityPowerRate;

    public Status(
        float hp = 0f,
        float defense = 0f,
        float attackDamage = 0f,
        float attackSpeed = 0f,
        float attackRange = 0f,
        float attackRangeRate = 0f,
        float abillityPower = 0f,
        float abillityPowerRate = 0f,
        float critRate = 0f,
        float critDamageRate = 0f,
        float shiled = 0f,
        float speed = 1f
    )
    {
        this.maxHp = hp;
        this.shiled = shiled;
        this.defense = defense;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        this.attackRange = attackRange;
        this.attackRangeRate = attackRangeRate;
        this.critRate = critRate;
        this.critDamageRate = critDamageRate;
        this.abillityPower = abillityPower;
        this.abillityPowerRate = abillityPowerRate;
        this.speed = speed;
    }

    // 복사 생성자
    public Status(Status other)
    {
        this.maxHp = other.maxHp;
        this.shiled = other.shiled;
        this.defense = other.defense;
        this.attackDamage = other.attackDamage;
        this.attackSpeed = other.attackSpeed;
        this.attackRange = other.attackRange;
        this.attackRangeRate = other.attackRangeRate;
        this.critRate = other.critRate;
        this.critDamageRate = other.critDamageRate;
        this.abillityPower = other.abillityPower;
        this.abillityPowerRate = other.abillityPowerRate;
        this.speed = other.Speed;
    }
}
