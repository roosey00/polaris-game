using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class BaseAttackController
{
    private Creature creature;

    //protected float AttackDamage
    //{
    //    set { creature.status.AttackDamage = value; }
    //    get { return creature.status.AttackDamage; }
    //}
    //protected float AttackRate => creature.status.CalcuratedAttackSpeed;
    [ReadOnly] public bool isAttack = false;

    // component              // child
    protected BaseMovementController movementController = null;

    public BaseAttackController(Creature creature)
    {
        this.creature = creature;
        movementController = creature.MovementController;
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
    public void Attack(float timer, float damage, float range, string targetTag, float angleRange = 360f, Transform parent = null, float damageTimer = 0f)
    {
        if (!isAttack)
        {
            creature.StartCoroutine(RangeAttackCoroutine(timer, damage, range, targetTag, angleRange, parent, damageTimer));
        }
    }

    protected IEnumerator RangeAttackCoroutine(float timer, float damage, float range, string targetTag, 
        float angleRange = 360f, Transform parent = null, float damageTimer = 0f)
    {
        if (!isAttack)
        {
            isAttack = true;
            movementController.IsNavMove = false;

            GameObject rngTrigger = UnityEngine.Object.Instantiate(GameManager.Instance.RangeTrigger,
            (parent ?? creature.transform).position, (parent ?? creature.transform).rotation, (parent ?? GameManager.Instance.rootTransform));
            RangeAttack triggerClass = rngTrigger.GetComponent<RangeAttack>();
                        
            triggerClass.Timer = timer;
            triggerClass.Damage = damage;
            triggerClass.Range = range;
            triggerClass.AngleRange = angleRange;
            triggerClass.DamageTimer = damageTimer;
            triggerClass.targetTag = "Enemy";
            yield return new WaitWhile(() => { return rngTrigger != null; });

            movementController.IsNavMove = true;            
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
