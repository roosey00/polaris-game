using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Creature : MonoBehaviour
{
    // 아이템
    public Item weapon = null;
    public Item Weapon;
    public Item helmet = null;
    public Item armor = null;
    public Item pants = null;
    public Item shoes = null;
    public Item ring = null;
    public Item necklace = null;
    public Item earring = null;

    // 스탯
    public float maxHp = 5f;
    public float hp = 5f;
    // 방어력
    public float shiled = 0f;
    public float defense = 5f;
    public float attackDamage = 1f;
    public float attackSpeed = 1f;
    public float attackRange = 1f;
    public float abillityPower = 0f;
    public float critRate = 0f;
    public float critDamage = 1.5f;

    // 상황별 함수
    public List<Action> dealFunc = null;
    public List<Action> dmgedFunc = null;
    public List<Action> tickFunc = null;

    // component
    protected NavMeshAgent nav = null;

    // child
    public Scanner attackScanner = null;

    // Gameobject
    protected GameObject target = null;

    // Tag
    public string targetTag;

    // Flag
    protected bool isAttack = false;

    const float FAST_ROTATION_SPEED = 10000f;
    protected bool isMove {
        set {
            nav.enabled = value;
            //nav.angularSpeed = nav.acceleration = value ? FAST_ROTATION_SPEED : 0f;
        }
        get{ return nav.enabled;}//return nav.acceleration == FAST_ROTATION_SPEED; }
    }

    public Vector3 MousePointDirNorm
    {
        get { return (GameManager.Instance.groundMouseHit.MousePos - transform.position).normalized;}
    }

    // Class Global

    private RaycastHit hit;

    protected void Start()
    {
        if (nav == null)
        {
            nav = gameObject.GetComponentInChildren<NavMeshAgent>();
        }
        if (attackScanner == null)
        {
            attackScanner = transform.Find("AttackRangeScanner").GetComponent<Scanner>();
        }
    } 

    // Update is called once per frame
    protected void Update()
    {
        if (hp <= 0f)
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

    public float GetDamage(float dmg)
    {
        float finalDamage = dmg * (100 / (100 + defense));
        float shiledReduce = finalDamage - shiled;
        if (shiled > 0f)
        {
            shiled -= finalDamage;
        }
        if (shiled <= 0f)
        {
            shiled = 0f;
            hp -= shiledReduce;
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
        float finalDamage = attackDamage;
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

    public void ForceMove(Vector3 dir, float speed, float timer)
    {
        if (isMove)
        {
            StartCoroutine(ForceMoveCoroutine(dir, speed, timer));
        }
    }

    protected IEnumerator ForceMoveCoroutine(Vector3 dir, float speed, float timer)
    {
        isMove = false;
        dir = Vector3Modifier.ChangeY(dir, 0);
        transform.LookAt(transform.position + dir);
        // 루프 시작
        while (timer > 0)
        {
            // 이동
            transform.position += dir.normalized * speed * Time.fixedDeltaTime;
            //transform.Translate(Vector3Modifier.ChangeY(dir, 0).normalized * speed * Time.fixedDeltaTime);
            
            // 타이머 감소
            timer -= Time.fixedDeltaTime;

            // 프레임 대기
            yield return new WaitForFixedUpdate(); // Time.deltaTime 주기로 갱신
        }
        //nav.ResetPath();
        isMove = true;
    }

    protected IEnumerator TargetAttack(GameObject target)
    {   
        nav.enabled = false;
        isAttack = true;
        while (isAttack)
        {
            yield return new WaitForSeconds(attackSpeed * weapon.AttackRate);
            if (isAttack)
            {
                Debug.Log(target.name);
                Dealing(target);
                if (target.activeSelf == false) 
                {
                    nav.enabled = true;
                    break;
                }
            }
            yield return new WaitForSeconds(attackSpeed * (1 - weapon.AttackRate));
        }
        isMove = true;
    }

    protected IEnumerator RangeAttack(GameObject target, Transform obj = null)
    {
        if (!isAttack)
        {
            nav.enabled = false;
            isAttack = true;
            GameObject rngTrigger = Instantiate(GameManager.Instance.RangeTrigger, 
            Vector3Modifier.ChangeY(transform.position + transform.forward * 3f, 0f), 
            Quaternion.identity);
            rngTrigger.transform.SetParent(obj);
            rngTrigger.GetComponent<RangeAttack>().timer = attackSpeed;
            rngTrigger.GetComponent<RangeAttack>().damage = attackDamage;
            yield return new WaitWhile(() => {return rngTrigger != null;});
            if (target.activeSelf == false) 
            {
                isMove = true;
            }
            isAttack = false;
        }
    }

    protected IEnumerator RangeAttack()
    {
        if (!isAttack)
        {
            isAttack = true;
            GameObject rngTrigger = Instantiate(GameManager.Instance.RangeTrigger, 
            Vector3Modifier.ChangeY(transform.position + transform.forward * 3f, 0f), 
            Quaternion.identity);
            rngTrigger.GetComponent<RangeAttack>().timer = attackSpeed;
            rngTrigger.GetComponent<RangeAttack>().damage = attackDamage;
            yield return new WaitWhile(() => {return rngTrigger != null;});
            isAttack = false;
        }
    }
}
