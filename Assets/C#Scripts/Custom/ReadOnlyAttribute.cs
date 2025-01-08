using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ReadOnlyAttribute : PropertyAttribute
{
    public readonly bool runtimeOnly;
    public ReadOnlyAttribute(bool runtimeOnly = false)
    {
        this.runtimeOnly = runtimeOnly;
    }
}

//#if UNITY_EDITOR
//[CustomPropertyDrawer(typeof(ReadOnlyAttribute), true)]
//public class ReadOnlyDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        EditorGUI.BeginDisabledGroup(true);
//        EditorGUI.PropertyField(position, property, label, true);
//        EditorGUI.EndDisabledGroup();
//    }

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        if (property.propertyType == SerializedPropertyType.Generic)
//        {
//            // CustomClass�� ���� ���� Ÿ���� ��� �ڽ� �ʵ� ���̸� �ջ�
//            float height = EditorGUIUtility.singleLineHeight; // Ŭ���� ��ü �� ����
//            foreach (var child in property.GetVisibleChildren())
//            {
//                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
//            }
//            return height;
//        }

//        return EditorGUI.GetPropertyHeight(property, label, true); // �⺻ �ʵ� ����
//    }
//}
//#endif