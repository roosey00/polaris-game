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
    /// <param name="buff">name : ������ ǥ�õ� �̸�, null�� ������ �̸��� �Ȱ��� ����</param>
    /// <returns></returns>
    public string AddBuff(Buff buff, int stack = 1)
    {
        buff.Name?.Let(name => Debug.LogError($"[AddBuff] {name}�� �� �� ���� �����Դϴ�."));

        if (BuffDict.ContainsKey(buff.Name))
        {
            if (buff.isMaxStack)
            {
                Debug.Log($"{creature.name}���� {buff.Name}�� �ִ�ġ�� {buff.MaxStack}�Դϴ�!");
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
            Debug.LogError($"{creature.name}���� {name}��(��) ���� �����Դϴ�!");
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
            Debug.LogError($"{creature.name}���� {name}��(��) ���� �����Դϴ�!");
            return false;
        }
    }
}
