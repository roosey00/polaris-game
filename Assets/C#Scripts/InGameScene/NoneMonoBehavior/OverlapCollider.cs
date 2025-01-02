using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class OverlapCollider
{
    //public LayerMask targetLayer; // 탐색할 레이어

    //void Update()
    //{
    //    //PerformSemiCircleRaycast();
    //    //PerformOverlapBox(new Vector3(5f, 1f, 5f));
    //}

    static public HashSet<GameObject> SemicircleRaycast(Transform transform, float radius, float angleRange, LayerMask layerMask)
    {
        Vector3 center = transform.position; // 박스의 중심
        Collider[] colliders = Physics.OverlapSphere(center, radius, layerMask);
        HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        foreach (var collider in colliders)
        {
            Vector3 directionToCollider = (collider.transform.position - center).normalized;
            float angle = Vector3.Angle(Vector3.ProjectOnPlane(transform.forward, Vector3.up), directionToCollider);

            if (angle <= angleRange / 2)
            {
                Debug.Log($"Detected {collider.name} within the semicircle");
                gameObjects.Add(collider.gameObject);
            }
        }
        return gameObjects;
    }


    //public static HashSet<GameObject> SemicircleRaycast(Transform transform, float radius = 5f, float angleRange = 180f)
    //{
    //    // 시작 각도와 종료 각도 설정
    //    float startAngle = -angleRange / 2f;
    //    float endAngle = angleRange / 2f;
    //    int rayCount = (int)(angleRange * 2f);
    //    HashSet<GameObject> raycastHits = new HashSet<GameObject>();

    //    for (int i = 0; i <= rayCount; i++)
    //    {
    //        // 각도를 계산하여 라디안으로 변환
    //        float t = (float)i / rayCount;
    //        float angle = Mathf.Lerp(startAngle, endAngle, t);
    //        float radian = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;

    //        // 방향 벡터 계산
    //        Vector3 direction = new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));

    //        // RaycastAll 실행
    //        Ray ray = new Ray(transform.position, direction);
    //        RaycastHit[] hits = Physics.RaycastAll(ray, radius);

    //        foreach (var hit in hits)
    //        {
    //            Debug.Log($"Hit: {hit.collider.name}");
    //            // 충돌한 객체에 대한 처리
    //            Debug.DrawLine(transform.position, hit.point, Color.red);
    //            raycastHits.Add(hit.collider.gameObject);
    //        }

    //        // 탐색 범위 시각화 (충돌이 없을 경우만)
    //        if (hits.Length == 0)
    //        {
    //            Debug.DrawLine(transform.position, transform.position + direction * radius, Color.green);
    //        }
    //    }

    //    return raycastHits;
    //}

    public static HashSet<GameObject> OverlapBox(Transform transform, Vector3 boxSize, int rayCountPerSide, LayerMask layerMask)
    {
        Vector3 center = transform.position; // 박스의 중심
        Collider[] hits = Physics.OverlapBox(center, boxSize / 2, Quaternion.identity, layerMask);
        HashSet<GameObject> gameObjects = new HashSet<GameObject>();
            
        foreach (var hit in hits)
        {
            //Debug.Log($"Detected: {hit.name}");
            //// 충돌한 객체를 시각화
            //Debug.DrawLine(center, hit.transform.position, Color.red);
            gameObjects.Add(hit.gameObject);
        }

        // 박스 시각화
        Debug.DrawLine(center - boxSize / 2, center + boxSize / 2, Color.green);

        return gameObjects;
    }
}
