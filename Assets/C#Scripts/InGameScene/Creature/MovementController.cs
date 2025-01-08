using System;
using System.Collections;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private NavMeshAgent nav;
    [ReadOnly] public Vector3 destination = Vector3.zero;

    const float FAST_ROTATION_SPEED = 10000f;
    public bool isMove => nav.velocity != Vector3.zero || isForceMove;
    public bool IsNavMove
    {
        get
        {
            return !nav.isStopped;
        }
        set
        {
            nav.isStopped = !value;
        }
    }
    private bool isForceMove = false;


    public Vector3 MousePointDirNorm => (GameManager.Instance.GroundMouseHit.MousePos - transform.position).normalized;

    private void Awake()
    {
        nav ??= GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// 특정 위치로 이동
    /// null일 경우, 기존 목적지가 있을 경우 이동
    /// </summary>
    /// <param name="destination">목적지</param>
    public void MoveTo(Vector3? destination = null)
    {
        if (IsNavMove)
        {
            this.destination = destination ?? this.destination;

            if (this.destination != null)
            {
                nav.SetDestination(this.destination);
            }
        }
    }

    /// <summary>
    /// 강제 이동
    /// </summary>
    /// <param name="dir">방향</param>
    /// <param name="speed">속도</param>
    /// <param name="timer">이동할 시간</param>
    public void ForceMove(Vector3 dir, float speed, float timer)
    {
        if (IsNavMove)
        {
            StartCoroutine(ForceMoveCoroutine(dir, speed, timer));
        }
    }

    protected IEnumerator ForceMoveCoroutine(Vector3 dir, float speed, float timer)
    {
        IsNavMove = false;
        isForceMove = true;
        dir = GameManager.ChangeZ(dir, 0);
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
        nav.ResetPath();
        isForceMove = false;
        IsNavMove = true;
    }

    public void StopMoveInSec(float timer)
    {
        if (IsNavMove)
        {
            StartCoroutine(StopMoveCoroutine(timer));
        }
    }

    protected IEnumerator StopMoveCoroutine(float timer)
    {
        IsNavMove = false;
        isForceMove = true;
        // 프레임 대기
        yield return new WaitForSeconds(timer); // Time.deltaTime 주기로 갱신
        isForceMove = false;
        IsNavMove = true;
    }
}
