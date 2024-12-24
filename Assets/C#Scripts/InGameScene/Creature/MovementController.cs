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
        get { return (GameManager.Instance.GroundMouseHit.MousePos - transform.position).normalized; }
    }

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Ư�� ��ġ�� �̵�
    public void MoveTo(Vector3? destination = null)
    {
        if (destination != null)
        {
            this.destination = destination.Value;
            nav.SetDestination(destination.Value);
        }
    }

    // �̵� ������ Ȯ��
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
        isMove = true;
    }


    // �̵� �ߴ�
    public void StopMovement()
    {
        nav.ResetPath();
    }
}
