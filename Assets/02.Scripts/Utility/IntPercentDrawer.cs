// Assets/Editor/IntPercentDrawer.cs

using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

[CustomPropertyDrawer(typeof(IntPercentAttribute))]
public class IntPercentDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.LabelField(position, label.text, "Use [IntPercent] with int.");
            return;
        }

        EditorGUI.BeginProperty(position, label, property);

        // 레이아웃 분할: 레이블 / 인풋 / 퍼센트
        float labelWidth = EditorGUIUtility.labelWidth;
        Rect labelRect = new Rect(position.x, position.y, labelWidth, position.height);
        Rect fieldRect = new Rect(position.x + labelWidth, position.y, position.width - labelWidth - 50,
            position.height);
        Rect percentRect = new Rect(position.x + position.width - 50, position.y, 50, position.height);

        EditorGUI.LabelField(labelRect, label);

        // 현재 배열(또는 리스트) 경로 찾기: "Datas.Array.data[0].Weight" -> "Datas"
        string path = property.propertyPath; // ex: Datas.Array.data[2].Weight
        string arrayPath = null;
        var m = Regex.Match(path, @"^(.+?)\.Array\.data\[\d+\]");
        if (m.Success) arrayPath = m.Groups[1].Value;

        int total = 0;
        if (!string.IsNullOrEmpty(arrayPath))
        {
            SerializedProperty arrayProp = property.serializedObject.FindProperty(arrayPath);
            if (arrayProp != null && arrayProp.isArray)
            {
                for (int i = 0; i < arrayProp.arraySize; i++)
                {
                    var elem = arrayProp.GetArrayElementAtIndex(i);
                    var weightProp = elem.FindPropertyRelative("Weight");
                    if (weightProp != null && weightProp.propertyType == SerializedPropertyType.Integer)
                        total += Mathf.Max(0, weightProp.intValue);
                }
            }
        }

        // 정수 입력 (가중치 자체를 편집)
        int newVal = EditorGUI.IntField(fieldRect, GUIContent.none, property.intValue);
        property.intValue = Mathf.Max(0, newVal);

        // 퍼센트 계산 및 표시
        float percent = (total <= 0) ? 0f : (property.intValue / (float)total) * 100f;
        EditorGUI.LabelField(percentRect, Mathf.RoundToInt(percent).ToString() + " %", EditorStyles.boldLabel);

        EditorGUI.EndProperty();
    }
}

public class IntPercentAttribute : PropertyAttribute
{
}