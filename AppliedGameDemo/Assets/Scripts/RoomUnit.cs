using System.Collections.Generic;
using UnityEngine;

public class RoomUnit : MonoBehaviour
{
    // 房间的边界
    public Bounds roomBounds;

    // 相邻房间的列表
    public List<RoomUnit> adjacentRooms = new List<RoomUnit>();

    // 房间内的敌人列表
    public List<EnemyManager> enemiesInRoom = new List<EnemyManager>();
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

    private void OnTriggerEnter(Collider other)//当敌人进到房间范围，被添加到房间敌人列表中
    {
        EnemyManager enemy = other.GetComponent<EnemyManager>();
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

    private void OnTriggerExit(Collider other)//当敌人出了房间范围，被从敌人列表移除
    {
        EnemyManager enemy = other.GetComponent<EnemyManager>();
        if (enemy != null)
        {
            if (enemiesInRoom.Contains(enemy))
            {
                enemiesInRoom.Remove(enemy);
                //enemy.SetCurrentRoom(null);
            }
        }
    }

    public void RemoveEnemy(EnemyManager enemy)
    {
        if (enemiesInRoom.Contains(enemy))
        {
            enemy.gameObject.transform.GetComponent<BoxCollider>().enabled = false;
            enemiesInRoom.Remove(enemy);
        }
    }
}