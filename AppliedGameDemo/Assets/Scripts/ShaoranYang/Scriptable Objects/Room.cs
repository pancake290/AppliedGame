using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRoom", menuName = "Room/Create New Room")]
public class Room : ScriptableObject
{
    [Header("房间属性")]
    public Vector3 roomCenter = Vector3.zero; // 房间中心点
    public Vector3 roomSize = new Vector3(10, 10, 10); // 房间大小

    [Header("房间物品数量")]
    public int itemCount;

    [Header("房间内的物品")]
    public List<GameObject> roomItems = new List<GameObject>();

    /// <summary>
    /// 刷新房间内的物品
    /// </summary>
    public void RefreshRoomItems()
    {
        roomItems.Clear();

        // 使用房间中心点和大小检测物品
        Collider[] colliders = Physics.OverlapBox(roomCenter, roomSize / 2, Quaternion.identity);

        foreach (var collider in colliders)
        {
            if (collider.gameObject != null)
            {
                roomItems.Add(collider.gameObject);
            }
        }

        // 更新物品数量
        itemCount = roomItems.Count;
    }
}