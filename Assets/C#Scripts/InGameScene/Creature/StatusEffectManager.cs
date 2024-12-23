using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 버프나 디버프 내용 구조체<br />
/// 사용법 : <br />
///         StatusEffect someBuff = new StatusEffect(<br />
///            name: "겁나 짱샌 버프",<br />
///            stats: new Stats(attackDamage: 9999, abillityPower: 9999), <br />
///            cooldown: float.PositiveInfinity,<br />
///            description: "아무도 날 막을 수 없어"<br />
///            );<br />
/// </summary>
public struct StatusEffect
{
    private string name;
    public string Name => name;

    private string description;
    public string Description => description;

    private float cooldown;
    public float Cooldown => cooldown;

    private Status stats;
    public Status Stats => stats;

    private Action<GameObject> beginAction;
    public Action<GameObject> BeginAction => beginAction;

    private Func<GameObject, IEnumerator> tickCoroutine;
    public Func<GameObject, IEnumerator> TickCoroutine => tickCoroutine;

    private Action<GameObject> endAction;
    public Action<GameObject> EndAction => endAction;

    public StatusEffect(
        string name, 
        Status stats,
        float cooldown = float.PositiveInfinity, 
        Action<GameObject> beginAction = null,
        Func<GameObject, IEnumerator> tickAction = null,
        Action<GameObject> endAction = null,
        string description = "")
    {
        this.name = name;
        this.stats = stats;
        this.cooldown = cooldown;
        this.beginAction = beginAction;
        this.tickCoroutine = tickAction;
        this.endAction = endAction;
        this.description = description;
    }

    public StatusEffect(
    string name,
    StatusEffect Other)
    {
        this.name = name;
        this.stats = Other.stats;
        this.cooldown = Other.cooldown;
        this.beginAction = Other.beginAction;
        this.tickCoroutine = Other.tickCoroutine;
        this.endAction = Other.endAction;
        this.description = Other.description;
    }

    public IEnumerator removeCoroutine(GameObject gameObject)
    {
        beginAction?.Invoke(gameObject);
        yield return new WaitForSeconds(cooldown);
        endAction?.Invoke(gameObject);
    }
}

public class StatusEffectManager : MonoBehaviour
{
    protected Dictionary<string, StatusEffect> statusEffectDict = new Dictionary<string, StatusEffect>();
    public Dictionary<string, StatusEffect> StatusEffectDict => statusEffectDict;

    // components
    Creature creature = null;

    protected void Awake()
    {
        creature = GetComponent<Creature>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">이 이펙트에 이름</param>
    /// <param name="Buff">name : 버프에 표시될 이름, null을 넣으면 이름을 똑같이 설정</param>
    /// <returns></returns>
    public string AddStatusEffect(string name, StatusEffect Buff)
    {
        Buff = Buff.Name == null ? new StatusEffect(name, Buff) : Buff;

        if (statusEffectDict.ContainsKey(name))
        {
            Debug.LogError(gameObject.name + "에게 " + name + "은(는) 이미 있는 버프입니다!");
        }
        else
        {
            statusEffectDict[name] = Buff;
            creature.status += Buff.Stats;       // 버프의 스탯을 더한다.

            StartCoroutine(Buff.removeCoroutine(gameObject));
            Buff.TickCoroutine?.Invoke(gameObject)?.Let(StartCoroutine);
        }
        return name;
    }

    public string RemoveStatusEffect(string name, StatusEffect Buff)
    {
        if (Buff.Name == null)
        {
            Buff = new StatusEffect(name, Buff);
        }
        if (statusEffectDict.ContainsKey(name))
        {
            Debug.LogError(gameObject.name + "에게 " + name + "은(는) 이미 있는 버프입니다!");
        }
        else
        {
            statusEffectDict[name] = Buff;
            creature.status += Buff.Stats;       // 버프의 스탯을 더한다.
        }
        return name;
    }

}
