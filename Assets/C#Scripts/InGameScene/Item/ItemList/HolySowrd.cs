using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class HolySword : Item
{
    readonly public float HealAmount;

    Action shieldAction;

    public HolySword(GameObject owner)
        : base(owner)
    {
        shieldAction = () =>
        {
            if (creature.CurrentHp + HealAmount >= creature.Status.Hp)
            {
                //if (ShiledAmount < ShiledMax)
                //{
                //    ShiledAmount += HealAmount;
                //}
            }
            else
            {
                //creature.CurrentHp += HealAmount;
            }
        };

        skill[0] = new LockObject<Action<GameObject>>(owner => {
            movementController.ForceMove(movementController.MousePointDirNorm, 9f, 0.5f);
        }, false);
        skill[1] = new LockObject<Action<GameObject>>(owner => {
            
        });
        skill[2] = new LockObject<Action<GameObject>>(owner => {
            
        });
        skill[3] = new LockObject<Action<GameObject>>(owner => {
            
        });
        skill[4] = new LockObject<Action<GameObject>>(owner => {
            
        });

        HealAmount = 1f;
    }

    public override void AddPassive() 
    {
        creature.attackFunc.Add(shieldAction);
    }
    public override void RemovePassive()
    {
        //creature.Shield -= ShiledAmount;
        creature.attackFunc.Remove(shieldAction);
    }
}
