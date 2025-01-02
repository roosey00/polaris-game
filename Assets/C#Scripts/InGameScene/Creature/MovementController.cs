using System;
using System.Collections;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private NavMeshAgent nav;
    public Vector3 destination = Vector3.zero;

    const float FAST_ROTATION_SPEED = 10000f;
    public bool IsNavMoveMode
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
        if (IsNavMoveMode)
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
        if (IsNavMoveMode)
        {
            StartCoroutine(ForceMoveCoroutine(dir, speed, timer));
        }
    }

    protected IEnumerator ForceMoveCoroutine(Vector3 dir, float speed, float timer)
    {
        IsNavMoveMode = false;
        dir = GameManager.ChangeY(dir, 0);
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
        //nav.ResetPath();
        IsNavMoveMode = true;
    }

    public void StopMoveInSec(float timer)
    {
        if (IsNavMoveMode)
        {
            StartCoroutine(StopMoveCoroutine(timer));
        }
    }

    protected IEnumerator StopMoveCoroutine(float timer)
    {
        IsNavMoveMode = false;
        // ������ ���
        yield return new WaitForSeconds(timer); // Time.deltaTime �ֱ�� ����
        IsNavMoveMode = true;
    }
}
