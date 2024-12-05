using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowOutline : MonoBehaviour
{
    public float Size = 1.2f;

    void Update()
    {
        // 부모 객체의 회전을 제거한 방향 계산
        Quaternion parentRotation = transform.parent ? transform.parent.rotation : Quaternion.identity;
        Vector3 dir = (transform.position - Camera.main.transform.position).normalized;

        // 부모 객체의 회전을 보정
        dir = Quaternion.Inverse(parentRotation) * dir;

        // 스케일 설정
        transform.localScale = dir * Size;
    }
}