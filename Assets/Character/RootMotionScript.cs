using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class RootMotionScript : MonoBehaviour
{
    Animator animator = null;
    public int frame = 86;

    private void Awake()
    {
        animator ??= GetComponent<Animator>();
    }

    void OnAnimatorMove()
    {
        if (animator)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.z += animator.GetFloat("Runspeed") * Time.deltaTime;
            transform.localPosition = newPosition;
        }
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // 현재 애니메이션이 Walk이고, 프레임이 마지막이면
        if ((stateInfo.IsName("Walk") && Mathf.FloorToInt(stateInfo.normalizedTime % 1 * frame) == 0) ||
            stateInfo.IsName("Idle"))
        {
            transform.localPosition = GameManager.ChangeZ(transform.localPosition, 0);
        }
    }
}
