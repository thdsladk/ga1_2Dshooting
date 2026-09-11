using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(IntPercentAttribute))]
public class IntPercentDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            float value = property.intValue / 100f;
            value = EditorGUI.Slider(position, label, value, 0f, 1f);
            property.intValue = Mathf.RoundToInt(value * 100);
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use [IntPercent] with int.");
        }
    }
}

public class IntPercentAttribute : PropertyAttribute
{
}