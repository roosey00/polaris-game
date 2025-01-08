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
    /// Ư�� ��ġ�� �̵�
    /// null�� ���, ���� �������� ���� ��� �̵�
    /// </summary>
    /// <param name="destination">������</param>
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
    /// ���� �̵�
    /// </summary>
    /// <param name="dir">����</param>
    /// <param name="speed">�ӵ�</param>
    /// <param name="timer">�̵��� �ð�</param>
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
        // ���� ����
        while (timer > 0)
        {
            // �̵�
            transform.position += dir.normalized * speed * Time.fixedDeltaTime;
            //transform.Translate(Vector3Modifier.ChangeY(dir, 0).normalized * speed * Time.fixedDeltaTime);

            // Ÿ�̸� ����
            timer -= Time.fixedDeltaTime;

            // ������ ���
            yield return new WaitForFixedUpdate(); // Time.deltaTime �ֱ�� ����
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
        // ������ ���
        yield return new WaitForSeconds(timer); // Time.deltaTime �ֱ�� ����
        isForceMove = false;
        IsNavMove = true;
    }
}
