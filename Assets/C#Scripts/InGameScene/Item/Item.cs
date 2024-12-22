using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item : ScriptableObject
{
    Creature Owner = null;
    string itemName = "None";
    public Sprite ItemImage = null;
    public string rank = "C";
    public Dictionary<string, float> stateDict = new Dictionary<string, float>();

    // 내부의 값을 무조건 초기화 해 줘야됨
    public LockObject<Action<Creature>>[] skill = new LockObject<Action<Creature>>[5];

    // 전달하고 싶은 Stats
    public Stats stats;

    public Item(Creature crt)
    {
        Owner = crt;
    }

    abstract public void AddPassive(Creature crt);
    abstract public void RemovePassive(Creature crt);
}