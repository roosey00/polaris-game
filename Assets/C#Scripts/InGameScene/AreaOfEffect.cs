using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.UI.CanvasScaler;

public class AreaOfEffect : MonoBehaviour
{
    protected HashSet<GameObject> triggeredArray = new HashSet<GameObject>();

    protected BoxCollider2D _boxCollider2D = null;
    protected BoxCollider2D boxCollider2D => _boxCollider2D ??= GetComponent<BoxCollider2D>();

    protected CircleCollider2D _circleCollider2D = null;
    protected CircleCollider2D circleCollider2D => _circleCollider2D ??= GetComponent<CircleCollider2D>();

    [SerializeField, ReadOnly] protected BaseDestroyTimer _baseDestroyTimer = null;
    public BaseDestroyTimer BaseDestroyTimer
    {
        get { return _baseDestroyTimer; }
        set { _baseDestroyTimer = value; }
    }

    [SerializeField, ReadOnly] private string targetLayerName = "Enemy";

    [SerializeField, ReadOnly] protected float damage = 90f;
    public float Damage
    {
        get => damage;
        set => damage = MathF.Max(value, 0f);
    }
    [SerializeField, ReadOnly] protected float damageStartTime = 0f;
    [SerializeField, ReadOnly] protected float _damageInterval = 5f;
    public float DamageInterval
    {
        get => _damageInterval;
        set 
        {            
            _damageInterval = MathF.Max(value, 0f);
            damageStartTime = _baseDestroyTimer.RemainingTime - _damageInterval;
        }
    }
    enum ScanMode 
    {
        BOX_MODE = 0, SEMICIRCLE_MODE = 1
    }
    [SerializeField, ReadOnly] ScanMode scanMode = ScanMode.SEMICIRCLE_MODE;

    [SerializeField, ReadOnly] private float range = 1f;

    [ReadOnly] public string targetTag = "Enemy";

    public float Range
    {
        set
        {            
            range = value;
            transform.localScale = new Vector3(range, range, 1);
            //switch (scanMode)
            //{
            //    case ScanMode.BOX_MODE:
            //        boxCollider2D.size= new Vector2(range, range);
            //        break;
            //    case ScanMode.SEMICIRCLE_MODE:
            //        circleCollider2D.radius = range;
            //        break;
            //    default:
            //        Debug.Log("Range Error Value");
            //        break;
            //}
        }
        get => range;
    }
    [SerializeField, ReadOnly] private float angleRange = 60f;
    public float AngleRange
    {
        get => angleRange;
        set
        {
            angleRange = (value % 360f + 360f) % 360f;
            angleRange = (angleRange == 0) ? 360f : angleRange;
        }
    }

    //public Vector3 boxSize = new Vector3(5f, 1f, 5f);
    //public int rayCountPerSide = 5;

    public Action startFunc = null;
    public TickObject<Action<HashSet<GameObject>>> intervalFuncObject = null;
    public Action<HashSet<GameObject>> endFunc = null;
    public Action<GameObject> triggerFunc = null;
    // Start is called before the first frame update
    private void Start()
    {
        startFunc?.Invoke();

        intervalFuncObject?.Let(interval => StartCoroutine(TickFunction(triggeredArray, interval.afterWaitTick)));

        if (_baseDestroyTimer == null)
        {
            Destroy(this);
        }

        boxCollider2D.enabled = (scanMode == ScanMode.BOX_MODE);
        circleCollider2D.enabled = (scanMode == ScanMode.SEMICIRCLE_MODE);
    }

    private void Update()
    {
        UpdateTimer();
        // ȸ�� ���� 3d��Ҹ� �����ϱ� ���� ����
        transform.position = GameManager.ChangeZ(transform.position, 0f);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        //ScanDetection();
    }

    private void UpdateTimer()
    {
        if (DamageInterval > 0 &&
            _baseDestroyTimer.RemainingTime <= damageStartTime - _damageInterval)
        {
            DamagedFunc();
            damageStartTime -= DamageInterval;
        }
        endFunc?.Invoke(triggeredArray);
    }

