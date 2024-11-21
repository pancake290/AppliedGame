using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelector
{
    
    public static RoomUnit GetRoomWithMostTargetItem(EnemyManager enemy, string itemType)
    {
        // 查找当前房间（enemy所处房间）及其周围房间中某个指定Item数量最多的房间
        RoomUnit targetRoom = enemy.currentRoom;
        int maxCount = 0;
        int currentCount = 0;
        foreach(ItemManager items in enemy.currentRoom.itemInRoom)
        {
            if(items.itemType == itemType)
            {
                currentCount += 1;
            }
        }
        if (currentCount > maxCount) maxCount = currentCount;

        foreach(RoomUnit roomUnit in enemy.currentRoom.adjacentRooms)
        {
            currentCount = 0;
            foreach(ItemManager items in roomUnit.itemInRoom)
            {
                if (items.itemType == itemType)
                {
                    currentCount += 1;
                }
            }
            if(currentCount > maxCount)
            {
                maxCount = currentCount;
                targetRoom = roomUnit;
            }
        }

        // 在当前房间和相邻房间中查找敌人最多的房间
        return targetRoom;
    }
}
