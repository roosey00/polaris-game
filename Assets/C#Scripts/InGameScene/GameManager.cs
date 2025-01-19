using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, ReadOnly] protected Transform _canvasUI = null;
    public Transform CanvasUI => _canvasUI;

    [SerializeField, ReadOnly] protected Transform rootTransform = null;
    public Transform RootTransform => rootTransform;

    [SerializeField, ReadOnly] protected GameObject playerObj = null;
    public GameObject PlayerObj => playerObj;

    [SerializeField, ReadOnly] protected Player _playerClass = null;
    public Player PlayerClass => _playerClass;

    [SerializeField, ReadOnly] protected MouseHit groundMouseHit = null;
    public MouseHit GroundMouseHit => groundMouseHit;

    [SerializeField, ReadOnly] protected ParticleManager particleManager = null;
    public ParticleManager ParticleManager => particleManager;

    [SerializeField, ReadOnly(true)] protected GameObject _rangeTrigger = null;
    public GameObject RangeTrigger => _rangeTrigger;

    [SerializeField, ReadOnly(true)] protected GameObject _followHealthBar = null;
    public GameObject FollowHealthBar => _followHealthBar;

    public static UnityEngine.Vector3 ChangeX(UnityEngine.Vector3 vector, float newX)
        => new Vector3(newX, vector.y, vector.z);
    public static UnityEngine.Vector3 ChangeY(UnityEngine.Vector3 vector, float newY)
        => new Vector3(vector.x, newY, vector.z);
    public static UnityEngine.Vector3 ChangeZ(UnityEngine.Vector3 vector, float newZ)
        => new Vector3(vector.x, vector.y, newZ);

    protected void Start()
    {
        Physics.gravity = new Vector3(0, 0, 9.81f);
    }

    override protected void InitalizeComponent()
    {
        _canvasUI = GameObject.Find("Canvas_UI").transform;
        rootTransform = GameObject.Find("ForUseXYZ").transform;
        playerObj = rootTransform.Find("Player").gameObject;
        _playerClass = playerObj.GetComponent<Player>();
        groundMouseHit = rootTransform.Find("Ground").GetComponent<MouseHit>();
        particleManager = rootTransform.Find("Particle Group").GetComponent<ParticleManager>();
        _rangeTrigger = Resources.Load<GameObject>("Prefab/Attack Trigger");
        _followHealthBar = Resources.Load<GameObject>("Prefab/FollowHealthBar");
    }

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
