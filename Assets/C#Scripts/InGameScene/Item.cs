using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item : ScriptableObject
{
    string itemName = "None";
    public Sprite ItemImage = null;
    public string rank = "C";
    public Dictionary<string, float> stateDict = new Dictionary<string, float>();

    // 내부의 값을 무조건 초기화 해 줘야됨
    public LockObject<GameManager.voidCreatureFunc>[] skill = new LockObject<GameManager.voidCreatureFunc>[5];

    State st = new State();
    public float attackRate = 0.7f;

    public Item(Creature crt)
    {
        crt.st += st;
    }

    abstract public void AddPassive(Creature crt);
    abstract public void RemovePassive(Creature crt);
}
