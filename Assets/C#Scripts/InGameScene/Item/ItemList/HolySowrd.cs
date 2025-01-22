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
    Buff getShield;

    public HolySword(GameObject owner)
        : base(owner)
    {
        getShield = new Buff(name: "성스러운 보호막", status:new Status { Shield = 1 }, maxStack:20 );
        shieldAction = () =>
        {
            if (creature.Status.CurrentHp + HealAmount >= creature.Status.MaxHp)
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
            GameManager.Instance.PlayerClass.TaskQueue.EnqueueTaskOnIdle(() => {
                movementController.ForceMove(movementController.MousePointDirNorm, 9f, 0.5f);
                attackController.Attack(0.3f, creature.Status.AttackDamage * 1.0f, creature.Status.AttackRange, "Enemy",
                    angleRange:180f, parent: owner.transform);
                creature.Status.Shield += 1;
            }, 0.5f);
        }, false);
        skill[1] = new LockObject<Action<GameObject>>(owner => {
            GameManager.Instance.PlayerClass.TaskQueue.EnqueueTaskOnIdle(() => {
                movementController.ForceMove(movementController.MousePointDirNorm, 0f, 0.5f);
            }, 0.5f);
        }, false);
        skill[2] = new LockObject<Action<GameObject>>(owner => {
            GameManager.Instance.PlayerClass.TaskQueue.EnqueueTaskOnIdle(() => {
                movementController.ForceMove(movementController.MousePointDirNorm, 0f, 0.5f);
            }, 0.5f);
            GameManager.Instance.PlayerClass.TaskQueue.EnqueueTask(() => {
                attackController.Attack(3f, creature.Status.AttackDamage * 3.0f, 5f, "Enemy",
                    damageTimer:0.5f);
            }, 3f);
        }, false);
        skill[3] = new LockObject<Action<GameObject>>(owner => {
            
        }, false);
        skill[4] = new LockObject<Action<GameObject>>(owner => {
            
        }, false);

        HealAmount = 1f;
    }

    public override void NormalAttack()
    {
        GameManager.Instance.PlayerClass.TaskQueue.EnqueueTaskOnIdle(() => {            
            attackController.Attack(0.3f, creature.Status.AttackDamage * 1.0f, creature.Status.AttackRange, "Enemy",
                angleRange: 180f, parent: Owner.transform);
        }, 0.5f);
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
