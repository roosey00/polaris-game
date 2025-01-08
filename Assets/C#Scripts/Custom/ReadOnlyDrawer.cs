using UnityEditor;
using UnityEngine;
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 읽기 전용 상태 활성화
        GUI.enabled = !Application.isPlaying && ((ReadOnlyAttribute)attribute).runtimeOnly;

        // Generic 타입 처리
        if (property.propertyType == SerializedPropertyType.Generic)
        {
            // Foldout을 렌더링
            property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), property.isExpanded, label);

            if (property.isExpanded)
            {
                SerializedProperty childProperty = property.Copy();
                SerializedProperty endProperty = property.GetEndProperty();

                position.y += EditorGUIUtility.singleLineHeight;

                while (childProperty.NextVisible(true) && !SerializedProperty.EqualContents(childProperty, endProperty))
                {
                    float childHeight = EditorGUI.GetPropertyHeight(childProperty, true);
                    Rect childPosition = new Rect(position.x + 15, position.y, position.width - 15, childHeight);

                    DrawDefaultProperty(childPosition, childProperty, label);
                    //EditorGUI.PropertyField(childPosition, childProperty, true);
                    position.y += childHeight + EditorGUIUtility.standardVerticalSpacing;
                }
            }
        }
        else
        {
            // Generic이 아닌 기본 타입 처리
            DrawDefaultProperty(position, property, label);
        }

        //// 읽기 전용 상태 복구
        GUI.enabled = true;
    }

    private void DrawDefaultProperty(Rect position, SerializedProperty property, GUIContent label)
    {

        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                property.intValue = EditorGUI.IntField(position, label, property.intValue);
                break;
            case SerializedPropertyType.Float:
                property.floatValue = EditorGUI.FloatField(position, label, property.floatValue);
                break;
            case SerializedPropertyType.String:
                property.stringValue = EditorGUI.TextField(position, label, property.stringValue);
                break;
            case SerializedPropertyType.ObjectReference:
                property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(Object), true);
                break;
            case SerializedPropertyType.Boolean:
                property.boolValue = EditorGUI.Toggle(position, label, property.boolValue);
                break;
            default:
                EditorGUI.LabelField(position, label.text, $"{property.propertyType} is Unsupported Type");
                break;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.Generic && property.isExpanded)
        {
            float totalHeight = EditorGUIUtility.singleLineHeight;
            SerializedProperty childProperty = property.Copy();
            SerializedProperty endProperty = property.GetEndProperty();

            while (childProperty.NextVisible(true) && !SerializedProperty.EqualContents(childProperty, endProperty))
            {
                totalHeight += EditorGUI.GetPropertyHeight(childProperty, true) + EditorGUIUtility.standardVerticalSpacing;
            }

            return totalHeight;
        }

        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}