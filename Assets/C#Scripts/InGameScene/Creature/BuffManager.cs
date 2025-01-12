using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[Serializable]
public class BuffManager
{
    protected Dictionary<string, Buff> BuffDict = new Dictionary<string, Buff>();
    public Dictionary<string, Buff> StatusEffectDict => BuffDict;

    // components
    private Creature creature = null;

    protected void Awake(Creature creature)
    {
        this.creature = creature;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buff">name : 버프에 표시될 이름, null을 넣으면 이름을 똑같이 설정</param>
    /// <returns></returns>
    public string AddBuff(Buff buff, int stack = 1)
    {
        buff.Name?.Let(name => Debug.LogError($"[AddBuff] {name}은 알 수 없는 버프입니다."));

        if (BuffDict.ContainsKey(buff.Name))
        {
            if (buff.isMaxStack)
            {
                Debug.Log($"{creature.name}에게 {buff.Name}의 최대치는 {buff.MaxStack}입니다!");
            }
            else
            {
                buff.Stack += stack;
            }
        }
        else
        {
            BuffDict[buff.Name] = buff;

            creature.StartCoroutine(buff.buffCoroutine(creature.gameObject));
            buff.TickCoroutine?.Invoke(creature.gameObject)?.Let(creature.StartCoroutine);
        }
        return buff.Name;
    }

    public Buff DecreaseBuff(string name, int stack = 1)
    {
        if (BuffDict.ContainsKey(name))
        {
            BuffDict[name].Stack -= stack;
            if (BuffDict[name].Stack <= 0)
            {
                RemoveBuff(name);
            }
        }
        else
        {
            Debug.LogError($"{creature.name}에게 {name}은(는) 없는 버프입니다!");
        }
        return BuffDict[name];
    }

    public bool RemoveBuff(string name)
    {
        if (BuffDict.ContainsKey(name))
        {
            BuffDict[name].Stack -= BuffDict[name].Stack;
            return BuffDict.Remove(name);
        }
        else
        {
            Debug.LogError($"{creature.name}에게 {name}은(는) 없는 버프입니다!");
            return false;
        }
    }
}
