using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Character/Stats")]
public class Stats : ScriptableObject
{
    // 체력 및 보호막
    protected float hp;
    public float Hp => hp;

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

    public Stats(
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
        float shiled = 0f
    )
    {
        this.hp = hp;
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
    }

}
