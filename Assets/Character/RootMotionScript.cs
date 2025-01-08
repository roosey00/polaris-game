using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class RootMotionScript : MonoBehaviour
{
    Animator animator = null;
    public int frame = 81;
    private AnimatorStateInfo stateInfo;

    private void Awake()
    {
        animator ??= GetComponent<Animator>();
    }

    void OnAnimatorMove()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animator)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.z = animator.GetFloat("Runspeed") * Mathf.FloorToInt((stateInfo.normalizedTime % 1f) * frame) * Time.fixedDeltaTime;
            transform.localPosition = newPosition;
        }
        //// 현재 애니메이션이 Walk이고, 프레임이 마지막이면
        //if ((stateInfo.IsName("Walk") &&  ||
        //    stateInfo.IsName("Idle"))
        //{
        //    transform.localPosition = GameManager.ChangeZ(transform.localPosition, 0);

        //}
       //Debug.Log(Mathf.FloorToInt((stateInfo.normalizedTime % 1) * frame));
    }
}
