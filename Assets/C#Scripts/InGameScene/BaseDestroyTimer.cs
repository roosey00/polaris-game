using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

[Serializable]
public class BaseDestroyTimer
{
    protected MonoBehaviour monoBehaviour;

    [SerializeField, ReadOnly(true)] protected float _remainingTime = -1f;
    public float RemainingTime => _remainingTime;
    [SerializeField, ReadOnly] protected bool isPause = false;

    public event Action OnTimeElapsed;

    public BaseDestroyTimer(MonoBehaviour monoBehaviour, float remainingTime)
    {
        this.monoBehaviour = monoBehaviour;
        _remainingTime = remainingTime;
        monoBehaviour.StartCoroutine(TimerCoroutine());
    }

    protected IEnumerator TimerCoroutine()
    {
        while (_remainingTime > 0f)
        {
            if (!isPause)
            {
                _remainingTime -= Time.deltaTime;
            }
            yield return null;
        }

        OnTimeElapsed?.Invoke();
        GameObject.Destroy(monoBehaviour.gameObject);
    }
}
