using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public struct State
{
    public float maxHp;
    public float hp;
    public float shiled;
    public float defense;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float abillityPower;
    public float speed;
    public float critRate;
    public float critDamage;

    public State(float maxHp = 5f
    , float defense = 0f
    , float shiled = 0f
    , float attackDamage = 2f
    , float attackSpeed = 1.0f
    , float attackRange = 2f
    , float abillityPower = 0f
    , float speed = 0f
    , float critRate = 0f
    , float critDamage = 0f)
    {
        this.maxHp = this.hp = maxHp;
        this.shiled = shiled;
        this.defense = defense;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        this.attackRange = attackRange;
        this.abillityPower = abillityPower;
        this.speed = speed;
        this.critRate = critRate;
        this.critDamage = critDamage;
    }
    public static State operator +(State st1, State st2)
    {
        return new State(
        st1.maxHp + st2.maxHp,
        st1.shiled + st2.shiled,
        st1.defense + st2.defense,
        st1.attackDamage + st2.attackDamage,
        st1.attackSpeed + st2.attackSpeed,
        st1.attackRange + st2.attackRange,
        st1.abillityPower + st2.abillityPower,
        st1.speed + st2.speed,
        st1.critRate + st2.critRate,
        st1.critDamage + st2.critDamage);
    }
}

public enum AttackType
{
    None, Target, Range, Spawn
}

public class Creature : MonoBehaviour
{
    public Moveable move;
    public Scanner attackScanner = null;

    public Item weapon;
    public Item helmet;
    public Item armor;
    public Item pants;
    public Item shoes;
    public Item ring;
    public Item necklace;
    public Item earring;

    public List<GameManager.voidvoidFunc> dealFunc = null;
    public List<GameManager.voidvoidFunc> dmgedFunc = null;
    public List<GameManager.voidvoidFunc> tickFunc = null;

    /// <summary>
    /// State
    /// </summary>

    protected GameObject target;
    public string targetTag
    {
        get
        {
            return _targetTag;
        }
    }
    protected string _targetTag;
    
    public State st;

    protected RaycastHit hit;
    protected bool isAttack = false;


    protected void Awake()
    {
        if (move == null)
        {
            move = gameObject.GetComponentInChildren<Moveable>();
        }
    } 
    // Update is called once per frame
    protected void Update()
    {
        if (st.hp <= 0f)
        {
            gameObject.SetActive(false);
        }
        if (tickFunc != null)
        {
            foreach (var func in tickFunc)
            {
                func();
            }
        }
    }

    public float GetDamage(float dmg, float defRate = 1.0f)
    {
        float finalDamage = dmg * (100 / (100 + st.defense * defRate));
        float shiledReduce = finalDamage - st.shiled;
        if (st.shiled > 0f)
        {
            st.shiled -= finalDamage;
        }
        if (st.shiled <= 0f)
        {
            st.shiled = 0f;
            st.hp -= shiledReduce;
            if (dmgedFunc != null)
            {
                foreach (var func in dmgedFunc)
                {
                    func();
                }
            }
        }        
        return finalDamage;
    }

    public float Dealing(GameObject obj)
    {
        float finalDamage = st.attackDamage;
        target.GetComponent<Creature>().GetDamage(finalDamage);
        if (dmgedFunc != null)
        {
            foreach (var func in dealFunc)
            {
                func();
            }
        }
        return finalDamage;
    }

    protected void SetAttackMode()
    {
        target = hit.transform.gameObject;
        move.targetPos = hit.collider.transform.position;    
    }

    protected void SetMoveMode()
    {
        move.isPause = false;
    }

    protected IEnumerator TargetAttack(GameObject target)
    {   
        move.isPause = true;
        isAttack = true;
        while (isAttack)
        {
            yield return new WaitForSeconds(st.attackSpeed * weapon.attackRate);
            if (isAttack)
            {
                Debug.Log(target.name);
                Dealing(target);
                if (target.activeSelf == false) 
                {
                    move.isPause = false;
                    break;
                }
            }
            yield return new WaitForSeconds(st.attackSpeed * (1 - weapon.attackRate));
        }
        SetMoveMode();
    }

    protected IEnumerator RangeAttack(GameObject target)
    {
        if (!isAttack)
        {
            move.isPause = true;
            isAttack = true;
            GameObject rngTrigger = Instantiate(GameManager.instance.RangeTrigger, 
            Vector3Modifier.ChangeY(transform.position + transform.forward * 3f, 0f), 
            Quaternion.identity);
            rngTrigger.GetComponent<RangeAttack>().timer = st.attackSpeed * weapon.attackRate;
            rngTrigger.GetComponent<RangeAttack>().damage = st.attackDamage;
            yield return new WaitWhile(() => {return rngTrigger != null;});
            if (target.activeSelf == false) 
            {
                SetMoveMode();
            }
            isAttack = false;
        }
    }

    protected IEnumerator RangeAttack()
    {
        if (!isAttack)
        {
            isAttack = true;
            GameObject rngTrigger = Instantiate(GameManager.instance.RangeTrigger, 
            Vector3Modifier.ChangeY(transform.position + transform.forward * 3f, 0f), 
            Quaternion.identity);
            rngTrigger.GetComponent<RangeAttack>().timer = st.attackSpeed * weapon.attackRate;
            rngTrigger.GetComponent<RangeAttack>().damage = st.attackDamage;
            yield return new WaitWhile(() => {return rngTrigger != null;});
            isAttack = false;
        }
    }
}
