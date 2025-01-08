using System;
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
        if (property.NextVisible(true))
        {
            do
            {
                //Debug.Log($"{property.name} value is {property}");
                using (new EditorGUI.DisabledScope("m_Script" == property.propertyPath))
                {
                    DrawPropertyWithChildren(property);
                }
            }
            while (property.NextVisible(false));
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawPropertyWithChildren(SerializedProperty property)
    {
        // 현재 속성에 ReadOnlyAttribute가 있는지 확인
        var targetType = target.GetType();
        var memberInfo = targetType.GetMember(property.name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        bool isReadOnly = false;
        bool isOnlyRuntime = false;

        foreach (var member in memberInfo)
        {
            ReadOnlyAttribute readOnlyAttribute = member.GetCustomAttribute<ReadOnlyAttribute>();
            if (readOnlyAttribute != null)
            {
                isOnlyRuntime = !Application.isPlaying && readOnlyAttribute.runtimeOnly;
                isReadOnly = true;
                break;
            }
        }

        if (isReadOnly)
        {
            GUI.enabled = isOnlyRuntime;
            // ReadOnly 속성을 가진 멤버만 표시
            EditorGUILayout.PropertyField(property, true);

            //// 자식 속성 처리
            //if (property.hasChildren)
            //{
            //    SerializedProperty child = property.Copy();
            //    if (child.NextVisible(true))
            //    {
            //        EditorGUI.indentLevel++;
            //        do
            //        {
            //            using (new EditorGUI.DisabledScope(true))
            //            {
            //                EditorGUILayout.PropertyField(child, true);
            //            }
            //        }
            //        while (child.NextVisible(false) && child.propertyPath.StartsWith(property.propertyPath));
            //        EditorGUI.indentLevel--;
            //    }
            //}
            GUI.enabled = true;
        }
        else
        {
            // ReadOnly가 아닌 속성은 기본 처리
            EditorGUILayout.PropertyField(property, true);
        }
    }
}