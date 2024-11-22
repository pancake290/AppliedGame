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
    public List<EnemyManager> enemiesInRoomToAdd = new List<EnemyManager>();
    public List<EnemyManager> enemiesInRoomToRemove = new List<EnemyManager>();
    public List<ItemManager> itemInRoom = new List<ItemManager>();
    public List<ItemManager> itemInRoomToAdd = new List<ItemManager>();
    public List<ItemManager> itemInRoomToRemove = new List<ItemManager>();

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
        EnemyManager enemy = other.GetComponent<EnemyManager>();//当敌人进到房间范围，被添加到房间敌人列表中
        if (enemy != null)
        {
            if (!enemiesInRoom.Contains(enemy))
            {
                enemiesInRoomToAdd.Add(enemy);
                //enemy.SetCurrentRoom(this);
            }
        }
        ItemManager item = other.GetComponent<ItemManager>();//当垃圾人进到房间范围，被添加到房间Item列表中
        if (item != null) 
        {
            if (!itemInRoom.Contains(item))
            {
                itemInRoomToAdd.Add(item);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyManager enemy = other.GetComponent<EnemyManager>();//当敌人出了房间范围，被从敌人列表移除
        if (enemy != null)
        {
            if (enemiesInRoom.Contains(enemy))
            {
                enemiesInRoomToRemove.Add(enemy);
                //enemy.SetCurrentRoom(null);
            }
        }
        ItemManager item = other.GetComponent<ItemManager>();
        if (item != null)
        {
            if (!itemInRoom.Contains(item))
            {
                itemInRoomToRemove.Add(item);
            }
        }
    }

    public void RemoveEnemy(EnemyManager enemy)//给其它功能用来移除移除敌人，比如敌人死亡
    {
        if (enemiesInRoom.Contains(enemy))
        {
            enemy.gameObject.transform.GetComponent<BoxCollider>().enabled = false;
            enemiesInRoomToRemove.Add(enemy);
        }
    }

    public void UpdateList() 
    {
        foreach (var enemy in enemiesInRoomToAdd)
        {
            enemiesInRoom.Add(enemy);
        }
        enemiesInRoomToAdd.Clear();
        foreach (var enemy in enemiesInRoomToRemove)
        {
            enemiesInRoom.Remove(enemy);
        }
        enemiesInRoomToRemove.Clear();
        foreach (var item in itemInRoomToAdd)
        {
            itemInRoom.Add(item);
        }
        itemInRoomToAdd.Clear();
        foreach (var item in itemInRoomToRemove)
        {
            itemInRoom.Remove(item);
        }
        itemInRoomToRemove.Clear();
    }
}