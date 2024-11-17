using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Room))]
public class RoomEditor : Editor
{
    private void OnSceneGUI()
    {
        Room room = (Room)target;

        // ��ʾ����ĵ�ǰ���ĵ�ʹ�С
        Handles.color = new Color(0, 1, 0, 0.2f);

        // ���ӻ����䷶Χ
        Handles.DrawWireCube(room.roomCenter, room.roomSize);

        // ���ĵ��������
        EditorGUI.BeginChangeCheck();
        Vector3 newCenter = Handles.PositionHandle(room.roomCenter, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(room, "Move Room Center");
            room.roomCenter = newCenter;
            EditorUtility.SetDirty(room);
        }

        // �ߴ��������
        EditorGUI.BeginChangeCheck();
        Vector3 newSize = Handles.ScaleHandle(room.roomSize, room.roomCenter, Quaternion.identity, HandleUtility.GetHandleSize(room.roomCenter));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(room, "Resize Room");
            room.roomSize = newSize;
            EditorUtility.SetDirty(room);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Room room = (Room)target;

        // ���һ��ˢ�°�ť
        if (GUILayout.Button("ˢ�·�����Ʒ"))
        {
            room.RefreshRoomItems();
            Debug.Log("������Ʒ��ˢ�£�������Ʒ��" + room.itemCount);
        }
    }
}