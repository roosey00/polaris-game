using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviour 기반의 싱글톤 추상 클래스
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool isShuttingDown = false;
    private static string instanceScene;

    [SerializeField] private string prefabPath; // Inspector에서 설정 가능

    /// <summary>
    /// 싱글톤 인스턴스에 접근하기 위한 프로퍼티
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance == null && !isShuttingDown)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject prefab = Resources.Load<GameObject>($"{typeof(T).Name}Prefab");
                    if (prefab != null)
                    {
                        instance = Instantiate(prefab).GetComponent<T>();
                    }
                    else
                    {
                        if (Debug.isDebugBuild)
                        {
                            Debug.LogWarning($"[Singleton] No prefab found for {typeof(T).Name}. Creating a new GameObject.");
                        }
                        GameObject singletonObject = new GameObject(typeof(T).Name);
                        instance = singletonObject.AddComponent<T>();
                    }
                }
            }
            return instance;
        }
    }

    /// <summary>
    /// 이벤트: 싱글톤 인스턴스가 생성될 때 호출
    /// </summary>
    public static event System.Action OnSingletonCreated;

    /// <summary>
    /// 이벤트: 싱글톤 인스턴스가 파괴될 때 호출
    /// </summary>
    public static event System.Action OnSingletonDestroyed;

    /// <summary>
    /// 초기화 작업을 수행하기 위한 메서드 (필요 시 오버라이드)
    /// </summary>
    protected virtual void Init() { }

    /// <summary>
    /// MonoBehaviour의 Awake 메서드를 오버라이드하여 싱글톤 인스턴스를 초기화
    /// </summary>
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            instanceScene = gameObject.scene.name;
            DontDestroyOnLoad(gameObject);
            Init();
            OnSingletonCreated?.Invoke();

            if (Debug.isDebugBuild)
            {
                Debug.Log($"[Singleton] {typeof(T).Name} instance created.");
            }
        }
        else if (instance != this)
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogWarning($"[Singleton] Duplicate instance of {typeof(T).Name} detected. Destroying duplicate.");
            }
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 싱글톤 인스턴스가 파괴될 때 호출
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (!isShuttingDown && instance == this)
        {
            OnSingletonDestroyed?.Invoke();
            instance = null;
        }
    }

    /// <summary>
    /// 애플리케이션 종료 시 싱글톤 인스턴스를 정리
    /// </summary>
    protected virtual void OnApplicationQuit()
    {
        isShuttingDown = true;
        instance = null;
    }
}