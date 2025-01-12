using System;
using System.Collections;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class BaseMovementController
{
    protected NavMeshAgent navMesh;
    protected Creature creature;
    [ReadOnly] public Vector3 destination = Vector3.zero;

    const float FAST_ROTATION_SPEED = 10000f;
    public bool isMove => navMesh.velocity != Vector3.zero || isForceMove;
    public bool IsNavMove
    {
        get
        {
            return !navMesh.isStopped;
        }
        set
        {
            navMesh.isStopped = !value;
        }
    }
    protected bool isForceMove = false;

    public Vector3 MousePointDirNorm => (GameManager.Instance.GroundMouseHit.MousePos - creature.transform.position).normalized;

    public BaseMovementController(Creature creature)
    {
        this.creature = creature;
        this.navMesh = creature.GetComponent<NavMeshAgent>();
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
                navMesh.SetDestination(this.destination);
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
        if (speed == 0f)
        {
            StopMoveInSec(timer);
            return;
        }
        if (IsNavMove)
        {
            creature.StartCoroutine(ForceMoveCoroutine(dir, speed, timer));
        }
    }

    protected IEnumerator ForceMoveCoroutine(Vector3 dir, float speed, float timer)
    {
        IsNavMove = false;
        isForceMove = true;
        dir = GameManager.ChangeZ(dir, 0);
        creature.transform.LookAt(creature.transform.position + dir);
        // ���� ����
        while (timer > 0)
        {
            // �̵�
            creature.transform.position += dir.normalized * speed * Time.fixedDeltaTime;
            //transform.Translate(Vector3Modifier.ChangeY(dir, 0).normalized * speed * Time.fixedDeltaTime);

            // Ÿ�̸� ����
            timer -= Time.fixedDeltaTime;

            // ������ ���
            yield return new WaitForFixedUpdate(); // Time.deltaTime �ֱ�� ����
        }
        navMesh.ResetPath();
        isForceMove = false;
        IsNavMove = true;
    }

    public void StopMoveInSec(float timer)
    {
        if (IsNavMove)
        {
            creature.StartCoroutine(StopMoveCoroutine(timer));
        }
    }

    protected IEnumerator StopMoveCoroutine(float timer)
    {
        IsNavMove = false;
        // ������ ���
        yield return new WaitForSeconds(timer); // Time.deltaTime �ֱ�� ����
        navMesh.ResetPath();
        IsNavMove = true;
    }
}
