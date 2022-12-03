using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(TargetExample))]
public class TargetExampleEditor : Editor
{
    private ReorderableList _playerItemArray;

    private void OnEnable()
    {
        _playerItemArray = new ReorderableList(serializedObject, serializedObject.FindProperty("playerItemArray")
            , true, true, true, true);

        //自定义列表名称
        _playerItemArray.drawHeaderCallback = (Rect rect) =>
        {
            GUI.Label(rect, "Player Array");
        };

        //定义元素的高度
        _playerItemArray.elementHeight = 68;

        //自定义绘制列表元素
        _playerItemArray.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
        {
            //根据index获取对应元素 
            SerializedProperty item = _playerItemArray.serializedProperty.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;
            EditorGUI.PropertyField(rect, item, new GUIContent("Index " + index));
        };

        //当删除元素时候的回调函数，实现删除元素时，有提示框跳出
        _playerItemArray.onRemoveCallback = (ReorderableList list) =>
        {
            if (EditorUtility.DisplayDialog("Warnning", "Do you want to remove this element?", "Remove", "Cancel"))
            {
                ReorderableList.defaultBehaviours.DoRemoveButton(list);
            }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //自动布局绘制列表
        _playerItemArray.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}

public enum PrefabType
{
    Player,
    Enemy,
}

public struct Creation
{
    public PrefabType prefabType;
    public string path;
}

