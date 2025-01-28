using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField, ReadOnly(true)] protected float _duration = -1f;
    public float Duration => _duration;
    [SerializeField, ReadOnly] protected bool isPause = false;

    public event Action OnTimeElapsed;

    public void Constructor(float duration)
    {
        if (_duration < 0f)
        {
            _duration = duration;
        }
    }

    public void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (_duration > 0f)
        {
            if (!isPause)
            {
                _duration -= Time.deltaTime;
            }
            yield return null;
        }

        OnTimeElapsed?.Invoke();
        Destroy(gameObject);
    }
}
