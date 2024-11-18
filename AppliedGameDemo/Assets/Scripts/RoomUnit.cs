using System.Collections.Generic;
using UnityEngine;

public class RoomUnit : MonoBehaviour
{
    // 房间的边界
    public Bounds roomBounds;

    // 相邻房间的列表
    public List<RoomUnit> adjacentRooms = new List<RoomUnit>();

    // 房间内的敌人列表
    public List<Enemy> enemiesInRoom = new List<Enemy>();
    public List<GameObject> trashInRoom = new List<GameObject>();

    private void Awake()
    {
        // 获取房间的边界
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            roomBounds = boxCollider.bounds;
        }
        else
        {
            Debug.LogError("房间缺少 BoxCollider");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (!enemiesInRoom.Contains(enemy))
            {
                enemiesInRoom.Add(enemy);
                //enemy.SetCurrentRoom(this);
            }
        }
        if (other.tag == "Trash") 
        {
            if (!trashInRoom.Contains(other.gameObject))
            {
                trashInRoom.Add(other.gameObject);
                //enemy.SetCurrentRoom(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (enemiesInRoom.Contains(enemy))
            {
                enemiesInRoom.Remove(enemy);
                //enemy.SetCurrentRoom(null);
            }
        }
    }
}