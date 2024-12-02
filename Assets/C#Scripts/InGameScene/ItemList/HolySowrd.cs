using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HolySword : Item
{
    public HolySword(Creature crt)
        : base(crt)
    {
        skill[0] = new LockObject<GameManager.voidCreatureFunc>(crt => {
            crt.StartCoroutine(crt.move.ForceMove(crt.transform.forward, 10f, 1f));
        }, false);
        skill[1] = new LockObject<GameManager.voidCreatureFunc>(crt => {
            
        });
        skill[2] = new LockObject<GameManager.voidCreatureFunc>(crt => {
            
        });
        skill[3] = new LockObject<GameManager.voidCreatureFunc>(crt => {
            
        });
        skill[4] = new LockObject<GameManager.voidCreatureFunc>(crt => {
            
        });
        stateDict.Add("ShiledMax", 20f);
        stateDict.Add("ShiledAdd", 0f);
    }

    public override void AddPassive(Creature crt) 
    {
        crt.dealFunc.Add(() => {
            if (crt.st.hp >= crt.st.maxHp)
            {
                if (crt.weapon.stateDict["ShiledAdd"] < crt.weapon.stateDict["ShiledMax"])
                {
                    crt.weapon.stateDict["ShiledAdd"]++;
                }
            }
            else
            {
                crt.st.hp++;
            }
        });
    }
    public override void RemovePassive(Creature crt)
    {
        crt.st.hp -= crt.weapon.stateDict["HpAdd"];
    }
}
