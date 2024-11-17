using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Room))]
public class RoomEditor : Editor
{
    private void OnSceneGUI()
    {
        Room room = (Room)target;

        // 显示房间的当前中心点和大小
        Handles.color = new Color(0, 1, 0, 0.2f);

        // 可视化房间范围
        Handles.DrawWireCube(room.roomCenter, room.roomSize);

        // 中心点调整工具
        EditorGUI.BeginChangeCheck();
        Vector3 newCenter = Handles.PositionHandle(room.roomCenter, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(room, "Move Room Center");
            room.roomCenter = newCenter;
            EditorUtility.SetDirty(room);
        }

        // 尺寸调整工具
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

        // 添加一个刷新按钮
        if (GUILayout.Button("刷新房间物品"))
        {
            room.RefreshRoomItems();
            Debug.Log("房间物品已刷新，共计物品：" + room.itemCount);
        }
    }
}