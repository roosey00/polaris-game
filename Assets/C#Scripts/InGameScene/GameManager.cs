using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


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
    public static Vector3 ChangeX(Vector3 vector, float newX)
    {
        vector.x = newX;
        return vector;
    }

    public static Vector3 ChangeY(Vector3 vector, float newY)
    {
        vector.y = newY;
        return vector;
    }

    public static Vector3 ChangeZ(Vector3 vector, float newZ)
    {
        vector.z = newZ;
        return vector;
    }
}

 public class GameManager : MonoBehaviour
{
    // ������ �ᱸ��
    private GameManager() { }

    // singleton
    public static GameManager instance
    {
        get { return _instance; }
    }
    private static GameManager _instance = null;

    // ��������
    public GameObject playerObj;
    public Ground ground;
    public ParticleManager particleManager;
    public GameObject RangeTrigger;

    public delegate void voidvoidFunc();
    public delegate void voidGameobjectFunc(GameObject g_obj);
    public delegate void voidCreatureFunc(Creature crt);
    public delegate float floatfloatFunc(float flt);

    void Start()
    {
        //Physics.gravity = new Vector3(0, 0, 9.81f);
        if (_instance == null)
        {
            _instance = GetComponent<GameManager>();
        }
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
        }
        if (ground == null)
        {
            ground = GameObject.Find("Ground").GetComponent<Ground>();
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
