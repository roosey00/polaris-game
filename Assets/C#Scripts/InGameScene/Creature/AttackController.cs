using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Stats stats;

    public float attackCooldown = 1f;
    public bool isAttack = false;

    // component
    protected MovementController movementController = null;

    // 기본 공격
    public void PerformAttack(GameObject target)
    {
        if (!isAttack && target != null)
        {
            StartCoroutine(AttackRoutine(target));
        }
    }

    // 공격 루틴
    private IEnumerator AttackRoutine(GameObject target)
    {
        isAttack = true;

        // 데미지 처리
        var targetCreature = target.GetComponent<Creature>();
        if (targetCreature != null)
        {
            targetCreature.TakeDamage(stats.AttackDamage);
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttack = false;
    }

    protected IEnumerator TargetAttack(GameObject target)
    {
        while (isAttack)
        {
            yield return new WaitForSeconds(stats.AttackSpeed);
            if (isAttack)
            {
                //Debug.Log(target.name);
                Dealing(target);
                if (target.activeSelf == false)
                {
                    movementController.isMove = true;
                    break;
                }
            }
            yield return new WaitForSeconds(stats.AttackSpeed);
        }
    }

    //protected IEnumerator RangeAttack(GameObject target, Transform obj = null)
    //{
    //    if (!isAttack)
    //    {
    //        movementController.isMove = false;
    //        isAttack = true;
    //        GameObject rngTrigger = Instantiate(GameManager.Instance.RangeTrigger, 
    //        GameManager.ChangeY(transform.position + transform.forward * 3f, 0f), 
    //        Quaternion.identity);
    //        rngTrigger.transform.SetParent(obj);
    //        rngTrigger.GetComponent<RangeAttack>().timer = stats.AttackSpeed;
    //        rngTrigger.GetComponent<RangeAttack>().damage = stats.AttackDamage;
    //        yield return new WaitWhile(() => {return rngTrigger != null;});
    //        if (target.activeSelf == false) 
    //        {
    //            movementController.isMove = true;
    //        }
    //        isAttack = false;
    //    }
    //}

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