    //private void ScanDetection()
    //{
    //    HashSet<GameObject> gameObjects = new HashSet<GameObject>();
    //    switch (scanMode)
    //    {
    //        case ScanMode.BOX_MODE:
    //            gameObjects = OverlapCollider.OverlapBox(transform, boxSize, rayCountPerSide, LayerMask.GetMask(targetLayerName));
    //            break;
    //        case ScanMode.SEMICIRCLE_MODE:
    //            gameObjects = OverlapCollider.SemicircleRaycast(transform, Range, AngleRange, LayerMask.GetMask(targetLayerName));
    //            break;
    //        default:
    //            break;
    //    }

    //    // Ž������ ���� ��� ���� ����
    //    if (gameObjects.Count == 0)
    //    {
    //        triggeredArray.Clear();
    //        return;
    //    }

    //    //HashSet<GameObject> IntersectionSet = new HashSet<GameObject>(gameObjects.Intersect(triggeredArray));

    //    // Intersection ����� HashSet���� ��ȯ
    //    HashSet<GameObject> ScannedExceptSet = new HashSet<GameObject>(gameObjects.Except(triggeredArray));

    //    // onTriggerEnter
    //    foreach (GameObject eachGameObject in ScannedExceptSet)
    //    {
    //        if (isTriggerDmg)
    //        {
    //            DamagedTriggerFunc(eachGameObject);
    //        }
    //        triggerFunc?.Invoke(eachGameObject);

    //        triggeredArray.Add(eachGameObject);
    //    }

    //    // onTriggerExist
    //    //foreach (GameObject eachGameObject in ExistingExceptSet)
    //    //{
    //    //    triggeredArray.Remove(eachGameObject.GetComponent<GameObject>());            
    //    //}


    //    triggeredArray = new HashSet<GameObject>(gameObjects);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Vector3 center = transform.position; // �ڽ��� �߽�
            Vector3 forwardDirection = transform.right; // �÷��̾ �ٶ󺸴� ���� (2D���� ���� transform.right ���)

            // �浹ü�� ���ϴ� ���� ���
            Vector3 directionToCollider = (collision.transform.position - center).normalized;

            // 2D ��鿡�� ���� ��� (z���� ����)
            float angleToCollider = Mathf.Atan2(directionToCollider.y, directionToCollider.x) * Mathf.Rad2Deg;
            float forwardAngle = Mathf.Atan2(forwardDirection.y, forwardDirection.x) * Mathf.Rad2Deg;

            // �� ������ ���� ���
            float angleDifference = Mathf.DeltaAngle(forwardAngle, angleToCollider);

            //Debug.DrawLine(center, center + forwardDirection * 5f, Color.green); // ���� 5 ������ ��� ��
            //Debug.Log($"Detected: Angle to {collision.name} is {angleDifference}. angleRange is {angleRange}.");

            // Ž�� ���� ���� ������ Ȯ��
            if (Mathf.Abs(angleDifference) <= angleRange / 2)
            {
                if (_damageInterval == 0f)
                {
                    DamagedTriggerFunc(collision.gameObject);
                }
                triggerFunc?.Invoke(collision.gameObject);

                triggeredArray.Add(collision.gameObject);

                //Debug.Log($"Detected {collision.name} within the semicircle");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            triggeredArray.Remove(collision.gameObject);
            //Debug.Log($"{collision.name} out of semicircle");
        }
    }

    IEnumerator TickFunction(HashSet<GameObject> list, float tick)
    {
        while (true)
        {
            if (list.Count > 0) // ���� �߰�: ����Ʈ�� �׸��� ���� ���� �۾� ����
            {
                intervalFuncObject.obj?.Invoke(list);
            }

            yield return new WaitForSeconds(intervalFuncObject.afterWaitTick); // �� ������ ��� (�񵿱� ó��)
        }
    }

    private void DamagedFunc()
    {
        foreach (var e in triggeredArray)
        {
            e.GetComponent<Creature>()?.TakeDamage(damage);
        }        
    }

    private void DamagedTriggerFunc(GameObject gameObject)
    {
        gameObject.GetComponent<Creature>()?.TakeDamage(damage);

    }
}
