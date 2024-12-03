using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class InitializeBehaviour : MonoBehaviour
{
    /// <summary>
    /// 등록된 모든 컴포넌트를 초기화하는 메서드
    /// </summary>
    protected void Initialize<T>(T obj, Func<T, T> func = null, T nullCheck = null) where T : UnityEngine.Object
    {
        func = func ?? ((o) => o.GetComponent<T>());
        if (obj == nullCheck)
        {
            obj = func(obj); // 초기화 함수 호출
        }
    }
}