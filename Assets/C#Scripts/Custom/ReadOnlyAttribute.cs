using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field)]
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
//            // CustomClass와 같은 복합 타입은 모든 자식 필드 높이를 합산
//            float height = EditorGUIUtility.singleLineHeight; // 클래스 자체 라벨 높이
//            foreach (var child in property.GetVisibleChildren())
//            {
//                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
//            }
//            return height;
//        }

//        return EditorGUI.GetPropertyHeight(property, label, true); // 기본 필드 높이
//    }
//}
//#endif