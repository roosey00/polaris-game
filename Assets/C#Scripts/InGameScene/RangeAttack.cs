using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.UI.CanvasScaler;

public class RangeAttack : MonoBehaviour
{
    protected HashSet<GameObject> triggeredArray = new HashSet<GameObject>();

    protected BoxCollider2D _boxCollider2D = null;
    protected BoxCollider2D boxCollider2D => _boxCollider2D ??= GetComponent<BoxCollider2D>();

    protected CircleCollider2D _circleCollider2D = null;
    protected CircleCollider2D circleCollider2D => _circleCollider2D ??= GetComponent<CircleCollider2D>();

    [SerializeField, ReadOnly] private string targetLayerName = "Enemy";

    [SerializeField, ReadOnly] protected float timer = 5f;
    public float Timer
    {
        get => timer;
        set => timer = MathF.Max(value, 0f);
    }
    [SerializeField, ReadOnly] protected float damage = 90f;
    public float Damage
    {
        get => damage;
        set => damage = MathF.Max(value, 0f);
    }
    [SerializeField, ReadOnly] protected float damageTimer = 5f;
    [SerializeField, ReadOnly] protected float DamageTime = 0f;
    public float DamageTimer
    {
        get => damageTimer;
        set 
        {
            damageTimer = MathF.Max(value, 0f);
            DamageTime = Timer - DamageTimer;
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
    public TickObject<Action<HashSet<GameObject>>> tickFuncObject = null;
    public Action<HashSet<GameObject>> endFunc = null;
    public Action<GameObject> triggerFunc = null;
    // Start is called before the first frame update
    private void Start()
    {
        startFunc?.Invoke();

        tickFuncObject?.Let(tick => StartCoroutine(TickFunction(triggeredArray, tick.afterWaitTick)));

        boxCollider2D.enabled = (scanMode == ScanMode.BOX_MODE);
        circleCollider2D.enabled = (scanMode == ScanMode.SEMICIRCLE_MODE);
    }

    private void Update()
    {
        UpdateTimer();
        // 회전 등의 3d요소를 제거하기 위한 고정
        transform.position = GameManager.ChangeZ(transform.position, 0f);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        //ScanDetection();
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if (DamageTimer > 0 && timer <= DamageTime)
            {
                DamagedFunc();
                DamageTime -= DamageTimer;
            }
            endFunc?.Invoke(triggeredArray);
            Destroy(gameObject);
        }
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

    //    // 탐지되지 않을 경우 조기 종료
    //    if (gameObjects.Count == 0)
    //    {
    //        triggeredArray.Clear();
    //        return;
    //    }

    //    //HashSet<GameObject> IntersectionSet = new HashSet<GameObject>(gameObjects.Intersect(triggeredArray));

    //    // Intersection 결과를 HashSet으로 변환
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
            Vector3 center = transform.position; // 박스의 중심
            Vector3 forwardDirection = transform.right; // 플레이어가 바라보는 방향 (2D에서 보통 transform.right 사용)

            // 충돌체로 향하는 방향 계산
            Vector3 directionToCollider = (collision.transform.position - center).normalized;

            // 2D 평면에서 각도 계산 (z축은 무시)
            float angleToCollider = Mathf.Atan2(directionToCollider.y, directionToCollider.x) * Mathf.Rad2Deg;
            float forwardAngle = Mathf.Atan2(forwardDirection.y, forwardDirection.x) * Mathf.Rad2Deg;

            // 두 각도의 차이 계산
            float angleDifference = Mathf.DeltaAngle(forwardAngle, angleToCollider);

            //Debug.DrawLine(center, center + forwardDirection * 5f, Color.green); // 길이 5 유닛의 녹색 선
            //Debug.Log($"Detected: Angle to {collision.name} is {angleDifference}. angleRange is {angleRange}.");

            // 탐색 각도 범위 내인지 확인
            if (Mathf.Abs(angleDifference) <= angleRange / 2)
            {
                if (damageTimer == 0f)
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
            if (list.Count > 0) // 조건 추가: 리스트에 항목이 있을 때만 작업 수행
            {
                tickFuncObject.obj?.Invoke(list);
            }

            yield return new WaitForSeconds(tickFuncObject.afterWaitTick); // 한 프레임 대기 (비동기 처리)
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
