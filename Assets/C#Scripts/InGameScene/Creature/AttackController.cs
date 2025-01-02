using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    //protected float AttackDamage
    //{
    //    set { creature.status.AttackDamage = value; }
    //    get { return creature.status.AttackDamage; }
    //}
    //protected float AttackRate => creature.status.CalcuratedAttackSpeed;
    public bool isAttack = false;

    // component
    protected Enemy creature = null;                     // child
    protected MovementController movementController = null;

    public void Start()
    {
        creature ??= GetComponent<Enemy>();
        movementController ??= GetComponent<MovementController>();
    }

    //// 기본 공격
    //public void PerformAttack(GameObject target)
    //{
    //    if (!isAttack && target != null)
    //    {
    //        StartCoroutine(AttackRoutine(target));
    //    }
    //}

    //// 공격 루틴
    //private IEnumerator AttackRoutine(GameObject target)
    //{
    //    isAttack = true;

    //    // 데미지 처리
    //    var targetCreature = target.GetComponent<Creature>();
    //    if (targetCreature != null)
    //    {
    //        targetCreature.TakeDamage(AttackDamage);
    //    }

    //    yield return new WaitForSeconds(attackCooldown);
    //    isAttack = false;
    //}

    //protected IEnumerator TargetAttack(GameObject target)
    //{
    //    while (isAttack)
    //    {
    //        yield return new WaitForSeconds(stats.AttackSpeed);
    //        if (isAttack)
    //        {
    //            //Debug.Log(target.name);
    //            Dealing(target);
    //            if (target.activeSelf == false)
    //            {
    //                movementController.isMove = true;
    //                break;
    //            }
    //        }
    //        yield return new WaitForSeconds(stats.AttackSpeed);
    //    }
    //}

    // 기본 공격
    public void Attack(float timer, float damage, bool atTrigger, Transform parent)
    {
        if (!isAttack)
        {
            StartCoroutine(RangeAttackCoroutine(timer, damage, atTrigger, parent));
        }
    }

    protected IEnumerator RangeAttackCoroutine(float timer, float damage, bool atTrigger, Transform parent = null)
    {
        if (!isAttack)
        {
            isAttack = true;
            movementController.IsNavMoveMode = false;

            GameObject rngTrigger = Instantiate(GameManager.Instance.RangeTrigger,
            parent.position, parent.rotation, parent);
            RangeAttack triggerClass = rngTrigger.GetComponent<RangeAttack>();
                        
            triggerClass.timer = timer;
            triggerClass.damage = damage;
            triggerClass.isTriggerDmg = true;
            triggerClass.isEndDmg = false;
            yield return new WaitWhile(() => { return rngTrigger != null; });

            movementController.IsNavMoveMode = true;            
            isAttack = false;
        }
    }

    //protected IEnumerator RangeAttack()
    //{
    //    if (!isAttack)
    //    {
    //        isAttack = true;
    //        GameObject rngTrigger = Instantiate(GameManager.Instance.RangeTrigger, 
    //        GameManager.ChangeY(transform.position + transform.forward * 3f, 0f), 
    //        Quaternion.identity);
    //        rngTrigger.GetComponent<RangeAttack>().timer = stats.AttackSpeed;
    //        rngTrigger.GetComponent<RangeAttack>().damage = stats.AttackDamage;
    //        yield return new WaitWhile(() => {return rngTrigger != null;});
    //        isAttack = false;
    //    }
    //}
}
