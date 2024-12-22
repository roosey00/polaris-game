using UnityEngine;

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

 public class GameManager : Singleton<GameManager>
{
    // ��������
    public GameObject playerObj = null;
    public MouseHit groundMouseHit = null;
    public ParticleManager particleManager = null;
    public GameObject RangeTrigger = null;

    public static UnityEngine.Vector3 ChangeX(UnityEngine.Vector3 vector, float newX)
    => new Vector3(newX, vector.y, vector.z);
    public static UnityEngine.Vector3 ChangeY(UnityEngine.Vector3 vector, float newY)
        => new Vector3(vector.x, newY, vector.z);
    public static UnityEngine.Vector3 ChangeZ(UnityEngine.Vector3 vector, float newZ)
        => new Vector3(vector.x, vector.y, newZ);

    protected override void Init()
    {
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
}
