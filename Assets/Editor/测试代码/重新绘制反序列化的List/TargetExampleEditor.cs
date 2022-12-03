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

        //�Զ����б�����
        _playerItemArray.drawHeaderCallback = (Rect rect) =>
        {
            GUI.Label(rect, "Player Array");
        };

        //����Ԫ�صĸ߶�
        _playerItemArray.elementHeight = 68;

        //�Զ�������б�Ԫ��
        _playerItemArray.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
        {
            //����index��ȡ��ӦԪ�� 
            SerializedProperty item = _playerItemArray.serializedProperty.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;
            EditorGUI.PropertyField(rect, item, new GUIContent("Index " + index));
        };

        //��ɾ��Ԫ��ʱ��Ļص�������ʵ��ɾ��Ԫ��ʱ������ʾ������
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
        //�Զ����ֻ����б�
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

