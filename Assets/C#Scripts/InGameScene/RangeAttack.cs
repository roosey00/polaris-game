using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class RangeAttack : MonoBehaviour
{
    private HashSet<GameObject> triggeredArray = new HashSet<GameObject>();

    public string targetLayerName = "Enemy";

    public float timer = 5f;
    public float damage = 90f;

    enum ScanMode 
    {
        BOX_MODE = 0, SEMICIRCLE_MODE = 1
    }
    ScanMode scanMode = ScanMode.SEMICIRCLE_MODE;

    public float range = 10f;
    public float degree = 60f;

    public Vector3 boxSize = new Vector3(5f, 1f, 5f);
    public int rayCountPerSide = 5;

    public Action startFunc = null;
    public TickObject<Action<HashSet<GameObject>>> tickFuncObject = null;
    public Action<HashSet<GameObject>> endFunc = null;
    public Action<GameObject> triggerFunc = null;

    public bool isEndDmg = false;
    public bool isTriggerDmg = false;

    // Start is called before the first frame update
    private void Start()
    {
        startFunc?.Invoke();

        tickFuncObject?.Let(tick => StartCoroutine(TickFunction(triggeredArray, tick.Tick)));
    }

    private void Update()
    {
        UpdateTimer();
        ScanDetection();
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if (isEndDmg)
            {
                DamagedEndFunc();
            }
            endFunc?.Invoke(triggeredArray);
            Destroy(gameObject);
        }
    }

    private void ScanDetection()
    {
        HashSet<GameObject> gameObjects = new HashSet<GameObject>();
        switch (scanMode)
        {
            case ScanMode.BOX_MODE:
                gameObjects = OverlapCollider.OverlapBox(transform, boxSize, rayCountPerSide, LayerMask.GetMask(targetLayerName));
                break;
            case ScanMode.SEMICIRCLE_MODE:
                gameObjects = OverlapCollider.SemicircleRaycast(transform, range, degree, LayerMask.GetMask(targetLayerName));
                break;
            default:
                break;
        }

        // 탐지되지 않을 경우 조기 종료
        if (gameObjects.Count == 0)
        {
            triggeredArray.Clear();
            return;
        }

        //HashSet<GameObject> IntersectionSet = new HashSet<GameObject>(gameObjects.Intersect(triggeredArray));

        // Intersection 결과를 HashSet으로 변환
        HashSet<GameObject> ScannedExceptSet = new HashSet<GameObject>(gameObjects.Except(triggeredArray));

        // onTriggerEnter
        foreach (GameObject eachGameObject in ScannedExceptSet)
        {
            if (isTriggerDmg)
            {
                DamagedTriggerFunc(eachGameObject);
            }
            triggerFunc?.Invoke(eachGameObject);

            triggeredArray.Add(eachGameObject);
        }

        // onTriggerExist
        //foreach (GameObject eachGameObject in ExistingExceptSet)
        //{
        //    triggeredArray.Remove(eachGameObject.GetComponent<GameObject>());            
        //}
        triggeredArray = new HashSet<GameObject>(gameObjects);
    }

    IEnumerator TickFunction(HashSet<GameObject> list, float tick)
    {
        while (true)
        {
            if (list.Count > 0) // 조건 추가: 리스트에 항목이 있을 때만 작업 수행
            {
                tickFuncObject.obj?.Invoke(list);
            }

            yield return new WaitForSeconds(tickFuncObject.Tick); // 한 프레임 대기 (비동기 처리)
        }
    }

    private void DamagedEndFunc()
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
