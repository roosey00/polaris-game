using System;
using System.Collections;
using UnityEngine;

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
[Serializable]
public class Buff
{
    [SerializeField, ReadOnly] private Creature _owner;
    public Creature Owner { get => _owner; set => _owner = value; }

    [SerializeField, ReadOnly] private string _name;
    public string Name { get => _name; set => _name = value; }

    [SerializeField, ReadOnly] private string _description;
    public string Description { get => _description; set => _description = value; }

    [SerializeField, ReadOnly] private float _cooldown;
    public float Cooldown { get => _cooldown; set => _cooldown = value; }

    [Header("Stacks")]
    [SerializeField, ReadOnly] private int _stack;
    public int Stack { get => _stack;
        set
        {
            _stack = Math.Clamp(value, 0, _maxStack);
            _owner.Status += _status * value;
        }
    }

    [SerializeField, ReadOnly] private int _maxStack;
    public int MaxStack { get => _maxStack; set => _maxStack = value; }

    public bool isMaxStack => _stack == _maxStack;

    [SerializeField, ReadOnly] private Status _status;
    public Status Status { get => _status; set => _status = value; }

    [Header("Func")]
    [SerializeField, ReadOnly] private Action<GameObject> _beginAction;
    public Action<GameObject> BeginAction => _beginAction;

    [SerializeField, ReadOnly] private Func<GameObject, IEnumerator> _tickCoroutine;
    public Func<GameObject, IEnumerator> TickCoroutine => _tickCoroutine;

    [SerializeField, ReadOnly] private Action<GameObject> _endAction;
    public Action<GameObject> EndAction => _endAction;

    public Buff(
        string name,
        Status status,
        Creature owner = null,
        int maxStack = 1,
        float cooldown = float.PositiveInfinity,
        Action<GameObject> beginAction = null,
        Func<GameObject, IEnumerator> tickCoroutine = null,
        Action<GameObject> endAction = null,
        string description = "")
    {
        _owner = owner;
        _name = name;
        _status = status;
        _cooldown = cooldown;
        _stack = 1;
        _maxStack = maxStack;
        _beginAction = beginAction ?? (_ => { });
        _tickCoroutine = tickCoroutine ?? (_ => null);
        _endAction = endAction ?? (_ => { });
        _description = description;
    }

    public Buff(Buff other)
    {
        _owner = other._owner;
        _name = other._name;
        _status = new Status(other._status); // 깊은 복사 수행
        _cooldown = other._cooldown;
        _stack = other._stack;
        _maxStack = other._maxStack;
        _beginAction = other._beginAction;
        _tickCoroutine = other._tickCoroutine;
        _endAction = other._endAction;
        _description = other._description;
    }

    /// <summary>
    /// 버프의 지속적인 동작을 처리하는 코루틴
    /// </summary>
    public IEnumerator buffCoroutine(GameObject gameObject)
    {
        for (; _stack > 0; Stack--)
        {
            _beginAction?.Invoke(gameObject);
            yield return new WaitForSeconds(_cooldown);
            _endAction?.Invoke(gameObject);
        }
    }
}