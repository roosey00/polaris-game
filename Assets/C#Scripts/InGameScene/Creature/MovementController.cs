using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private NavMeshAgent nav;
    Vector3 destination = Vector3.zero;

    const float FAST_ROTATION_SPEED = 10000f;
    public bool isMove
    {
        set
        {
            MoveTo();
            //nav.enabled = value;
            //nav.angularSpeed = nav.acceleration = value ? FAST_ROTATION_SPEED : 0f;
        }
        get 
        {
            return IsMoving();
            //return nav.enabled; 
        }//return nav.acceleration == FAST_ROTATION_SPEED; }
    }

    public Vector3 MousePointDirNorm
    {
        get { return (GameManager.Instance.groundMouseHit.MousePos - transform.position).normalized; }
    }

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // 특정 위치로 이동
    public void MoveTo(Vector3? destination = null)
    {
        if (destination != null)
        {
            this.destination = destination.Value;
            nav.SetDestination(destination.Value);
        }
    }

    // 이동 중인지 확인
    public bool IsMoving()
    {
        return nav.velocity.sqrMagnitude > 0;
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
        dir = GameManager.ChangeY(dir, 0);
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


    // 이동 중단
    public void StopMovement()
    {
        nav.ResetPath();
    }
}
