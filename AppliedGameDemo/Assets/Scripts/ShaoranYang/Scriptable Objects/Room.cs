using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRoom", menuName = "Room/Create New Room")]
public class Room : ScriptableObject
{
    [Header("��������")]
    public Vector3 roomCenter = Vector3.zero; // �������ĵ�
    public Vector3 roomSize = new Vector3(10, 10, 10); // �����С

    [Header("������Ʒ����")]
    public int itemCount;

    [Header("�����ڵ���Ʒ")]
    public List<GameObject> roomItems = new List<GameObject>();

    /// <summary>
    /// ˢ�·����ڵ���Ʒ
    /// </summary>
    public void RefreshRoomItems()
    {
        roomItems.Clear();

        // ʹ�÷������ĵ�ʹ�С�����Ʒ
        Collider[] colliders = Physics.OverlapBox(roomCenter, roomSize / 2, Quaternion.identity);

        foreach (var collider in colliders)
        {
            if (collider.gameObject != null)
            {
                roomItems.Add(collider.gameObject);
            }
        }

        // ������Ʒ����
        itemCount = roomItems.Count;
    }
}