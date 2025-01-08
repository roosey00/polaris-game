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
        
        // �⺻ �ν����� UI ����
        //DrawDefaultInspector();

        serializedObject.Update();

        // ��� �ʵ� ��ȸ
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
        // ���� �Ӽ��� ReadOnlyAttribute�� �ִ��� Ȯ��
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
            // ReadOnly �Ӽ��� ���� ����� ǥ��
            EditorGUILayout.PropertyField(property, true);

            //// �ڽ� �Ӽ� ó��
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
            // ReadOnly�� �ƴ� �Ӽ��� �⺻ ó��
            EditorGUILayout.PropertyField(property, true);
        }
    }
}