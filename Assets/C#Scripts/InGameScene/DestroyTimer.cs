using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField, ReadOnly] protected float _duration;
    public float Duration => _duration;    
    public event Action OnTimeElapsed;

    public void StartTimer(float duration)
    {
        _duration = Mathf.Max(0, duration);        
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (_duration > 0f)
        {
            _duration -= Time.deltaTime;
            yield return null;
        }

        OnTimeElapsed?.Invoke();
        Destroy(gameObject);
    }
}
