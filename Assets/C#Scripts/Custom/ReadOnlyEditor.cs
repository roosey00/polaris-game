using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)]
public class ReadOnlyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 UI 유지
        //DrawDefaultInspector();

        serializedObject.Update();

        // 모든 필드 순회
        SerializedProperty property = serializedObject.GetIterator();
        // 스크립트 파일이면
        if (property.NextVisible(true))
        {
            do
            {
                using (new EditorGUI.DisabledScope(property.name[0] == 'm'))
                {
                    //Debug.Log($"{property.name} value is {property.GetType()}");
                    if (property is SerializedProperty)
                    {
                        DrawPropertyWithChildren(property);
                    }
                }
                
            }
            while (property.NextVisible(false));
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawPropertyWithChildren(SerializedProperty property)
    {
        // 현재 속성에 ReadOnlyAttribute가 있는지 확인
        Type targetType = target.GetType();
        HashSet<Type> excludedTypes = new HashSet<Type> { typeof(MonoBehaviour), typeof(object) };
        MemberInfo[] memberInfo = null;
        bool isReadOnly = false;
        bool isOnlyRuntime = false;

        // 부모 클래스를 따라가며 탐색
        while (targetType != null &&
            !excludedTypes.Contains(targetType))
        {
            memberInfo = targetType.GetMember(property.name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            //Debug.Log($"{property.name} in {targetType}");

            foreach (var member in memberInfo)
            {
                if (member != null)
                {
                    var readOnlyAttribute = member.GetCustomAttribute<ReadOnlyAttribute>();
                    //Debug.Log($"{member.Name} is {member.DeclaringType} and in the {target.GetType()}!");

                    if (readOnlyAttribute != null)
                    {
                        isOnlyRuntime = !Application.isPlaying && readOnlyAttribute.runtimeOnly;
                        isReadOnly = true;
                        break; // 원하는 타입 및 속성 발견 시 탐색 종료
                    }
                }
            }

            if (isReadOnly) break; // 탐색 종료

            targetType = targetType.BaseType; // 부모 클래스로 이동
        }


        if (isReadOnly)
        {
            using (new EditorGUI.DisabledScope(!isOnlyRuntime))
            {
                // ReadOnly 속성을 가진 멤버만 표시
                EditorGUILayout.PropertyField(property, true);
            }
        }
        else
        {
            // ReadOnly가 아닌 속성은 기본 처리
            EditorGUILayout.PropertyField(property, true);
        }
    }
}