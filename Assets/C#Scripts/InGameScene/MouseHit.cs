using UnityEngine;
using System.Collections;

/// <summary>
/// MouseHit 클래스: 화면 상의 마우스 위치를 Raycast로 검출하여 월드 좌표를 반환합니다.
/// </summary>
public class MouseHit : MonoBehaviour
{
    public string targetLayerName = null;

    /// <summary>
    /// 마우스 위치를 월드 좌표로 반환합니다.
    /// - Raycast를 사용하여 마우스가 클릭한 지점의 월드 좌표를 가져옵니다.
    /// - 만약 충돌이 감지되지 않으면 Vector3.positiveInfinity를 반환합니다.
    /// </summary>
    public Vector3 MousePos =>
            // 마우스 포인터로부터 생성한 Ray로 충돌 검사
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask(targetLayerName)) ?
            hit.point : // 충돌한 경우: 충돌 지점의 월드 좌표 반환
            Vector3.positiveInfinity; // 충돌하지 않은 경우: 무한대 값 반환

    /// <summary>
    /// MouseHit를 Vector3로 명시적 변환
    /// </summary>
    public static explicit operator Vector3(MouseHit hit) => hit.MousePos;
}