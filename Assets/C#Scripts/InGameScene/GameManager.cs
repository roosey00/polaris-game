using UnityEngine;


 public class GameManager : Singleton<GameManager>
{
    // ��������
    protected GameObject playerObj = null;
    public GameObject PlayerObj => playerObj ??= GameObject.Find("Player");

    protected MouseHit groundMouseHit = null;
    public MouseHit GroundMouseHit => groundMouseHit ??= GameObject.Find("Ground").GetComponent<MouseHit>();

    protected ParticleManager particleManager = null;
    public ParticleManager ParticleManager => particleManager ??= GameObject.Find("Particle Group").GetComponent<ParticleManager>();

    protected GameObject rangeTrigger = null;
    public GameObject RangeTrigger => rangeTrigger ??= Resources.Load<GameObject>("Prefab/Attack Trigger");

    public static UnityEngine.Vector3 ChangeX(UnityEngine.Vector3 vector, float newX)
        => new Vector3(newX, vector.y, vector.z);
    public static UnityEngine.Vector3 ChangeY(UnityEngine.Vector3 vector, float newY)
        => new Vector3(vector.x, newY, vector.z);
    public static UnityEngine.Vector3 ChangeZ(UnityEngine.Vector3 vector, float newZ)
        => new Vector3(vector.x, vector.y, newZ);

    //protected void Start()
    //{
    //    //Physics.gravity = new Vector3(0, 0, 9.81f);
    //    // Initialize(_instance);
    //    // Initialize(playerObj, (o) => GameObject.Find("Plyaer"));
    //    // Initialize(groundMouseHit, (o) => GameObject.Find("Ground").GetComponent<MouseHit>());
    //    // Initialize(particleManager, (o) => GameObject.Find("Particle Group").GetComponent<ParticleManager>());
    //    // Initialize(RangeTrigger, (o) => Resources.Load<GameObject>("Prefab/Attack Trigger"));

    //    if (groundMouseHit == null)
    //    {
    //        groundMouseHit = GameObject.Find("Ground").GetComponent<MouseHit>();
    //    }
    //    if (particleManager == null)
    //    {
    //        particleManager = GameObject.Find("Particle Group").GetComponent<ParticleManager>();
    //    }    
    //    if (rangeTrigger == null)
    //    {
    //        rangeTrigger = Resources.Load<GameObject>("Prefab/Attack Trigger");
    //    }
    //}
}
