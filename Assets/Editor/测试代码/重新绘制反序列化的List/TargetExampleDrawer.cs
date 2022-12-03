using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PlayerItem))]
public class TargetExampleDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            //设置属性名宽度
            EditorGUIUtility.labelWidth = 60;
            position.height = EditorGUIUtility.singleLineHeight;

            var iconRect = new Rect(position)
            {
                width = 64,
                height = 64
            };

            var prefabRect = new Rect(position)
            {
                width = position.width - 80,
                x = position.x + 80
            };

            var nameRect = new Rect(prefabRect)
            {
                y = prefabRect.y + EditorGUIUtility.singleLineHeight + 5
            };

            var attackSliderRect = new Rect(nameRect)
            {
                y = nameRect.y + EditorGUIUtility.singleLineHeight + 5
            };

            var iconProperty = property.FindPropertyRelative("icon");
            var prefabProperty = property.FindPropertyRelative("prefab");
            var nameProperty = property.FindPropertyRelative("name");
            var attackProperty = property.FindPropertyRelative("attack");

            iconProperty.objectReferenceValue = EditorGUI.ObjectField(iconRect, iconProperty.objectReferenceValue, typeof(Texture), false);
            nameProperty.stringValue = EditorGUI.TextField(nameRect, nameProperty.displayName, nameProperty.stringValue);
            prefabProperty.objectReferenceValue = EditorGUI.ObjectField(prefabRect, prefabProperty.objectReferenceValue, typeof(GameObject), false);
            attackProperty.intValue = EditorGUI.IntSlider(attackSliderRect, attackProperty.intValue, 0, 100);
        }
    }
}

