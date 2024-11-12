using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyController : MonoBehaviour
{
    public float moveIntensity = 0.5f;
    public float speed = 1.0f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // 위아래로 살짝 랜덤하게 움직이는 효과 추가
        float offsetY = Mathf.Sin(Time.time * speed) * moveIntensity;
        float offsetX = Mathf.Cos(Time.time * speed * 0.5f) * moveIntensity;

        transform.position = initialPosition + new Vector3(offsetX, offsetY, 0);
    }
}
