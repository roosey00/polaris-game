using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// public struct RateValue<T> where T : struct
// {
//     // 값과 비율
//     public T val;
//     public T rate;

//     // 값과 비율을 초기화하는 생성자
//     public RateValue(T val = default, T rate = default)
//     {
//         this.val = val;
//         this.rate = rate;
//     }

//     // 암시적 변환 연산자: RateValue -> T
//     public static implicit operator T(RateValue<T> rateValue)
//     {
//         dynamic v = rateValue.val;
//         dynamic r = rateValue.rate;
//         return v * r;
//     }

//     // 암시적 변환 연산자: T -> RateValue
//     public static implicit operator RateValue<T>(T val)
//     {
//         return new RateValue<T>(val);
//     }

//     // 문자열로 변환
//     public override string ToString()
//     {
//         return $"Value: {val}, Rate: {rate}";
//     }
// }

public class LockObject<T>
 {
    public LockObject(T obj, bool isLock = true)
    {
        this.isLock = isLock;
        this.obj = obj;
    }
    
    public bool isLock;
    public T obj;
 }

public class Vector3Modifier
{
    public static UnityEngine.Vector3 ChangeX(UnityEngine.Vector3 vector, float newX)
    {
        vector.x = newX;
        return vector;
    }

    public static UnityEngine.Vector3 ChangeY(UnityEngine.Vector3 vector, float newY)
    {
        vector.y = newY;
        return vector;
    }

    public static UnityEngine.Vector3 ChangeZ(UnityEngine.Vector3 vector, float newZ)
    {
        vector.z = newZ;
        return vector;
    }
}

 public class GameManager : Singleton<GameManager>
{
    // ������ �ᱸ��
    private GameManager() { }

    // ��������
    public GameObject playerObj = null;
    public MouseHit groundMouseHit = null;
    public ParticleManager particleManager = null;
    public GameObject RangeTrigger = null;

    public delegate void voidvoidFunc();
    public delegate void voidGameobjectFunc(GameObject g_obj);
    public delegate void voidCreatureFunc(Creature crt);
    public delegate float floatfloatFunc(float flt);

    new void Awake()
    {
        base.Awake();

        //Physics.gravity = new Vector3(0, 0, 9.81f);
        // Initialize(_instance);
        // Initialize(playerObj, (o) => GameObject.Find("Plyaer"));
        // Initialize(groundMouseHit, (o) => GameObject.Find("Ground").GetComponent<MouseHit>());
        // Initialize(particleManager, (o) => GameObject.Find("Particle Group").GetComponent<ParticleManager>());
        // Initialize(RangeTrigger, (o) => Resources.Load<GameObject>("Prefab/Attack Trigger"));
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
        }
        if (groundMouseHit == null)
        {
            groundMouseHit = GameObject.Find("Ground").GetComponent<MouseHit>();
        }
        if (particleManager == null)
        {
            particleManager = GameObject.Find("Particle Group").GetComponent<ParticleManager>();
        }    
        if (RangeTrigger == null)
        {
            RangeTrigger = Resources.Load<GameObject>("Prefab/Attack Trigger");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
