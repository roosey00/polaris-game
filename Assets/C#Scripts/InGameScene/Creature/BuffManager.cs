using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// ������ ����� ���� ����ü<br />
/// ���� : <br />
///         StatusEffect someBuff = new StatusEffect(<br />
///            name: "�̳� ¯�� ����",<br />
///            stats: new Stats(attackDamage: 9999, abillityPower: 9999), <br />
///            cooldown: float.PositiveInfinity,<br />
///            description: "�ƹ��� �� ���� �� ����"<br />
///            );<br />
/// </summary>
public struct Buff
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

    public Buff(
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

    public Buff(
    string name,
    Buff Other)
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

public class BuffManager : MonoBehaviour
{
    protected Dictionary<string, Buff> statusEffectDict = new Dictionary<string, Buff>();
    public Dictionary<string, Buff> StatusEffectDict => statusEffectDict;

    // components
    Creature creature = null;

    protected void Awake()
    {
        creature = GetComponent<Creature>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">�� ����Ʈ�� �̸�</param>
    /// <param name="Buff">name : ������ ǥ�õ� �̸�, null�� ������ �̸��� �Ȱ��� ����</param>
    /// <returns></returns>
    public string BuffEffect(string name, Buff Buff)
    {
        Buff = Buff.Name == null ? new Buff(name, Buff) : Buff;

        if (statusEffectDict.ContainsKey(name))
        {
            Debug.LogError(gameObject.name + "���� " + name + "��(��) �̹� �ִ� �����Դϴ�!");
        }
        else
        {
            statusEffectDict[name] = Buff;
            creature.Status += Buff.Stats;       // ������ ������ ���Ѵ�.

            StartCoroutine(Buff.removeCoroutine(gameObject));
            Buff.TickCoroutine?.Invoke(gameObject)?.Let(StartCoroutine);
        }
        return name;
    }

    public string RemoveBuff(string name, Buff Buff)
    {
        if (Buff.Name == null)
        {
            Buff = new Buff(name, Buff);
        }
        if (statusEffectDict.ContainsKey(name))
        {
            Debug.LogError(gameObject.name + "���� " + name + "��(��) �̹� �ִ� �����Դϴ�!");
        }
        else
        {
            statusEffectDict[name] = Buff;
            creature.Status += Buff.Stats;       // ������ ������ ���Ѵ�.
        }
        return name;
    }

}
